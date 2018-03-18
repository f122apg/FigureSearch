using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureSearch.WebScraping
{
	public class Product
	{
		// メンバ
		private string mSite = "";       // 検索したサイト
		private string mProductName = "";// 商品名
		private string mImageURL = "";   // m付きでない(フルサイズの)画像のURL
		private string mMaker = "";      // メーカー名、空白の時もある
		private string mReleaseDate = "";// 発売日または発売予定日
		private int mPrice = 0;          // 価格または品切れ
        private string mProductURL = ""; // 商品のURL

        // プロパティ
        public string Site { get => mSite; set => mSite = value; }
        public string ProductName { get => mProductName; set => mProductName = value; }
        public string ImageURL { get => mImageURL; set => mImageURL = value; }
        public string Maker { get => mMaker; set => mMaker = value; }
        public string ReleaseDate { get => mReleaseDate; set => mReleaseDate = value; }
        public int Price { get => mPrice; set => mPrice = value; }
        public string ProductURL { get => mProductURL; set => mProductURL = value; }

        // コンストラクタ
        public Product(string site, string productName, string imageURL, 
			string maker, string releaseDate, int price, string productURL)
		{
			mSite = site;
			mProductName = productName;

			if (System.Text.RegularExpressions.Regex.IsMatch(imageURL, "^https?://.*") 
                || imageURL == "null")
				mImageURL = imageURL;
			else
				throw new ArgumentException("ImageURLはhttpから始まる値を指定してください。\n値:" + imageURL);

			mMaker = maker;
			mReleaseDate = releaseDate;
			mPrice = price;
            mProductURL = productURL;
        }

        public string DisplayPrice()
        {
            return Price.ToString("#,0") + " 円";
        }
	}
}
