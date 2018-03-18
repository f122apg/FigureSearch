using OpenQA.Selenium;
using System.Linq;
using System.Windows.Forms;

namespace FigureSearch.WebScraping.Amiami
{
    public static class Amiami
    {
        private static string mTopPage = "http://amiami.jp/";
        private const string ImageKey = "Amiami";

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
                    // 検索結果の一番目の商品のリンクをクリックし、ページを遷移する
                    // ヘッドレスモードだとWebDriver#Click()が効かなかったので
                    // SendKeys(Keys.Enter)でページを遷移するようにした
                    WebDriver.FindElement(By.ClassName(Attributes.product_name_list.GetValue()))
                        .FindElement(By.TagName("a"))
                        .SendKeys(OpenQA.Selenium.Keys.Enter);

                    /* 検索結果から各情報を引き抜く */
                    string ProductName = WebDriver
                        .FindElement(By.ClassName(Attributes.title.GetValue())).Text;

                    string ImageURL = WebDriver
                        .FindElement(By.ClassName(Attributes.product_img_area.GetValue()))
                        .FindElement(By.TagName("img")).GetAttribute("src");

                    var SpecDataElements = WebDriver
                        .FindElement(By.ClassName(Attributes.spec_data.GetValue()))
                        .FindElements(By.TagName("dd"));

                    // spec_dataという属性値にメーカー名や価格などが
                    // ddというタグで一緒くたに挿入されているので、挿入順に沿って取得する
                    // なお商品のジャンル(フィギュアやCD等)によって
                    // ddタグの位置などが違うため、フィギュアのみの検索を対象とする
                    string Maker = SpecDataElements
                            .Skip(4)
                            .First()
                            .Text;

                    string ReleaseDate = SpecDataElements
                        .Skip(3)
                        .First()
                        .Text;

                    string PriceStr = WebDriver
                        .FindElement(By.ClassName(Attributes.price.GetValue())).Text;

                    int Price = int.Parse(System.Text.RegularExpressions.Regex.Match(PriceStr, "[0-9,]+円").Value.Replace(",", "").Replace("円", ""));
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
