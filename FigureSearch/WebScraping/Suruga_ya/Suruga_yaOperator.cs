using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FigureSearch.Selenium;

namespace FigureSearch.WebScraping.Suruga_ya
{
    public class Suruga_yaOperator : WebOperatorBase
    {
        private const string TopPage = "https://www.suruga-ya.jp/";
        private const string ImageKey = "Suruga_ya";
        private readonly TimeSpan _timeout = new TimeSpan(0, 0, 15);
        
        public override IWebDriver GetSearchResultPage(string searchText, SeleniumBrowers.Name browser, bool headlessMode)
        {
            IWebDriver webDriver = SeleniumOperation.CreateInstance(browser, headlessMode);

            webDriver.Url = TopPage;

            // 検索フォームにカーソルを移す
            IWebElement searchElement = webDriver.FindElement(By.Id(Attributes.searchText.GetValue()));
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
                wait.Until(ExpectedConditions.ElementExists(By.ClassName(Attributes.title.GetValue())));

                // 検索結果に存在する商品名とURLを取得する(1Pageで最大24個存在する)
                var resultElements = webDriver.FindElements(By.ClassName(Attributes.title.GetValue()));
                SimpleProduct[] results = new SimpleProduct[resultElements.Count];

                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = new SimpleProduct(
                        resultElements[i].Text,
                        resultElements[i].FindElement(By.TagName("a")).GetAttribute("href"));
                }

                return results;
            }
            catch (NoSuchElementException)
            {
                return null;
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

            try
            {
                /* 検索結果から各情報を引き抜く */
                string productName;
                // 商品名を取得
                // 予約商品の場合、属性値が異なるので例外をキャッチし別属性で取得
                try
                {
                    productName = webDriver
                                  .FindElement(By.Id(Attributes.item_title.GetValue()))
                                  .Text;
                }
                catch (NoSuchElementException)
                {
                    productName = webDriver
                                  .FindElement(By.Id(Attributes.item_title_yoyaku.GetValue()))
                                  .Text;
                }

                string imageUrl = webDriver
                                  .FindElement(By.Id(Attributes.imagedetail.GetValue()))
                                  .GetAttribute("href");

                string maker = webDriver
                               .FindElements(By.ClassName(Attributes.t_contents.GetValue()))[3]
                               .Text;

                string releaseDate = webDriver
                                     .FindElements(By.ClassName(Attributes.t_contents.GetValue()))[1]
                                     .Text;

                int price;
                // 品切れの場合、Id:priceは存在しないので例外が発生する
                try
                {
                    string priceStr = webDriver
                                      .FindElement(By.Id(Attributes.price.GetValue()))
                                      .FindElement(By.ClassName(Attributes.red.GetValue()))
                                      .Text;
                    price = int.Parse(System.Text.RegularExpressions.Regex.Match(priceStr, "[0-9,]+").Value.Replace(",", ""));
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
            }
            catch (NoSuchElementException)
            {
                product = new DetailProduct(
                    ImageKey,
                    "検索がヒットしませんでした。",
                    "null",
                    "null",
                    "null",
                    0,
                    "null");
            }

            // webDriver.Dispose()と同義
            webDriver.Quit();
            return product;
        }

        public override string GetMostSimilarProductUrl(string searchText, IWebDriver webDriver)
        {
            try
            {
                // 検索結果に存在する商品名を取得する(1Pageで最大24個存在する)
                var resultElements = webDriver.FindElements(By.ClassName(Attributes.title.GetValue()));
                string[] results = new string[resultElements.Count];

                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = resultElements[i].Text;
                }

                Lucene lucene = new Lucene();
                // 検索文字に取得した商品名が一番近似している文字列を計算し、一番近似している商品のURLを取得する
                string retValue = resultElements[lucene.MostSimilarString(searchText, results)]
                    .FindElement(By.TagName("a")).GetAttribute("href");

                return retValue;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
