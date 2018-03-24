using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using FigureSearch.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FigureSearch.WebScraping.Amazon
{
    public class AmazonOperator : WebOperatorBase
    {
        private const string TopPage = "https://www.amazon.co.jp/";
        private const string ImageKey = "Amazon";
        private readonly TimeSpan _timeout = new TimeSpan(0, 0, 15);

        public override IWebDriver GetSearchResultPage(string searchText, SeleniumBrowers.Name browser, bool headlessMode)
        {
            IWebDriver webDriver = SeleniumOperation.CreateInstance(browser, headlessMode);

            webDriver.Url = TopPage;

            // 検索フォームにカーソルを移す
            IWebElement searchElement = webDriver.FindElement(By.Id(Attributes.twotabsearchtextbox.GetValue()));
            // 検索ワードを入力して検索実行
            searchElement.SendKeys(searchText);
            searchElement.Submit();

            return webDriver;
        }

        public override SimpleProduct[] GetProductDataList(IWebDriver webDriver)
        {
            try
            {
                // 特定の要素が出現するまで_timeout秒待つ
                var wait = new WebDriverWait(webDriver, _timeout);
                wait.Until(ExpectedConditions.ElementExists(By.Id(Attributes.result_.GetValue() + "0")));

                /* 検索結果に存在する商品名とURLを取得する(1Pageで最大31個存在する) */
                IWebElement[] resultElements = new IWebElement[31];
                SimpleProduct[] results;
                // 検索結果に商品が何個存在しているかカウントする
                int productCount = -1;

                try
                {
                    for (int i = 0; i < resultElements.Length; i++)
                    {
                        resultElements[productCount + 1] =
                            webDriver.FindElement(By.Id(Attributes.result_.GetValue() + (productCount + 1)));
                        productCount++;
                    }
                }
                catch (NoSuchElementException)
                {
                }
                finally
                {
                    results = new SimpleProduct[productCount + 1];
                    for (int i = 0; i <= productCount; i++)
                    {
                        results[i] = new SimpleProduct(
                            resultElements[i].FindElements(By.TagName("a"))[1].GetAttribute("title"),
                            resultElements[i].FindElements(By.TagName("a"))[1].GetAttribute("href"));
                    }
                }

                return results;
            }
            catch (Exception e)
            {
                if (e is NoSuchElementException ||
                   e is WebDriverTimeoutException)
                    return null;
                else
                    throw;
            }
        }

        public override DetailProduct GetOneProductData(IWebDriver webDriver, string productUrl)
        {
            DetailProduct product;

            if (productUrl == null)
            {
                webDriver.Quit();
                product = new DetailProduct(
                    ImageKey,
                    "検索がヒットしませんでした。",
                    "null",
                    "null",
                    "null",
                    0,
                    "null");

                return product;
            }

            webDriver.Navigate().GoToUrl(productUrl);

            /* 検索結果から各情報を引き抜く */
            string productName = webDriver
                .FindElement(By.Id(Attributes.centerCol.GetValue()))
                .FindElement(By.TagName("span")).Text;

            string imageUrl;
            // 画像を取得
            // 通常imageの方で画像を取得できるが、kindleのページだけ？image2を用いて取得しなければならない
            try
            {
                imageUrl = webDriver
                           .FindElement(By.Id(Attributes.image.GetValue())).GetAttribute("src");
            }
            catch (NoSuchElementException)
            {
                imageUrl = webDriver
                           .FindElement(By.Id(Attributes.image2.GetValue())).GetAttribute("src");
            }

            string maker;
            // メーカー名を取得する
            // メーカー名は２つパターンがあるのでどちらかを取得
            try
            {
                maker = webDriver
                        .FindElement(By.Id(Attributes.maker2.GetValue())).Text;
            }
            catch (NoSuchElementException)
            {
                // 書籍などの場合はメーカー名が存在しないため、ここで例外をキャッチしmakerにnullを設定する
                try
                {
                    maker = webDriver
                            .FindElement(By.Id(Attributes.maker.GetValue())).Text;
                }
                catch (NoSuchElementException)
                {
                    maker = "null";
                }
            }

            string releaseDate;
            // 発売予定日が存在すればそれを取得
            try
            {
                releaseDate = webDriver
                              .FindElement(By.Id(Attributes.comingSoon.GetValue()))
                              .FindElement(By.TagName("span")).Text;
                // 取得した発売日が在庫あり等、発売日以外の情報かどうかチェックする
                if (releaseDate.Contains("年"))
                    releaseDate = Regex.Match(releaseDate, "[0-9]+年[0-9]+月[0-9]+日").Value;
                // 発売日以外の情報だったら他の手段を用いて、発売日を取得する
                else
                    throw new NoSuchElementException();
            }
            // 存在しなければAmazonの"新UI"取扱開始日を取得
            catch (NoSuchElementException)
            {
                try
                {
                    releaseDate = webDriver
                                  .FindElement(By.ClassName(Attributes.releaseDate.GetValue()))
                                  .FindElement(By.ClassName(Attributes.releaseDateDetail.GetValue())).Text;

                    Regex regex = new Regex("/");
                    releaseDate = regex.Replace(releaseDate, "年", 1);
                    releaseDate = regex.Replace(releaseDate, "月", 1) + "日";
                }
                // 新UIでなければAmazonの"旧UI"取扱開始日を取得
                catch (NoSuchElementException)
                {
                    releaseDate = webDriver
                                 .FindElements(By.ClassName(Attributes.content.GetValue()))[1]
                                 .FindElements(By.TagName("li"))[5]
                                 .Text;

                    releaseDate = releaseDate.Replace("発売日：", "");
                    Regex regex = new Regex("/");
                    releaseDate = regex.Replace(releaseDate, "年", 1);
                    releaseDate = regex.Replace(releaseDate, "月", 1) + "日";
                }
            }

            int price;
            try
            {
                string priceStr = webDriver
                                  .FindElement(By.Id(Attributes.price.GetValue())).Text;
                price = int.Parse(Regex.Match(priceStr, "[0-9,]+").Value.Replace(",", ""));
            }
            catch (NoSuchElementException)
            {
                price = 0;
            }

            product = new DetailProduct(
                ImageKey,
                productName,
                imageUrl,
                maker,
                releaseDate,
                price,
                productUrl);

            // webDriver.Dispose()と同義
            webDriver.Quit();
            return product;
        }

        public override string GetMostSimilarProductUrl(string searchText, IWebDriver webDriver)
        {
            try
            {
                // 特定の要素が出現するまで_timeout秒待つ
                var wait = new WebDriverWait(webDriver, _timeout);
                wait.Until(ExpectedConditions.ElementExists(By.Id(Attributes.result_.GetValue() + "0")));

                /* 検索結果に存在する商品名とURLを取得する(1Pageで最大31個存在する) */
                IWebElement[] resultElements = new IWebElement[31];
                string[] results;
                // 検索結果に商品が何個存在しているかカウントする
                int productCount = -1;

                try
                {
                    for (int i = 0; i < resultElements.Length; i++)
                    {
                        resultElements[productCount + 1] =
                            webDriver.FindElement(By.Id(Attributes.result_.GetValue() + (productCount + 1)));
                        productCount++;
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    results = new string[productCount];
                    for (int i = 0; i <= productCount; i++)
                    {
                        results[i] = resultElements[i].FindElements(By.TagName("a"))[1].GetAttribute("title");
                    }
                }

                Lucene lucene = new Lucene();
                // 検索文字に取得した商品名が一番近似している文字列を計算し、一番近似している商品のURLを取得する
                string retValue = resultElements[lucene.MostSimilarString(searchText, results)]
                    .FindElements(By.TagName("a"))[1].GetAttribute("href");

                return retValue;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
