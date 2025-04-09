using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen
{
    public partial class Checkout : Form
    {
        public static event EventHandler clearCos;
        public Checkout()
        {
            InitializeComponent();
            InfoClient1.Text=form1.currentClient.ToString();

            InfoComanda1.Columns.Add("Denumire Produs", 600);
            InfoComanda1.Columns.Add("Pret", 130);

            InfoComanda1.View = View.Details;
            InfoComanda1.VirtualMode = true;

            InfoComanda1.VirtualListSize = form1.cosProdus.Count;

            InfoComanda1.RetrieveVirtualItem += InfoComanda1_RetrieveVirtualItem;
            float price = 0;
            foreach(Produs p in form1.cosProdus.Keys)
            {
                price += p.Pret*form1.cosProdus[p];
            }
            Total.Text ="Total : " + price.ToString() + " RON";
            


        }
        private void InfoComanda1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (form1.cosProdus != null && e.ItemIndex < form1.cosProdus.Count)
            {
               
                Produs p = form1.cosProdus.Keys.ElementAt(e.ItemIndex);
                int quantity = form1.cosProdus[p];

                ListViewItem item = new ListViewItem(p.Denumire + " x" + quantity.ToString());
                item.SubItems.Add((p.Pret * quantity).ToString() + " RON");

                e.Item = item;
            }
        }
        private void Checkout_FormClosing(object sender, FormClosingEventArgs e)
        {
            SignUpForm.returnToMainPage?.Invoke(this, false);
        }
        private bool IsRadioButtonSelected(Panel label)
        {
            return label.Controls.OfType<RadioButton>().Any(rb => rb.Checked);
        }

        private async void PlasareComanda_Click(object sender, EventArgs e)
        {
            if (IsRadioButtonSelected(livrare) && IsRadioButtonSelected(plata))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    MessageBox.Show("Comanda Confirmata!");
                    SaveOrderDetailsToFile();
                    await UpdateStockAndSaveAsync();
                    clearCos?.Invoke(this, EventArgs.Empty);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați opțiunile atât pentru livrare, cât și pentru plată.");
            }
        }

        private async Task UpdateStockAndSaveAsync()
        {
            foreach (var kvp in form1.cosProdus)
            {
                string productCode = kvp.Key.CodIdentificare;
                int quantityOrdered = kvp.Value;
                Depozit.Current.UpdateStoc(productCode, quantityOrdered);
            }

            await Depozit.Current.SaveProduseToXmlAsync("produseAlimentare.xml", "Alimentar");
            await Depozit.Current.SaveProduseToXmlAsync("produseNealimentare.xml", "Nealimentar");
        }
        private void SaveOrderDetailsToFile()
        {
            Random rnd = new Random(); 
            int nr = rnd.Next(10000000, 99999999);
            string filePath = $"Comenzi\\Comanda_{nr}_{form1.currentClient.ID}.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(form1.currentClient.ID.ToString());
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    string metodaLivrare = livrare.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Text;
                    writer.WriteLine(metodaLivrare);
                    string metodaPlata = plata.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Text;
                    writer.WriteLine(metodaPlata);
                    foreach (var kvp in form1.cosProdus)
                    {
                        writer.WriteLine($"{kvp.Key.CodIdentificare}/{kvp.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ceva nu a mers bine: {ex.Message}");
            }
        }

    }

}
