using OpenQA.Selenium;

namespace FigureSearch.WebScraping.Suruga_ya
{
	public static class Suruga_ya
	{
		private static string mTopPage = "https://www.suruga-ya.jp/";
		private static string mReplaceString = "REPLACE";
		private static string mTempleteImageURL = "https://www.suruga-ya.jp/database/pics/game/REPLACE.jpg";
		private const string ImageKey = "Suruga_ya";

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
                    /* 検索結果から各情報を引き抜く */
                    // クラスTitleには商品名と商品番号が二つ記載されているので、分割するためにここだけIWebElementにする
                    IWebElement Title = WebDriver
                        .FindElement(By.ClassName(Attributes.title.GetValue()));
                    string ProductName = Title.Text;
                    
                    // Titleから子孫タグ"a"を取得し、"href"に存在する商品番号を取得
                    string ProductNumberFull = Title
                        .FindElement(By.TagName("a"))
                        .GetAttribute("href");
                    string ProductNumber = ProductNumberFull.Substring(ProductNumberFull.LastIndexOf('/') + 1, 9);

                    string ImageURL = mTempleteImageURL.Replace(mReplaceString, ProductNumber);

                    string Maker = WebDriver
                        .FindElement(By.ClassName(Attributes.maker.GetValue())).Text;

                    string ReleaseDate = WebDriver
                        .FindElement(By.ClassName(Attributes.release_date.GetValue())).Text;
                    ReleaseDate = ReleaseDate.Replace("発売日：", "");

                    string PriceStr = WebDriver
                        .FindElement(By.ClassName(Attributes.price.GetValue())).Text;
                    int Price = int.Parse(System.Text.RegularExpressions.Regex.Match(PriceStr, "[0-9,]+").Value.Replace(",", ""));

                    string ProductURL = ProductNumberFull;

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
