namespace Examen
{
    partial class Factura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Factura));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nrFactura = new System.Windows.Forms.Label();
            this.dataFactura = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numeClient = new System.Windows.Forms.Label();
            this.adresaClient = new System.Windows.Forms.Label();
            this.metodaLivrare = new System.Windows.Forms.Label();
            this.metodaPlata = new System.Windows.Forms.Label();
            this.facturaProduse = new System.Windows.Forms.ListView();
            this.a = new System.Windows.Forms.Label();
            this.total = new System.Windows.Forms.Label();
            this.tva = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.totalDePlata = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(33, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 138);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // nrFactura
            // 
            this.nrFactura.BackColor = System.Drawing.Color.DarkCyan;
            this.nrFactura.Font = new System.Drawing.Font("Kristen ITC", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nrFactura.Location = new System.Drawing.Point(495, 21);
            this.nrFactura.Name = "nrFactura";
            this.nrFactura.Size = new System.Drawing.Size(275, 46);
            this.nrFactura.TabIndex = 1;
            this.nrFactura.Text = "Factura #534213";
            this.nrFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataFactura
            // 
            this.dataFactura.BackColor = System.Drawing.Color.Transparent;
            this.dataFactura.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataFactura.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataFactura.Location = new System.Drawing.Point(495, 67);
            this.dataFactura.Name = "dataFactura";
            this.dataFactura.Size = new System.Drawing.Size(275, 46);
            this.dataFactura.TabIndex = 2;
            this.dataFactura.Text = "Data facturarii:";
            this.dataFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Location = new System.Drawing.Point(178, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "CUMPARATOR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numeClient
            // 
            this.numeClient.BackColor = System.Drawing.Color.Transparent;
            this.numeClient.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeClient.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.numeClient.Location = new System.Drawing.Point(178, 67);
            this.numeClient.Name = "numeClient";
            this.numeClient.Size = new System.Drawing.Size(275, 46);
            this.numeClient.TabIndex = 4;
            this.numeClient.Text = "Nume client";
            this.numeClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adresaClient
            // 
            this.adresaClient.BackColor = System.Drawing.Color.Transparent;
            this.adresaClient.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adresaClient.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.adresaClient.Location = new System.Drawing.Point(178, 113);
            this.adresaClient.Name = "adresaClient";
            this.adresaClient.Size = new System.Drawing.Size(311, 92);
            this.adresaClient.TabIndex = 5;
            this.adresaClient.Text = "Adresa client";
            // 
            // metodaLivrare
            // 
            this.metodaLivrare.BackColor = System.Drawing.Color.Transparent;
            this.metodaLivrare.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metodaLivrare.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.metodaLivrare.Location = new System.Drawing.Point(495, 113);
            this.metodaLivrare.Name = "metodaLivrare";
            this.metodaLivrare.Size = new System.Drawing.Size(275, 46);
            this.metodaLivrare.TabIndex = 6;
            this.metodaLivrare.Text = "Metoda de livrare:";
            this.metodaLivrare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metodaPlata
            // 
            this.metodaPlata.BackColor = System.Drawing.Color.Transparent;
            this.metodaPlata.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metodaPlata.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.metodaPlata.Location = new System.Drawing.Point(496, 159);
            this.metodaPlata.Name = "metodaPlata";
            this.metodaPlata.Size = new System.Drawing.Size(275, 46);
            this.metodaPlata.TabIndex = 7;
            this.metodaPlata.Text = "Metoda de plata:";
            this.metodaPlata.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // facturaProduse
            // 
            this.facturaProduse.FullRowSelect = true;
            this.facturaProduse.GridLines = true;
            this.facturaProduse.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.facturaProduse.HideSelection = false;
            this.facturaProduse.Location = new System.Drawing.Point(12, 220);
            this.facturaProduse.Name = "facturaProduse";
            this.facturaProduse.Size = new System.Drawing.Size(758, 359);
            this.facturaProduse.TabIndex = 8;
            this.facturaProduse.UseCompatibleStateImageBehavior = false;
            // 
            // a
            // 
            this.a.BackColor = System.Drawing.Color.Transparent;
            this.a.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.a.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.a.Location = new System.Drawing.Point(361, 581);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(82, 45);
            this.a.TabIndex = 9;
            this.a.Text = "TOTAL";
            this.a.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // total
            // 
            this.total.BackColor = System.Drawing.Color.Transparent;
            this.total.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.total.Location = new System.Drawing.Point(498, 581);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(271, 46);
            this.total.TabIndex = 10;
            this.total.Text = "Pret";
            this.total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tva
            // 
            this.tva.BackColor = System.Drawing.Color.Transparent;
            this.tva.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tva.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tva.Location = new System.Drawing.Point(498, 615);
            this.tva.Name = "tva";
            this.tva.Size = new System.Drawing.Size(271, 46);
            this.tva.TabIndex = 12;
            this.tva.Text = "Pret";
            this.tva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label5.Location = new System.Drawing.Point(361, 615);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 45);
            this.label5.TabIndex = 11;
            this.label5.Text = "TVA";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalDePlata
            // 
            this.totalDePlata.BackColor = System.Drawing.Color.Transparent;
            this.totalDePlata.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalDePlata.ForeColor = System.Drawing.Color.DarkCyan;
            this.totalDePlata.Location = new System.Drawing.Point(499, 648);
            this.totalDePlata.Name = "totalDePlata";
            this.totalDePlata.Size = new System.Drawing.Size(270, 46);
            this.totalDePlata.TabIndex = 14;
            this.totalDePlata.Text = "Pret";
            this.totalDePlata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Location = new System.Drawing.Point(361, 648);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 45);
            this.label6.TabIndex = 13;
            this.label6.Text = "Total de plata";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.save.Location = new System.Drawing.Point(12, 648);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(119, 41);
            this.save.TabIndex = 15;
            this.save.Text = "Salveaza";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 703);
            this.Controls.Add(this.numeClient);
            this.Controls.Add(this.save);
            this.Controls.Add(this.totalDePlata);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tva);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.total);
            this.Controls.Add(this.a);
            this.Controls.Add(this.facturaProduse);
            this.Controls.Add(this.metodaPlata);
            this.Controls.Add(this.metodaLivrare);
            this.Controls.Add(this.adresaClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataFactura);
            this.Controls.Add(this.nrFactura);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Kristen ITC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(800, 750);
            this.MinimumSize = new System.Drawing.Size(800, 750);
            this.Name = "Factura";
            this.Text = "Factura";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label nrFactura;
        private System.Windows.Forms.Label dataFactura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label numeClient;
        private System.Windows.Forms.Label adresaClient;
        private System.Windows.Forms.Label metodaLivrare;
        private System.Windows.Forms.Label metodaPlata;
        private System.Windows.Forms.ListView facturaProduse;
        private System.Windows.Forms.Label a;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.Label tva;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label totalDePlata;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button save;
    }
}