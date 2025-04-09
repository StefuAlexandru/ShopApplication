using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Policy;
using System.IO;

namespace Examen
{
    public partial class MyAccount : Form
    {
        public static List<string> orders = new List<string>();
        public MyAccount()
        {
            InitializeComponent();

            form1.CompleteInformations += OnCompleteInformations;

            contentDashboard.Text = $"Bine ai venit {form1.currentClient.FirstName + " " + form1.currentClient.LastName}. \n Aici poti sa vezi comenziile facute de tine, adresele tale de livrare,\r\ncat si alte detalii despre contul tau! ";
            dash.BackColor = Color.CornflowerBlue;
            detalii.BackColor = comenzi.BackColor = adrese.BackColor = Color.PowderBlue;
            contentDashboard.Visible = true;
            adress.Visible = false;
            save.Visible = false;
            contentComenzi.Visible = false;
            
            orders = getOrders();

            contentComenzi.FullRowSelect = true;
            contentComenzi.VirtualListSize = orders.Count;
            contentComenzi.Columns.Add("Numar Comanda", 300);
            contentComenzi.Columns.Add("Vezi detalii", 350);

            SetValues();

            
        }
        private void OnCompleteInformations(object sender, EventArgs e)
        {
    
            dash_Leave(sender, e);
            adrese_Enter(sender, e);
            adrese_Click(sender, e);
            adrese_Enter(sender, e);

        }
        private void SetValues()
        {
            tara.Text = form1.currentClient.Adresa.Tara ?? string.Empty; 
            judet.Text = form1.currentClient.Adresa.Judet ?? string.Empty;
            oras.Text = form1.currentClient.Adresa.Oras ?? string.Empty;
            strada.Text = form1.currentClient.Adresa.Strada ?? string.Empty;
            codPostal.Text = form1.currentClient.Adresa.CodPostal ?? string.Empty;
            bloc.Text = form1.currentClient.Adresa.Bloc ?? string.Empty;
            numar.Text = form1.currentClient.Adresa.Numar != null ? form1.currentClient.Adresa.Numar.ToString() : string.Empty;
            scara.Text = form1.currentClient.Adresa.Scara ?? string.Empty;
            phone.Text = form1.currentClient.Adresa.NumarDeTelefon ?? string.Empty;
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUpForm.returnToMainPage?.Invoke(this, false);
            this.Close();
        }

        private void MyAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            SignUpForm.returnToMainPage?.Invoke(this,false);
        }

        private void dash_Click(object sender, EventArgs e)
        {
            dash.BackColor = Color.CornflowerBlue;
            contentDashboard.Visible = true;
            adress.Visible = false;
            save.Visible = false;
            contentDetalii.Visible = false;
            contentComenzi.Visible = false;
        }

        private void adrese_Click(object sender, EventArgs e)
        {
            adress.Visible = true;
            contentDashboard.Visible = false;
            save.Visible = true;
            contentDetalii.Visible = false;
            contentComenzi.Visible = false;
        }

        private void save_Click(object sender, EventArgs e)
        {
            int nr = 0;
            int.TryParse(numar.Text, out nr);
            form1.currentClient.Adresa = new Adresa(tara.Text, judet.Text, oras.Text,strada.Text ,codPostal.Text,bloc.Text,nr,scara.Text,phone.Text);
            string mesaj = form1.currentClient.Adresa.SaveAddressInDatabase(form1.currentClient.ID);
            MessageBox.Show(mesaj);
        }

        private void comenzi_Enter(object sender, EventArgs e)
        {
            comenzi.BackColor = Color.CornflowerBlue;
        }

        private void comenzi_Leave(object sender, EventArgs e)
        {
            comenzi.BackColor = Color.PowderBlue;
        }

        private void dash_Enter(object sender, EventArgs e)
        {
            if(sender == this)
            {
                dash.BackColor = Color.CornflowerBlue;
            }
        }

        private void dash_Leave(object sender, EventArgs e)
        {
            dash.BackColor = Color.PowderBlue;
        }

        private void adrese_Enter(object sender, EventArgs e)
        {
            adrese.BackColor = Color.CornflowerBlue;
        
        }

        private void adrese_Leave(object sender, EventArgs e)
        {
            adrese.BackColor = Color.PowderBlue;
        }

