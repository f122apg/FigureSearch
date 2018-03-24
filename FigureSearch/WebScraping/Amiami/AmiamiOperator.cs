using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FigureSearch.Selenium;

namespace FigureSearch.WebScraping.Amiami
{
    public class AmiamiOperator : WebOperatorBase
    {
        private const string TopPage = "http://amiami.jp/";
        private const string ImageKey = "Amiami";
        private readonly TimeSpan _timeout = new TimeSpan(0, 0, 15);

        public override IWebDriver GetSearchResultPage(string searchText, SeleniumBrowers.Name browser,
            bool headlessMode)
        {
            IWebDriver webDriver = SeleniumOperation.CreateInstance(browser, headlessMode);

            webDriver.Url = TopPage;

            // 検索フォームにカーソルを移す
            IWebElement searchElement = webDriver.FindElement(By.Id(Attributes.s_keywords.GetValue()));
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
                wait.Until(ExpectedConditions.ElementExists(By.ClassName(Attributes.product_box.GetValue())));

                // 検索結果に存在する商品名とURLを取得する(1Pageで最大40個存在する)
                var resultElements = webDriver.FindElements(By.ClassName(Attributes.product_box.GetValue()));
                SimpleProduct[] results = new SimpleProduct[resultElements.Count];

                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = new SimpleProduct(
                        resultElements[i].FindElement(By.ClassName(Attributes.product_name_list.GetValue()))
                                         .Text,
                        resultElements[i].FindElement(By.ClassName(Attributes.product_name_list.GetValue()))
                                         .FindElement(By.TagName("a"))
                                         .GetAttribute("href"));
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
                string productName = webDriver
                    .FindElement(By.ClassName(Attributes.title.GetValue())).Text;

                string imageUrl = webDriver
                    .FindElement(By.ClassName(Attributes.product_img_area.GetValue()))
                    .FindElement(By.TagName("img")).GetAttribute("src");

                var specDataElements = webDriver
                    .FindElement(By.ClassName(Attributes.spec_data.GetValue()))
                    .FindElements(By.TagName("dd"));

                // spec_dataという属性値にメーカー名や価格などが
                // ddというタグで一緒くたに挿入されているので、挿入順に沿って取得する
                // なお商品のジャンル(フィギュアやCD等)によって
                // ddタグの位置などが違うため、フィギュアのみの検索を対象とする
                string maker = specDataElements
                        .Skip(4)
                        .First()
                        .Text;

                string releaseDate = specDataElements
                    .Skip(3)
                    .First()
                    .Text;

                string priceStr = webDriver
                    .FindElement(By.ClassName(Attributes.price.GetValue())).Text;

                int price = int.Parse(System.Text.RegularExpressions.Regex.Match(priceStr, "[0-9,]+円").Value.Replace(",", "").Replace("円", ""));

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
                // 検索結果に存在する商品名を取得する(1Pageで最大40個存在する)
                var resultElements = webDriver.FindElements(By.ClassName(Attributes.product_box.GetValue()));
                string[] results = new string[resultElements.Count];

                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = resultElements[i].FindElement(By.ClassName(Attributes.product_name_list.GetValue()))
                                                  .Text;
                }

                Lucene lucene = new Lucene();
                // 検索文字に取得した商品名が一番近似している文字列を計算し、一番近似している商品のURLを取得する
                string retValue = resultElements[lucene.MostSimilarString(searchText, results)]
                    .FindElement(By.TagName("a"))
                    .GetAttribute("href");

                return retValue;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}