namespace FigureSearch
{
    partial class ProductListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SimpleProductList_ListView = new System.Windows.Forms.ListView();
            this.ProductName_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProductUrl_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SimpleProductList_ListView
            // 
            this.SimpleProductList_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SimpleProductList_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProductName_ColumnHeader,
            this.ProductUrl_ColumnHeader});
            this.SimpleProductList_ListView.FullRowSelect = true;
            this.SimpleProductList_ListView.GridLines = true;
            this.SimpleProductList_ListView.Location = new System.Drawing.Point(13, 13);
            this.SimpleProductList_ListView.MultiSelect = false;
            this.SimpleProductList_ListView.Name = "SimpleProductList_ListView";
            this.SimpleProductList_ListView.Size = new System.Drawing.Size(866, 697);
            this.SimpleProductList_ListView.TabIndex = 0;
            this.SimpleProductList_ListView.UseCompatibleStateImageBehavior = false;
            this.SimpleProductList_ListView.View = System.Windows.Forms.View.Details;
            // 
            // ProductName_ColumnHeader
            // 
            this.ProductName_ColumnHeader.Text = "商品名";
            this.ProductName_ColumnHeader.Width = 424;
            // 
            // ProductUrl_ColumnHeader
            // 
            this.ProductUrl_ColumnHeader.Text = "URL";
            this.ProductUrl_ColumnHeader.Width = 247;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OK_Button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OK_Button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.OK_Button.FlatAppearance.BorderSize = 2;
            this.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OK_Button.Location = new System.Drawing.Point(13, 716);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(338, 30);
            this.OK_Button.TabIndex = 1;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = false;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.Cancel_Button.FlatAppearance.BorderSize = 2;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Location = new System.Drawing.Point(551, 716);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(328, 30);
            this.Cancel_Button.TabIndex = 2;
            this.Cancel_Button.Text = "キャンセル";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // ProductListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 752);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.SimpleProductList_ListView);
            this.Name = "ProductListForm";
            this.Text = "ProductList";
            this.Shown += new System.EventHandler(this.ProductListForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader ProductName_ColumnHeader;
        private System.Windows.Forms.ColumnHeader ProductUrl_ColumnHeader;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Button Cancel_Button;
        public System.Windows.Forms.ListView SimpleProductList_ListView;
    }
}