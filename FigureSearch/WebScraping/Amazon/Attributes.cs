namespace FigureSearch.WebScraping.Amazon
{
    //属性名
    public enum Attributes
    {
        twotabsearchtextbox, // id 検索文字を入れるフォーム
        result_,             // id 検索結果で商品のリンク _の後に0から数字が連番で付く
        centerCol,           // id 商品ページの商品情報等
        image,               // id 画像のURL
        image2,              // id 画像のURL kindle ver
        price,               // id 価格または品切れの値
        maker,               // id メーカー Amazonによるとそのうち修正され、maker2のような値になるまでの措置
        maker2,              // id メーカー
        comingSoon,          // class 発売予定日
        releaseDate,         // class 新UI 発売日を含んだテキスト
        releaseDateDetail,   // class 新UI 発売日だけを抽出する属性
        content              // class 旧UI 発売日を含んだテキスト
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
                "twotabsearchtextbox",
                "result_",
                "centerCol",
                "landingImage",
                "imgBlkFront",
                "priceblock_ourprice",
                "brand",
                "bylineInfo",
                "availability",
                "date-first-available",
                "value",
                "content"
            };

            return values[(int)attr];
        }
    }
}
