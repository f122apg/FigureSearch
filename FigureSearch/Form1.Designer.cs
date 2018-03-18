namespace FigureSearch
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.SearchText_TextBox = new System.Windows.Forms.TextBox();
            this.Search_Button = new System.Windows.Forms.Button();
            this.Product_ListView = new System.Windows.Forms.ListView();
            this.Site_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Image_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Maker_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProductName_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReleaseDate_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Favicons_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.Amazon_CheckBox = new System.Windows.Forms.CheckBox();
            this.Suruga_ya_CheckBox = new System.Windows.Forms.CheckBox();
            this.Amiami_CheckBox = new System.Windows.Forms.CheckBox();
            this.Notification_Button = new System.Windows.Forms.Button();
            this.HeadLess_CheckBox = new System.Windows.Forms.CheckBox();
            this.ProductURL_ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "検索ワード：";
            // 
            // SearchText_TextBox
            // 
            this.SearchText_TextBox.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.SearchText_TextBox.Location = new System.Drawing.Point(125, 12);
            this.SearchText_TextBox.Name = "SearchText_TextBox";
            this.SearchText_TextBox.Size = new System.Drawing.Size(725, 29);
            this.SearchText_TextBox.TabIndex = 1;
            this.SearchText_TextBox.WordWrap = false;
            this.SearchText_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchText_TextBox_KeyDown);
            // 
            // Search_Button
            // 
            this.Search_Button.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.Search_Button.Location = new System.Drawing.Point(866, 10);
            this.Search_Button.Name = "Search_Button";
            this.Search_Button.Size = new System.Drawing.Size(126, 36);
            this.Search_Button.TabIndex = 2;
            this.Search_Button.Text = "検索";
            this.Search_Button.UseVisualStyleBackColor = true;
            this.Search_Button.Click += new System.EventHandler(this.Search_Button_Click);
            // 
            // Product_ListView
            // 
            this.Product_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Product_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Site_ColumnHeader,
            this.Image_ColumnHeader,
            this.Maker_ColumnHeader,
            this.ProductName_ColumnHeader,
            this.ReleaseDate_ColumnHeader,
            this.Price_ColumnHeader,
            this.ProductURL_ColumnHeader});
            this.Product_ListView.FullRowSelect = true;
            this.Product_ListView.GridLines = true;
            this.Product_ListView.Location = new System.Drawing.Point(13, 88);
            this.Product_ListView.MultiSelect = false;
            this.Product_ListView.Name = "Product_ListView";
            this.Product_ListView.Size = new System.Drawing.Size(1339, 543);
            this.Product_ListView.SmallImageList = this.Favicons_ImageList;
            this.Product_ListView.TabIndex = 3;
            this.Product_ListView.UseCompatibleStateImageBehavior = false;
            this.Product_ListView.View = System.Windows.Forms.View.Details;
            this.Product_ListView.DoubleClick += new System.EventHandler(this.Product_ListView_DoubleClick);
            // 
            // Site_ColumnHeader
            // 
            this.Site_ColumnHeader.Tag = "";
            this.Site_ColumnHeader.Text = "";
            this.Site_ColumnHeader.Width = 48;
            // 
            // Image_ColumnHeader
            // 
            this.Image_ColumnHeader.Text = "画像";
            this.Image_ColumnHeader.Width = 100;
            // 
            // Maker_ColumnHeader
            // 
            this.Maker_ColumnHeader.Tag = "";
            this.Maker_ColumnHeader.Text = "メーカー";
            this.Maker_ColumnHeader.Width = 114;
            // 
            // ProductName_ColumnHeader
            // 
            this.ProductName_ColumnHeader.Tag = "";
            this.ProductName_ColumnHeader.Text = "商品名";
            this.ProductName_ColumnHeader.Width = 406;
            // 
            // ReleaseDate_ColumnHeader
            // 
            this.ReleaseDate_ColumnHeader.Tag = "";
            this.ReleaseDate_ColumnHeader.Text = "発売日";
            this.ReleaseDate_ColumnHeader.Width = 94;
            // 
            // Price_ColumnHeader
            // 
            this.Price_ColumnHeader.Tag = "";
            this.Price_ColumnHeader.Text = "価格";
            this.Price_ColumnHeader.Width = 89;
            // 
            // Favicons_ImageList
            // 
            this.Favicons_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Favicons_ImageList.ImageStream")));
            this.Favicons_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Favicons_ImageList.Images.SetKeyName(0, "Suruga_ya");
            this.Favicons_ImageList.Images.SetKeyName(1, "Amiami");
            this.Favicons_ImageList.Images.SetKeyName(2, "Amazon");
            // 
            // Amazon_CheckBox
            // 
            this.Amazon_CheckBox.AutoSize = true;
            this.Amazon_CheckBox.Checked = true;
            this.Amazon_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Amazon_CheckBox.Font = new System.Drawing.Font("MS UI Gothic", 15F);
            this.Amazon_CheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Amazon_CheckBox.ImageKey = "Amazon";
            this.Amazon_CheckBox.ImageList = this.Favicons_ImageList;
            this.Amazon_CheckBox.Location = new System.Drawing.Point(7, 53);
            this.Amazon_CheckBox.Name = "Amazon_CheckBox";
            this.Amazon_CheckBox.Size = new System.Drawing.Size(133, 29);
            this.Amazon_CheckBox.TabIndex = 4;
            this.Amazon_CheckBox.Text = "Amazon";
            this.Amazon_CheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Amazon_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Suruga_ya_CheckBox
            // 
            this.Suruga_ya_CheckBox.AutoSize = true;
            this.Suruga_ya_CheckBox.Checked = true;
            this.Suruga_ya_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Suruga_ya_CheckBox.Font = new System.Drawing.Font("MS UI Gothic", 15F);
            this.Suruga_ya_CheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Suruga_ya_CheckBox.ImageKey = "Suruga_ya";
            this.Suruga_ya_CheckBox.ImageList = this.Favicons_ImageList;
            this.Suruga_ya_CheckBox.Location = new System.Drawing.Point(146, 53);
            this.Suruga_ya_CheckBox.Name = "Suruga_ya_CheckBox";
            this.Suruga_ya_CheckBox.Size = new System.Drawing.Size(125, 29);
            this.Suruga_ya_CheckBox.TabIndex = 5;
            this.Suruga_ya_CheckBox.Text = "駿河屋";
            this.Suruga_ya_CheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Suruga_ya_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Amiami_CheckBox
            // 
            this.Amiami_CheckBox.AutoSize = true;
            this.Amiami_CheckBox.Checked = true;
            this.Amiami_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Amiami_CheckBox.Font = new System.Drawing.Font("MS UI Gothic", 15F);
            this.Amiami_CheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Amiami_CheckBox.ImageKey = "Amiami";
            this.Amiami_CheckBox.ImageList = this.Favicons_ImageList;
            this.Amiami_CheckBox.Location = new System.Drawing.Point(277, 53);
            this.Amiami_CheckBox.Name = "Amiami_CheckBox";
            this.Amiami_CheckBox.Size = new System.Drawing.Size(134, 29);
            this.Amiami_CheckBox.TabIndex = 6;
            this.Amiami_CheckBox.Text = "あみあみ";
            this.Amiami_CheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Amiami_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Notification_Button
            // 
            this.Notification_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Notification_Button.Font = new System.Drawing.Font("MS UI Gothic", 105F);
            this.Notification_Button.ForeColor = System.Drawing.Color.Red;
            this.Notification_Button.Location = new System.Drawing.Point(13, 119);
            this.Notification_Button.Name = "Notification_Button";
            this.Notification_Button.Size = new System.Drawing.Size(1339, 520);
            this.Notification_Button.TabIndex = 7;
            this.Notification_Button.Text = "商品情報取得中";
            this.Notification_Button.UseVisualStyleBackColor = true;
            this.Notification_Button.Visible = false;
            // 
            // HeadLess_CheckBox
            // 
            this.HeadLess_CheckBox.AutoSize = true;
            this.HeadLess_CheckBox.Checked = true;
            this.HeadLess_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HeadLess_CheckBox.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.HeadLess_CheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HeadLess_CheckBox.ImageKey = "(なし)";
            this.HeadLess_CheckBox.Location = new System.Drawing.Point(998, 17);
            this.HeadLess_CheckBox.Name = "HeadLess_CheckBox";
            this.HeadLess_CheckBox.Size = new System.Drawing.Size(328, 26);
            this.HeadLess_CheckBox.TabIndex = 8;
            this.HeadLess_CheckBox.Text = "ヘッドレス(ブラウザの非表示)モード";
            this.HeadLess_CheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.HeadLess_CheckBox.UseVisualStyleBackColor = true;
            // 
            // ProductURL_ColumnHeader
            // 
            this.ProductURL_ColumnHeader.Text = "商品URL";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 651);
            this.Controls.Add(this.HeadLess_CheckBox);
            this.Controls.Add(this.Notification_Button);
            this.Controls.Add(this.Amiami_CheckBox);
            this.Controls.Add(this.Suruga_ya_CheckBox);
            this.Controls.Add(this.Amazon_CheckBox);
            this.Controls.Add(this.Product_ListView);
            this.Controls.Add(this.Search_Button);
            this.Controls.Add(this.SearchText_TextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox SearchText_TextBox;
		private System.Windows.Forms.Button Search_Button;
		private System.Windows.Forms.ListView Product_ListView;
		private System.Windows.Forms.CheckBox Amazon_CheckBox;
		private System.Windows.Forms.CheckBox Suruga_ya_CheckBox;
		private System.Windows.Forms.CheckBox Amiami_CheckBox;
        private System.Windows.Forms.ColumnHeader Site_ColumnHeader;
		private System.Windows.Forms.ColumnHeader Image_ColumnHeader;
		private System.Windows.Forms.ColumnHeader ProductName_ColumnHeader;
		private System.Windows.Forms.ColumnHeader Maker_ColumnHeader;
		private System.Windows.Forms.ColumnHeader ReleaseDate_ColumnHeader;
		private System.Windows.Forms.ColumnHeader Price_ColumnHeader;
		private System.Windows.Forms.ImageList Favicons_ImageList;
        private System.Windows.Forms.Button Notification_Button;
        private System.Windows.Forms.CheckBox HeadLess_CheckBox;
        private System.Windows.Forms.ColumnHeader ProductURL_ColumnHeader;
    }
}

