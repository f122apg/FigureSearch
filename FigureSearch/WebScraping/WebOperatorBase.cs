using System.Windows.Forms;
using OpenQA.Selenium;

namespace FigureSearch.WebScraping
{
    public abstract class WebOperatorBase
    {
        /// <summary>
        /// 検索ワードで検索し、検索結果をブラウザのコントローラ(<seealso cref="IWebDriver"/>)で返す
        /// </summary>
        /// <param name="searchText">検索ワード</param>
        /// <param name="browser">何のブラウザで起動するか</param>
        /// <param name="headlessMode">ブラウザを非表示状態で起動するかどうか</param>
        /// <returns>ブラウザのコントローラ(<seealso cref="IWebDriver"/>)</returns>
        /// <exception cref="NoSuchElementException">検索結果が0だった場合のみ発生</exception>
        public abstract IWebDriver GetSearchResultPage(string searchText, Selenium.SeleniumBrowers.Name browser, bool headlessMode);

        /// <summary>
        /// ブラウザのコントローラ(<seealso cref="IWebDriver"/>)から簡易的な商品情報(<seealso cref="SimpleProduct"/>)を抜き出す
        /// </summary>
        /// <param name="webDriver">ブラウザのコントローラ(<seealso cref="IWebDriver"/>)</param>
        /// <returns>簡易的な商品情報(<seealso cref="SimpleProduct"/>)が入った配列</returns>
        public abstract SimpleProduct[] GetProductDataList(IWebDriver webDriver);

        /// <summary>
        /// 指定されたURLから詳細な商品情報(<seealso cref="DetailProduct"/>)を取得する
        /// </summary>
        /// <param name="webDriver">ブラウザのコントローラ(<seealso cref="IWebDriver"/>)</param>
        /// <param name="productUrl">商品のURL</param>
        /// <returns>詳細な商品情報(<seealso cref="DetailProduct"/>)</returns>
        public abstract DetailProduct GetOneProductData(IWebDriver webDriver, string productUrl);

        /// <summary>
        /// 検索結果をリスト化してフォームで表示し、選択した商品のURLを取得して返す
        /// </summary>
        /// <param name="webDriver">ブラウザのコントローラ(<seealso cref="IWebDriver"/>)</param>
        /// <returns>選択された商品のURL</returns>
        public string SelectProductList(IWebDriver webDriver)
        {
            // 多数の商品情報を取得
            SimpleProduct[] simpleProducts = GetProductDataList(webDriver);

            if (simpleProducts != null)
            {
                ListViewItem[] listViewItems = new ListViewItem[simpleProducts.Length];

                // SimpleProductをListViewItem化
                for (int i = 0; i < simpleProducts.Length; i++)
                {
                    listViewItems[i]      = new ListViewItem();
                    listViewItems[i].Text = simpleProducts[i].ProductName;
                    listViewItems[i].SubItems.Add(simpleProducts[i].ProductUrl);
                }

                using (var productListFrom = new ProductListForm())
                {
                    // フォームのListViewに商品情報の項目を追加する
                    productListFrom.SimpleProductList_ListView.Items.AddRange(listViewItems);
                    if (productListFrom.ShowDialog() == DialogResult.OK)
                        return productListFrom.ProductUrl;
                }
            }

            return null;
        }

        /// <summary>
        /// 検索文字にもっとも近似している商品名のURLを取得する
        /// </summary>
        /// <param name="searchText">検索文字</param>
        /// <param name="webDriver">ブラウザのコントローラ(<seealso cref="IWebDriver"/>)</param>
        /// <returns>商品のURL</returns>
        public abstract string GetMostSimilarProductUrl(string searchText, IWebDriver webDriver);

        /// <summary>
        /// 検索結果から商品の選択、商品情報を持ってくる処理を一発で行う
        /// </summary>
        /// <param name="searchText">検索ワード</param>
        /// <param name="browser">何のブラウザで起動するか</param>
        /// <param name="headlessMode">ブラウザを非表示状態で起動するかどうか</param>
        /// <returns></returns>
        public DetailProduct OneShotGetProductFromList(string searchText, Selenium.SeleniumBrowers.Name browser, bool headlessMode)
        {
            // 検索文字で検索する
            IWebDriver webDriver = GetSearchResultPage(searchText, browser, headlessMode);
            // 検索結果の全ての商品を取得しリストで表示してユーザーに商品を選択してもらう
            string productUrl = SelectProductList(webDriver);
            // 選択された商品の詳細情報を取得
            return GetOneProductData(webDriver, productUrl);
        }

        /// <summary>
        /// 検索結果から検索文字に一番近似している商品の情報を持ってくる処理を一発で行う
        /// </summary>
        /// <param name="searchText">検索ワード</param>
        /// <param name="browser">何のブラウザで起動するか</param>
        /// <param name="headlessMode">ブラウザを非表示状態で起動するかどうか</param>
        /// <returns></returns>
        public DetailProduct OneShotGetProductFromSimilarProduct(string searchText, Selenium.SeleniumBrowers.Name browser, bool headlessMode)
        {
            // 検索文字で検索する
            IWebDriver webDriver = GetSearchResultPage(searchText, browser, headlessMode);
            // 検索結果から検索文字に一番近似している商品は何か計算しURLを取得する
            string productUrl = GetMostSimilarProductUrl(searchText, webDriver);
            // 取得したURLから商品の詳細情報を取得
            return GetOneProductData(webDriver, productUrl);
        }
    }
}