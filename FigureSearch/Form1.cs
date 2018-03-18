using System;
using System.Data;
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
                XLWorkbook WorkBook = new XLWorkbook(@"D:\Documents\figures_excel\figures.xlsm");
                IXLWorksheet WorkSheet = WorkBook.Worksheet(1);

                // 全ての列が空白な行を探す
                int RowCount = 1;
                do
                {
                    if (WorkSheet.Row(RowCount).IsEmpty())
                        break;
                    else
                        RowCount++;
                } while (true);

                ListView Lv = (ListView)sender;

                // メーカー
                WorkSheet.Cell(RowCount, 1).Value = Lv.SelectedItems[0].SubItems[2].Text;
                // 画像
                WorkSheet.Cell(RowCount, 2).Value = Lv.SelectedItems[0].SubItems[1].Text;
                // 商品名
                WorkSheet.Cell(RowCount, 3).Value = Lv.SelectedItems[0].SubItems[3].Text;
                // 発売日
                WorkSheet.Cell(RowCount, 4).Value = Lv.SelectedItems[0].SubItems[4].Text;
                // 予約締切日
                WorkSheet.Cell(RowCount, 5).Value = Lv.SelectedItems[0].SubItems[4].Text;
                // 値段
                WorkSheet.Cell(RowCount, 6).Value = Lv.SelectedItems[0].SubItems[5].Tag;
                // 商品URL
                WorkSheet.Cell(RowCount, 7).Value = Lv.SelectedItems[0].SubItems[6].Text;

                WorkBook.Save();

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
			Product[] Data = new Product[3];

            // Amazon
            if (Amazon_CheckBox.Checked)
                Data[0] = Amazon.GetProductData(SeleniumBrowers.Name.Chrome, SearchText_TextBox.Text, HeadLess_CheckBox.Checked);

            // あみあみ
            if (Amiami_CheckBox.Checked)
                Data[1] = Amiami.GetProductData(SeleniumBrowers.Name.Chrome, SearchText_TextBox.Text, HeadLess_CheckBox.Checked);

            // 駿河屋
            if (Suruga_ya_CheckBox.Checked)
                Data[2] = Suruga_ya.GetProductData(SeleniumBrowers.Name.Chrome, SearchText_TextBox.Text, HeadLess_CheckBox.Checked);

            // 価格をキーとして昇順でソートしてListViewに追加していく
            Data.Where(p => p != null)              // nullチェック
                .OrderBy(p => p.Price)              // 価格をキーとして昇順でソート
                .ToList()                           // List化
                .ForEach(v => AddListViewItem(v));  // 値をListViewに追加

            Product_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            Notification_Button.Visible = false;
        }

		/// <summary>
		/// ListViewに行を追加する
		/// </summary>
		/// <param name="item">データを取得したProductクラス</param>
		private void AddListViewItem(Product Item)
		{
			ListViewItem LvItem = new ListViewItem();
            LvItem.ImageKey = Item.Site;
            LvItem.SubItems.Add(Item.ImageURL);
            LvItem.SubItems.Add(Item.Maker);
            LvItem.SubItems.Add(Item.ProductName);
            LvItem.SubItems.Add(Item.ReleaseDate);
            // Excelへ値段を書き込むときはTagに設定されている
            // 純粋な数値情報だけの価格を使う
            LvItem.SubItems.Add(Item.DisplayPrice()).Tag = Item.Price;
            LvItem.SubItems.Add(Item.ProductURL);

            Product_ListView.Items.Add(LvItem);
		}
    }
}
