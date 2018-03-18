using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace FigureSearch.WebScraping.Amazon
{
    public static class Amazon
    {
        private static string mTopPage = "https://www.amazon.co.jp/";
        private const string ImageKey = "Amazon";

        public static Product GetProductData(SeleniumBrowers.Name Browser, string SearchWord, bool HeadLess)
        {
            using (IWebDriver WebDriver = SeleniumOperation.CreateInstance(Browser, HeadLess))
            {
                WebDriver.Url = mTopPage;

                // 検索フォームにカーソルを移す
                IWebElement SearchElement = WebDriver.FindElement(By.Id(Attributes.searchText.GetValue()));
                // 検索ワードを入力して検索実行
                SearchElement.SendKeys(SearchWord);
                SearchElement.Submit();
                Product product;

                // 検索結果が"0件"ならば、NoSuchElementExceptionが発生する
                try
                {
                    WebDriver.Navigate().GoToUrl(
                        WebDriver
                            .FindElement(By.Id(Attributes.productlink.GetValue()))
                            .FindElement(By.TagName("div"))
                            .FindElements(By.TagName("div"))[8]
                            .FindElement(By.TagName("a"))
                            .GetAttribute("href")
                        );

                    // 検索結果が出るまでの時間を稼ぐ
                    System.Threading.Thread.Sleep(3000);
                    /* 検索結果から各情報を引き抜く */
                    string ProductName = WebDriver
                        .FindElement(By.Id(Attributes.centerCol.GetValue()))
                        .FindElement(By.TagName("span")).Text;

                    string ImageURL = WebDriver
                        .FindElement(By.Id(Attributes.image.GetValue())).GetAttribute("src");

                    string Maker = "";
                    // メーカー名を取得する
                    // メーカー名は２つパターンがあるのでどちらかを取得
                    try
                    {
                        Maker = WebDriver
                            .FindElement(By.Id(Attributes.maker2.GetValue())).Text;
                    }
                    catch (NoSuchElementException)
                    {
                        Maker = WebDriver
                            .FindElement(By.Id(Attributes.maker.GetValue())).Text;
                    }

                    string ReleaseDate = "";
                    // 発売予定日が存在すればそれを取得
                    try
                    {
                        ReleaseDate = WebDriver.FindElement(By.Id(Attributes.comingSoon.GetValue()))
                            .FindElement(By.TagName("span")).Text;
                        ReleaseDate = Regex.Match(ReleaseDate, "[0-9]+年[0-9]+月[0-9]+日").Value;
                    }

                    // 存在しなければAmazonの"新UI"取扱開始日を取得
                    catch (NoSuchElementException)
                    {
                        try
                        {
                            ReleaseDate = WebDriver
                                .FindElement(By.ClassName(Attributes.releaseDate.GetValue()))
                                .FindElement(By.ClassName(Attributes.releaseDateDetail.GetValue())).Text;

                            Regex regex = new Regex("/");
                            ReleaseDate = regex.Replace(ReleaseDate, "年", 1);
                            ReleaseDate = regex.Replace(ReleaseDate, "月", 1) + "日";
                        }
                        // 新UIでなければAmazonの"旧UI"取扱開始日を取得
                        // 旧UIを採用した商品ページがなかなか見つからないのでとりあえずnullとしておく
                        catch (NoSuchElementException)
                        {
                            ReleaseDate = "null";
                        }
                    }
                    
                    int Price = 0;

                    try
                    {
                        string PriceStr = WebDriver
                            .FindElement(By.Id(Attributes.price.GetValue())).Text;
                        Price = int.Parse(Regex.Match(PriceStr, "[0-9,]+").Value.Replace(",", ""));
                    }
                    catch (NoSuchElementException)
                    {
                        Price = 0;
                    }

                    string ProductURL = WebDriver.Url;

                    product = new Product(
                        ImageKey,
                        ProductName,
                        ImageURL,
                        Maker,
                        ReleaseDate,
                        Price,
                        ProductURL);
                }
                catch (NoSuchElementException)
                {
                    product = new Product(
                        ImageKey,
                        "検索がヒットしませんでした。",
                        "null",
                        "null",
                        "null",
                        0,
                        "null");
                }

                WebDriver.Quit();
                return product;
            }
        }
    }
}
