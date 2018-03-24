namespace FigureSearch.WebScraping
{
    public class SimpleProduct
    {
        // プロパティ
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }

        // コンストラクタ
        public SimpleProduct(string productName, string productUrl)
        {
            ProductName = productName;

            if (System.Text.RegularExpressions.Regex.IsMatch(productUrl, "^https?://.*"))
                ProductUrl = productUrl;
            else
                throw new System.ArgumentException("ProductURLはhttpから始まる値を指定してください。\n値:" + productUrl);
        }
    }
}
