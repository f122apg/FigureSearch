namespace FigureSearch.WebScraping.Amazon
{
	//属性名
	public enum Attributes
	{
        searchText,        // id 検索文字を入れるフォーム
        productlink,       // id 検索結果で商品のリンク
        centerCol,         // id 商品ページの商品情報等
        image,             // id 画像のURL
        price,             // id 価格または品切れの値
        maker,             // id メーカー Amazonによるとそのうち修正され、maker2のような値になるまでの措置
        maker2,            // id メーカー
        comingSoon,        // class 発売予定日
        releaseDate,       // class 発売日を含んだテキスト
        releaseDateDetail  // class 発売日だけを抽出する属性
	}

	/// <summary>
	/// 拡張メソッド
	/// </summary>
	public static class AttributesExtend
	{
		/// <summary>
		/// 特定の属性の値を取得する
		/// </summary>
		/// <param name="Attr">Attributes型の属性名</param>
		/// <returns>string型の属性値</returns>
		public static string GetValue(this Attributes Attr)
		{
			// 属性の値
			string[] Values = {
                "twotabsearchtextbox",
                "result_0",
                "centerCol",
                "landingImage",
                "priceblock_ourprice",
                "brand",
                "bylineInfo",
                "availability",
                "date-first-available",
                "value"
            };

			return Values[(int)Attr];
		}
	}
}
