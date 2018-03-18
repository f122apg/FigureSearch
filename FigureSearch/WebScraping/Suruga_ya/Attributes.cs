namespace FigureSearch.WebScraping.Suruga_ya
{
	//属性名
	public enum Attributes
	{
		searchText,  // id 検索文字を入れるフォーム
		title,       // class 商品名及び、商品番号
        maker,       // class メーカー名、空白の時もある
        release_date,// class 発売日
        price        // class 価格または品切れの値
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
				"searchText",
				"title",
				"maker",
				"release_date",
				"price"
			};

			return Values[(int)Attr];
		}
	}
}
