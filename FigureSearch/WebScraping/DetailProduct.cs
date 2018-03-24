namespace FigureSearch.WebScraping
{
    public class DetailProduct
    {
        // プロパティ
        public string Site { get; set; }        // 検索したサイト
        public string ProductName { get; set; } // 商品名
        public string ImageUrl { get; set; }    // フルサイズの画像のURL
        public string Maker { get; set; }       // メーカー名、空白の時もある
        public string ReleaseDate { get; set; } // 発売日または発売予定日
        public int    Price { get; set; }       // 価格または品切れ
        public string ProductUrl { get; set; }  // 商品のURL

        // コンストラクタ
        public DetailProduct(string site, string productName, string imageUrl,
            string maker, string releaseDate, int price, string productUrl)
        {
            Site = site;
            ProductName = productName;

            if (System.Text.RegularExpressions.Regex.IsMatch(imageUrl, "^https?://.*")
                || imageUrl == "null")
                ImageUrl = imageUrl;
            else
                throw new System.ArgumentException("ImageURLはhttpから始まる値を指定してください。\n値:" + imageUrl);

            Maker = maker;
            ReleaseDate = releaseDate;
            Price = price;
            ProductUrl = productUrl;
        }

        public string DisplayPrice()
        {
            return Price.ToString("#,0") + " 円";
        }
    }
}
