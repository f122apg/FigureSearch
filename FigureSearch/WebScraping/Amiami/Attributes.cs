namespace FigureSearch.WebScraping.Amiami
{
    //属性名
    public enum Attributes
    {
        s_keywords,        // id 検索文字を入れるフォーム
        product_box,       // class 検索結果にある商品の様々な情報
        product_name_list, // class 検索結果で商品のリンク
        title,             // class 商品名及び、商品番号
        product_img_area,  // class 画像のURL
        price,             // class 価格または品切れの値
        spec_data          // class 発売日、ブランド名等まとめた欄
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
                "s_keywords",
                "product_box",
                "product_name_list",
                "heading_10",
                "product_img_area",
                "price",
                "spec_data"
            };

            return Values[(int)Attr];
        }
    }
}