        private void detalii_Enter(object sender, EventArgs e)
        {
            detalii.BackColor = Color.CornflowerBlue;
        }

        private void detalii_Leave(object sender, EventArgs e)
        {
            detalii.BackColor = Color.PowderBlue;
        }

        private void comenzi_Click(object sender, EventArgs e)
        {
            contentComenzi.Visible = true;
            contentDashboard.Visible = false;
            adress.Visible = false;
            save.Visible = false;
            contentDetalii.Visible = false;
        }
        
        private void detalii_Click(object sender, EventArgs e)
        {
            contentDetalii.Visible = true;
            contentDetalii.Text = "Informatii personale : \n" + form1.currentClient.ToString();
            contentDashboard.Visible = false;
            adress.Visible = false;
            save.Visible = false;
            contentComenzi.Visible = false;
        }
        private List<string> getOrders()
        {
            List<string> comenzi = new List<string>();
            try
            {
                IEnumerable<string> folderComenzi = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "\\Comenzi");
                
                foreach(string nameOfFile in folderComenzi)
                {
                    string comanda = nameOfFile.Substring(nameOfFile.LastIndexOf("_") + 1);
                    string  sufix = comanda.Remove(comanda.LastIndexOf("."));
                    int nr;
                    if(int.TryParse(sufix, out nr))
                    {
                        if (nr == form1.currentClient.ID)
                        {
                            comenzi.Add(nameOfFile);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ceva nu-i bine : {ex.Message}");
            }
            return comenzi;
        }
        private void contentComenzi_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (orders != null)
            {
                if(e.ItemIndex < orders.Count)
                {
                    string fullPath = orders[e.ItemIndex];
                    string fileName = Path.GetFileNameWithoutExtension(fullPath);
                    string prefix = fileName.Substring(fileName.IndexOf("_") + 1,8); 
                    ListViewItem item = new ListViewItem(prefix);
                    item.SubItems.Add("Vizualizeaza factura");
                    e.Item = item;
                }
            }


        }
        private Invoice getOrderDetails(int index)
        {
            FileStream fs = null;
            StreamReader sr = null;  
            Invoice factura = null;
            try
            {
                using (fs = new FileStream(orders[index], FileMode.Open))
                {
                    using (sr = new StreamReader(fs))
                    {

                        int id = int.Parse(sr.ReadLine());
                        Client customer = Client.LoadFromDatabase(id);

                        DateTime dataFactura = DateTime.Parse(sr.ReadLine());

                        string[] livrareDetails = sr.ReadLine().Split('-');
                        string metodaLivrare = livrareDetails[0];
                        float costLivrare = float.Parse(livrareDetails[1].Split(' ')[0]);

                        string metodaPlata = sr.ReadLine();

                        string fullPath = orders[index];
                        string fileName = Path.GetFileNameWithoutExtension(fullPath);
                        string prefix = fileName.Substring(fileName.IndexOf("_") + 1, 8);

                        FacturaBuilder builder = new FacturaBuilder()
                            .SetNumarFactura(int.Parse(prefix)) 
                            .SetDataFactura(dataFactura)
                            .SetClient(customer)
                            .SetMetodaLivrare(metodaLivrare)
                            .SetMetodaPlata(metodaPlata);

                       
                        while (true)
                        {
                            string line = sr.ReadLine();
                            if (line == null)
                            {
                                break;
                            }

                            string[] sublines = line.Split('/');
                            Produs produs = Depozit.Current.getProdusFromAllProducts(sublines[0]);
                            if (produs != null)
                            {
                                builder.AdaugaArticol(produs.Denumire, int.Parse(sublines[1]), produs.Pret);
                            }
                        }

                        factura = builder.Build();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ceva nu a mers bine : {e.Message}");
            }

            return factura;
        }

        private void contentComenzi_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = contentComenzi.HitTest(e.X, e.Y);
            if (info != null && info.SubItem != null && info.SubItem.Text.Equals("Vizualizeaza factura"))
            {
                int selectedIndex = info.Item.Index;
                Invoice factura = getOrderDetails(selectedIndex);

                if (factura != null)
                {
                    Factura facturaForm = new Factura(factura);
                    facturaForm.ShowDialog();
                }
            }
        }

    }
}
