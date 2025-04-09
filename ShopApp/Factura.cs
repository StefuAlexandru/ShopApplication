using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Examen
{
    public partial class Factura : Form
    {
        public Factura(Invoice invoice)
        {
            InitializeComponent();

            InitializeListView();

            foreach (var item in invoice.Articole)
            {
                ListViewItem listViewItem = new ListViewItem(item.DenumireProdus);
                listViewItem.SubItems.Add(item.Cantitate.ToString());
                listViewItem.SubItems.Add(item.PretUnitar.ToString("0.00"));
                listViewItem.SubItems.Add(item.PretTotal.ToString("0.00"));
                float tvaValue = item.PretTotal * 19 / 100;
                listViewItem.SubItems.Add(tvaValue.ToString("0.00"));
                facturaProduse.Items.Add(listViewItem);
            }

            numeClient.Text = $"{invoice.client.FirstName} {invoice.client.LastName}";
            adresaClient.Text = invoice.client.Adresa.ToString();
            dataFactura.Text += " " +invoice.DataFactura.ToString("dd/MM/yyyy HH:mm:ss");
            metodaLivrare.Text += " " +invoice.MetodaLivrare;
            metodaPlata.Text += " " + invoice.MetodaPlata;
            total.Text = invoice.SumaTotalaFaraTVA.ToString("0.00");
            tva.Text = (invoice.SumaTotala - invoice.SumaTotalaFaraTVA).ToString("0.00");
            totalDePlata.Text = (invoice.SumaTotala+ invoice.CostLivrare).ToString("0.00");
        }

        private void InitializeListView()
        {
            facturaProduse.View = View.Details;
            facturaProduse.FullRowSelect = true;
            facturaProduse.Columns.Add("Denumire produs", 200);
            facturaProduse.Columns.Add("Cantitate", 100);
            facturaProduse.Columns.Add("Pret unitar", 100);
            facturaProduse.Columns.Add("Pret total", 100);
            facturaProduse.Columns.Add("TVA", 100);
        }

        private void save_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Bitmap bitmap = new Bitmap(this.Width-20, this.Height-40);

            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            string fileName = $"Factura_{DateTime.Now:yyyyMMddHHmmss}.png";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            bitmap.Dispose();

            MessageBox.Show($"Factura salvata cu succes in:\n{filePath}", "Factura salvata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }
    }
}
