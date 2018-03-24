using System;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using FigureSearch.WebScraping;
using FigureSearch.WebScraping.Suruga_ya;
using FigureSearch.WebScraping.Amiami;
using FigureSearch.WebScraping.Amazon;

namespace FigureSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Product_ListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // 依存関係が強い処理を行う
                // 列変更には対応しない
                XLWorkbook workBook = new XLWorkbook(@"D:\Documents\figures_excel\figures.xlsm");
                IXLWorksheet workSheet = workBook.Worksheet(1);

                // 全ての列が空白な行を探す
                int rowCount = 1;
                do
                {
                    if (workSheet.Row(rowCount).IsEmpty())
                        break;
                    else
                        rowCount++;
                } while (true);

                ListView lv = (ListView)sender;

                // メーカー
                workSheet.Cell(rowCount, 1).Value = lv.SelectedItems[0].SubItems[2].Text;
                // 画像
                workSheet.Cell(rowCount, 2).Value = lv.SelectedItems[0].SubItems[1].Text;
                // 商品名
                workSheet.Cell(rowCount, 3).Value = lv.SelectedItems[0].SubItems[3].Text;
                // 発売日
                workSheet.Cell(rowCount, 4).Value = lv.SelectedItems[0].SubItems[4].Text;
                // 予約締切日
                workSheet.Cell(rowCount, 5).Value = lv.SelectedItems[0].SubItems[4].Text;
                // 値段
                workSheet.Cell(rowCount, 6).Value = lv.SelectedItems[0].SubItems[5].Tag;
                // 商品URL
                workSheet.Cell(rowCount, 7).Value = lv.SelectedItems[0].SubItems[6].Text;

                workBook.Save();

                MessageBox.Show("選択されたアイテムをExcelへ保存しました。", "処理終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("figures.xlsmを閉じてから項目を保存してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchText_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                Search_Button_Click(this, new EventArgs());
        }

        private void Search_Button_Click(object sender, EventArgs e)
        {
            Notification_Button.Visible = true;

            Product_ListView.Items.Clear();
            DetailProduct[] data = new DetailProduct[3];

            if (Suruga_ya_CheckBox.Checked)
            {
                Suruga_yaOperator surugaya = new Suruga_yaOperator();
                if (SimilarMode_CheckBox.Checked)
                    data[0] = surugaya.OneShotGetProductFromSimilarProduct(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
                else
                    data[0] = surugaya.OneShotGetProductFromList(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
            }

            if (Amiami_CheckBox.Checked)
            {
                AmiamiOperator amiami = new AmiamiOperator();
                if (SimilarMode_CheckBox.Checked)
                    data[1] = amiami.OneShotGetProductFromSimilarProduct(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
                else
                    data[1] = amiami.OneShotGetProductFromList(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
            }

            if (Amazon_CheckBox.Checked)
            {
                AmazonOperator amazon = new AmazonOperator();
                if (SimilarMode_CheckBox.Checked)
                    data[2] = amazon.OneShotGetProductFromSimilarProduct(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
                else
                    data[2] = amazon.OneShotGetProductFromList(SearchText_TextBox.Text,
                        Selenium.SeleniumBrowers.Name.Chrome, HeadlessMode_CheckBox.Checked);
            }

            // 価格をキーとして昇順でソートしてListViewに追加していく
            data.Where(p => p != null)             // nullチェック
                .OrderBy(p => p.Price)             // 価格をキーとして昇順でソート
                .ToList()                          // List化
                .ForEach(v => AddListViewItem(v)); // 値をListViewに追加

            Product_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            Notification_Button.Visible = false;
        }

        /// <summary>
        /// ListViewに行を追加する
        /// </summary>
        /// <param name="item">データを取得したProductクラス</param>
        private void AddListViewItem(DetailProduct item)
        {
            if (item != null)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.ImageKey = item.Site;
                lvItem.SubItems.Add(item.ImageUrl);
                lvItem.SubItems.Add(item.Maker);
                lvItem.SubItems.Add(item.ProductName);
                lvItem.SubItems.Add(item.ReleaseDate);
                // Excelへ値段を書き込むときはTagに設定されている
                // 純粋な数値情報だけの価格を使う
                lvItem.SubItems.Add(item.DisplayPrice()).Tag = item.Price;
                lvItem.SubItems.Add(item.ProductUrl);

                Product_ListView.Items.Add(lvItem);
            }
        }
    }
}
