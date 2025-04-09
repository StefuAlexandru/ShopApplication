namespace Examen
{
    partial class form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form1));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.alimentareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nealimentareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.info = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.OpenSearchBar = new System.ComponentModel.BackgroundWorker();
            this.CloseSearchBar = new System.ComponentModel.BackgroundWorker();
            this.cautare = new System.Windows.Forms.TextBox();
            this.cos = new System.Windows.Forms.ListView();
            this.search = new RoundedButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator3,
            this.info,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1182, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alimentareToolStripMenuItem,
            this.nealimentareToolStripMenuItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(114, 32);
            this.toolStripDropDownButton1.Text = "Produse";
            // 
            // alimentareToolStripMenuItem
            // 
            this.alimentareToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alimentareToolStripMenuItem.Image")));
            this.alimentareToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.alimentareToolStripMenuItem.Name = "alimentareToolStripMenuItem";
            this.alimentareToolStripMenuItem.Size = new System.Drawing.Size(207, 28);
            this.alimentareToolStripMenuItem.Text = "Alimentare";
            this.alimentareToolStripMenuItem.Click += new System.EventHandler(this.alimentareToolStripMenuItem_Click);
            // 
            // nealimentareToolStripMenuItem
            // 
            this.nealimentareToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nealimentareToolStripMenuItem.Image")));
            this.nealimentareToolStripMenuItem.Name = "nealimentareToolStripMenuItem";
            this.nealimentareToolStripMenuItem.Size = new System.Drawing.Size(207, 28);
            this.nealimentareToolStripMenuItem.Text = "Nealimentare";
            this.nealimentareToolStripMenuItem.Click += new System.EventHandler(this.nealimentareToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator1.Size = new System.Drawing.Size(10, 35);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButton2.Size = new System.Drawing.Size(124, 32);
            this.toolStripButton2.Text = "Coșul meu";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator3.Size = new System.Drawing.Size(10, 35);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Transparent;
            this.info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.info.CheckOnClick = true;
            this.info.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info.Image = ((System.Drawing.Image)(resources.GetObject("info.Image")));
            this.info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(95, 32);
            this.info.Text = "Despre";
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButton3.Size = new System.Drawing.Size(29, 32);
            this.toolStripButton3.Text = "Deconecteaza-te!";
            this.toolStripButton3.Click += new System.EventHandler(this.deco);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator2.Size = new System.Drawing.Size(10, 35);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(136, 32);
            this.toolStripButton1.Text = "Contul Meu";
            this.toolStripButton1.Click += new System.EventHandler(this.toAccount);
            // 
            // OpenSearchBar
            // 
            this.OpenSearchBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OpenSearchBar_DoWork);
            // 
            // CloseSearchBar
            // 
            this.CloseSearchBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CloseSearchBar_DoWork);
            // 
            // cautare
            // 
            this.cautare.AccessibleDescription = "";
            this.cautare.AccessibleName = "";
            this.cautare.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cautare.Font = new System.Drawing.Font("Kristen ITC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cautare.ForeColor = System.Drawing.Color.Navy;
            this.cautare.Location = new System.Drawing.Point(83, 72);
            this.cautare.Name = "cautare";
            this.cautare.Size = new System.Drawing.Size(426, 44);
            this.cautare.TabIndex = 2;
            this.cautare.Text = "Search";
            this.cautare.TextChanged += new System.EventHandler(this.cautare_TextChanged);
            this.cautare.Enter += new System.EventHandler(this.cautare_Enter);
            this.cautare.Leave += new System.EventHandler(this.cautare_Leave);
            // 
            // cos
            // 
            this.cos.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.cos.BackColor = System.Drawing.Color.PowderBlue;
            this.cos.Dock = System.Windows.Forms.DockStyle.Right;
            this.cos.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cos.FullRowSelect = true;
            this.cos.GridLines = true;
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            this.cos.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.cos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.cos.HideSelection = false;
            this.cos.Location = new System.Drawing.Point(612, 35);
            this.cos.Name = "cos";
            this.cos.OwnerDraw = true;
            this.cos.Size = new System.Drawing.Size(570, 638);
            this.cos.TabIndex = 4;
            this.cos.UseCompatibleStateImageBehavior = false;
            this.cos.View = System.Windows.Forms.View.Details;
            this.cos.VirtualMode = true;
            this.cos.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.cos_DrawColumnHeader);
            this.cos.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.cos_DrawSubItem);
            this.cos.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.cos_RetrieveVirtualItem);
            this.cos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cos_MouseClick);
            // 
            // search
            // 
            this.search.BackColor = System.Drawing.Color.Transparent;
            this.search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("search.BackgroundImage")));
            this.search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.search.Location = new System.Drawing.Point(27, 66);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(50, 50);
            this.search.TabIndex = 3;
            this.search.UseVisualStyleBackColor = false;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(24F, 51F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1182, 673);
            this.Controls.Add(this.cos);
            this.Controls.Add(this.search);
            this.Controls.Add(this.cautare);
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Kristen ITC", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.MaximumSize = new System.Drawing.Size(1200, 720);
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "form1";
            this.Text = "LaBober";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form1_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton info;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem alimentareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nealimentareToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.ComponentModel.BackgroundWorker OpenSearchBar;
        private System.ComponentModel.BackgroundWorker CloseSearchBar;
        private System.Windows.Forms.TextBox cautare;
        private RoundedButton roundedButton1;
        private RoundedButton search;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        public System.Windows.Forms.ListView cos;
    }
}

