namespace FigureSearch.WebScraping.Suruga_ya
{
    //属性名
    public enum Attributes
    {
        searchText,        // id 検索文字を入れるフォーム
        title,             // class 商品名及び、商品番号
        item_title,        // id 商品名
        item_title_yoyaku, // id 予約商品の商品名
        imagedetail,       // id 商品画像のURL
        t_contents,        // class メーカー名や発売日など様々な値が同じクラス名で入っている
        price,             // id spanタグに価格または品切れが入っている
        red                // class 上記のspanタグの属性 価格または品切れが入っている
    }

    /// <summary>
    /// 拡張メソッド
    /// </summary>
    public static class AttributesExtend
    {
        /// <summary>
        /// 特定の属性の値を取得する
        /// </summary>
        /// <param name="attr">Attributes型の属性名</param>
        /// <returns>string型の属性値</returns>
        public static string GetValue(this Attributes attr)
        {
            // 属性の値
            string[] values = {
                "searchText",
                "title",
                "item_title",
                "item_title_yoyaku",
                "imagedetail",
                "t_contents",
                "price",
                "red"
            };

            return values[(int)attr];
        }
    }
}
