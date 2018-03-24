using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FigureSearch
{
    public partial class ProductListForm : Form
    {
        public string ProductUrl { get; set; }

        public ProductListForm()
        {
            InitializeComponent();
        }

        private void ProductListForm_Shown(object sender, EventArgs e)
        {
            SimpleProductList_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.Activate();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            var selectedItem = SimpleProductList_ListView.SelectedItems;

            if (selectedItem.Count == 1)
            {
                ProductUrl = selectedItem[0].SubItems[1].Text;
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("リストから商品を１つ選択してください！", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
