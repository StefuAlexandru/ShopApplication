namespace Examen
{
    partial class Checkout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Checkout));
            this.das = new System.Windows.Forms.Label();
            this.fancourier = new System.Windows.Forms.RadioButton();
            this.dpd = new System.Windows.Forms.RadioButton();
            this.sameday = new System.Windows.Forms.RadioButton();
            this.sadas = new System.Windows.Forms.Label();
            this.Cash = new System.Windows.Forms.RadioButton();
            this.Card = new System.Windows.Forms.RadioButton();
            this.InfoClient = new System.Windows.Forms.Label();
            this.InfoClient1 = new System.Windows.Forms.Label();
            this.InfoComanda = new System.Windows.Forms.Label();
            this.PlasareComanda = new System.Windows.Forms.Button();
            this.livrare = new System.Windows.Forms.Panel();
            this.plata = new System.Windows.Forms.Panel();
            this.InfoComanda1 = new System.Windows.Forms.ListView();
            this.Total = new System.Windows.Forms.TextBox();
            this.livrare.SuspendLayout();
            this.plata.SuspendLayout();
            this.SuspendLayout();
            // 
            // das
            // 
            this.das.BackColor = System.Drawing.Color.DarkCyan;
            this.das.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.das.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.das.Location = new System.Drawing.Point(12, 9);
            this.das.Name = "das";
            this.das.Size = new System.Drawing.Size(305, 29);
            this.das.TabIndex = 0;
            this.das.Text = "Alege metoda de livrare";
            // 
            // fancourier
            // 
            this.fancourier.AutoSize = true;
            this.fancourier.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fancourier.Location = new System.Drawing.Point(3, 3);
            this.fancourier.Name = "fancourier";
            this.fancourier.Size = new System.Drawing.Size(222, 28);
            this.fancourier.TabIndex = 1;
            this.fancourier.TabStop = true;
            this.fancourier.Text = "Fan Courier-19,99 RON";
            this.fancourier.UseVisualStyleBackColor = true;
            // 
            // dpd
            // 
            this.dpd.AutoSize = true;
            this.dpd.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpd.Location = new System.Drawing.Point(3, 35);
            this.dpd.Name = "dpd";
            this.dpd.Size = new System.Drawing.Size(165, 28);
            this.dpd.TabIndex = 2;
            this.dpd.TabStop = true;
            this.dpd.Text = "DPD-24,99 RON";
            this.dpd.UseVisualStyleBackColor = true;
            // 
            // sameday
            // 
            this.sameday.AutoSize = true;
            this.sameday.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sameday.Location = new System.Drawing.Point(3, 67);
            this.sameday.Name = "sameday";
            this.sameday.Size = new System.Drawing.Size(199, 28);
            this.sameday.TabIndex = 3;
            this.sameday.TabStop = true;
            this.sameday.Text = "SameDay-19,99 RON";
            this.sameday.UseVisualStyleBackColor = true;
            // 
            // sadas
            // 
            this.sadas.BackColor = System.Drawing.Color.DarkCyan;
            this.sadas.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sadas.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sadas.Location = new System.Drawing.Point(349, 9);
            this.sadas.Name = "sadas";
            this.sadas.Size = new System.Drawing.Size(405, 29);
            this.sadas.TabIndex = 4;
            this.sadas.Text = "Alege metoda de plata";
            // 
            // Cash
            // 
            this.Cash.AutoSize = true;
            this.Cash.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cash.Location = new System.Drawing.Point(3, 3);
            this.Cash.Name = "Cash";
            this.Cash.Size = new System.Drawing.Size(151, 28);
            this.Cash.TabIndex = 5;
            this.Cash.TabStop = true;
            this.Cash.Text = "Cash la livrare";
            this.Cash.UseVisualStyleBackColor = true;
            // 
            // Card
            // 
            this.Card.AutoSize = true;
            this.Card.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Card.Location = new System.Drawing.Point(3, 35);
            this.Card.Name = "Card";
            this.Card.Size = new System.Drawing.Size(73, 28);
            this.Card.TabIndex = 6;
            this.Card.TabStop = true;
            this.Card.Text = "Card";
            this.Card.UseVisualStyleBackColor = true;
            // 
            // InfoClient
            // 
            this.InfoClient.AutoSize = true;
            this.InfoClient.BackColor = System.Drawing.Color.DarkCyan;
            this.InfoClient.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoClient.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InfoClient.Location = new System.Drawing.Point(12, 159);
            this.InfoClient.Name = "InfoClient";
            this.InfoClient.Size = new System.Drawing.Size(287, 28);
            this.InfoClient.TabIndex = 7;
            this.InfoClient.Text = "Informatiile dumneavoastra:";
            // 
            // InfoClient1
            // 
            this.InfoClient1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.InfoClient1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InfoClient1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoClient1.ForeColor = System.Drawing.Color.Black;
            this.InfoClient1.Location = new System.Drawing.Point(12, 198);
            this.InfoClient1.Name = "InfoClient1";
            this.InfoClient1.Size = new System.Drawing.Size(762, 96);
            this.InfoClient1.TabIndex = 8;
            // 
            // InfoComanda
            // 
            this.InfoComanda.BackColor = System.Drawing.Color.DarkCyan;
            this.InfoComanda.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoComanda.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InfoComanda.Location = new System.Drawing.Point(12, 308);
            this.InfoComanda.Name = "InfoComanda";
            this.InfoComanda.Size = new System.Drawing.Size(409, 29);
            this.InfoComanda.TabIndex = 9;
            this.InfoComanda.Text = "Informatii despre comanda:";
            // 
            // PlasareComanda
            // 
            this.PlasareComanda.Location = new System.Drawing.Point(177, 645);
            this.PlasareComanda.Name = "PlasareComanda";
            this.PlasareComanda.Size = new System.Drawing.Size(405, 41);
            this.PlasareComanda.TabIndex = 11;
            this.PlasareComanda.Text = "Plaseaza Comanda";
            this.PlasareComanda.UseVisualStyleBackColor = true;
            this.PlasareComanda.Click += new System.EventHandler(this.PlasareComanda_Click);
            // 
            // livrare
            // 
            this.livrare.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.livrare.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.livrare.Controls.Add(this.fancourier);
            this.livrare.Controls.Add(this.dpd);
            this.livrare.Controls.Add(this.sameday);
            this.livrare.Location = new System.Drawing.Point(12, 41);
            this.livrare.Name = "livrare";
            this.livrare.Size = new System.Drawing.Size(305, 104);
            this.livrare.TabIndex = 12;
            // 
            // plata
            // 
            this.plata.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.plata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plata.Controls.Add(this.Cash);
            this.plata.Controls.Add(this.Card);
            this.plata.Location = new System.Drawing.Point(349, 41);
            this.plata.Name = "plata";
            this.plata.Size = new System.Drawing.Size(405, 104);
            this.plata.TabIndex = 13;
            // 
            // InfoComanda1
            // 
            this.InfoComanda1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.InfoComanda1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoComanda1.ForeColor = System.Drawing.Color.Black;
            this.InfoComanda1.GridLines = true;
            this.InfoComanda1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.InfoComanda1.HideSelection = false;
            this.InfoComanda1.Location = new System.Drawing.Point(12, 340);
            this.InfoComanda1.Name = "InfoComanda1";
            this.InfoComanda1.Size = new System.Drawing.Size(762, 239);
            this.InfoComanda1.TabIndex = 0;
            this.InfoComanda1.UseCompatibleStateImageBehavior = false;
            this.InfoComanda1.VirtualMode = true;
            // 
            // Total
            // 
            this.Total.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Total.Font = new System.Drawing.Font("Kristen ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total.ForeColor = System.Drawing.Color.Black;
            this.Total.Location = new System.Drawing.Point(511, 585);
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Size = new System.Drawing.Size(263, 35);
            this.Total.TabIndex = 0;
            this.Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Checkout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 703);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.InfoComanda1);
            this.Controls.Add(this.plata);
            this.Controls.Add(this.livrare);
            this.Controls.Add(this.PlasareComanda);
            this.Controls.Add(this.InfoComanda);
            this.Controls.Add(this.InfoClient1);
            this.Controls.Add(this.InfoClient);
            this.Controls.Add(this.sadas);
            this.Controls.Add(this.das);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Kristen ITC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MaximumSize = new System.Drawing.Size(800, 750);
            this.MinimumSize = new System.Drawing.Size(800, 750);
            this.Name = "Checkout";
            this.Text = "Checkout";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Checkout_FormClosing);
            this.livrare.ResumeLayout(false);
            this.livrare.PerformLayout();
            this.plata.ResumeLayout(false);
            this.plata.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label das;
        private System.Windows.Forms.RadioButton fancourier;
        private System.Windows.Forms.RadioButton dpd;
        private System.Windows.Forms.RadioButton sameday;
        private System.Windows.Forms.Label sadas;
        private System.Windows.Forms.RadioButton Cash;
        private System.Windows.Forms.RadioButton Card;
        private System.Windows.Forms.Label InfoClient;
        private System.Windows.Forms.Label InfoClient1;
        private System.Windows.Forms.Label InfoComanda;
        private System.Windows.Forms.Button PlasareComanda;
        private System.Windows.Forms.Panel livrare;
        private System.Windows.Forms.Panel plata;
        private System.Windows.Forms.ListView InfoComanda1;
        private System.Windows.Forms.TextBox Total;
    }
}