using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace Examen
{
    public partial class form1 : Form
    {
        private bool logIn;
        private string currentUsername = "";
        public static Client currentClient;
        public Depozit depozit;
        private BackgroundWorker displayWorker;
        private List<Produs> products;
        public static Dictionary<Produs,int> cosProdus = new Dictionary<Produs, int> { };
        private ListView produseListView;

        public static event EventHandler CompleteInformations;

        public form1()
        {
            InitializeComponent();
            LoginForm.LoggedIn += LoginForm_LoggedIn;
            SignUpForm.returnToMainPage += handleReturn;
            Checkout.clearCos += ClearCos;
            LoadLoginState();
            Init();
            

            depozit = Depozit.Current;


            LoadProducts("Alimentar");
            LoadProducts("Nealimentar");

            displayWorker = new BackgroundWorker();
            displayWorker.DoWork += DisplayWorker_DoWork;
            displayWorker.RunWorkerCompleted += DisplayWorker_RunWorkerCompleted;

            produseListView = new ListView
            {
                View = View.Details,
                VirtualMode = true,
                FullRowSelect = true,
                Size = new Size(1000, 500),
                Location = new Point(83, 150),
                Font = new Font("Kristen ITC", 15, FontStyle.Regular),
                GridLines = true,
                BackColor = Color.PowderBlue,
                ForeColor = Color.Black
            };
            produseListView.Columns.Add("Numele Produsului", 300);
            produseListView.Columns.Add("Descrierea Produsului", 250);
            produseListView.Columns.Add("Pret", 150);
            produseListView.Columns.Add("Stoc", 75);
            produseListView.Columns.Add("", 200);
            produseListView.RetrieveVirtualItem += ProduseListView_RetrieveVirtualItem;
            produseListView.MouseClick += ProduseListView_MouseClick;
            produseListView.OwnerDraw = true;
            produseListView.DrawColumnHeader += ProduseListView_DrawColumnHeader;
            produseListView.DrawSubItem += ProduseListView_DrawSubItem;


            this.Controls.Add(produseListView);

            cos.Columns.Add("", 40);
            cos.Columns.Add("Produsele dumneavostra", 280);
            cos.Columns.Add("Pret", 140);
            cos.Columns.Add("", 50);
            cos.Columns.Add("", 50);
        }

        private void ProduseListView_DrawColumnHeader(object sender,DrawListViewColumnHeaderEventArgs e)
        { 
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            e.Graphics.DrawString(e.Header.Text, new Font(e.Font,FontStyle.Bold), Brushes.Black, e.Bounds);
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
                e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }
        }
        private void ProduseListView_DrawSubItem(object sender,DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        private async void LoadProducts(string categorie)
        {
            this.Cursor = Cursors.WaitCursor;
            if (displayWorker != null)
            {
                if (displayWorker.IsBusy)
                {
                    displayWorker.CancelAsync();
                }
            }
            products?.Clear();
            if (categorie == "Alimentar")
            {
                await depozit.LoadProduseFromXmlAsync("produseAlimentare.xml", "Alimentar");
            }
            else
            {
                await depozit.LoadProduseFromXmlAsync("produseNealimentare.xml", "Nealimentar");

            }

            this.Cursor = Cursors.Default; 

            if (depozit != null)
            {
                if (!displayWorker.IsBusy)
                {
                    displayWorker.RunWorkerAsync(depozit.produse);
                }
            }

        }

        private void LoginForm_LoggedIn(object sender, Client client)
        {
            currentClient = client;
            logIn = true;
            currentUsername = client.Username;
            this.Show();
        }

        private void handleReturn(object sender, bool close)
        {
            if (close) this.Close(); else this.Show();
        }

        private void LoadLoginState()
        {
            logIn = Properties.Settings.Default.LoggedIn;
            currentUsername = Properties.Settings.Default.Username;
            currentClient = Client.LoadFromDatabase(currentUsername);
        }

        private void SaveLoginState(bool logIn, string currentUsername)
        {
            Properties.Settings.Default.LoggedIn = logIn;
            Properties.Settings.Default.Username = currentUsername;
            Properties.Settings.Default.Save();
        }

        private void Init()
        {
            if (!logIn)
            {
                this.Hide();
                using (SignUpForm suf = new SignUpForm())
                {
                    suf.ShowDialog();
                }
            }
            cautare.Width = 0;
            cautare.Enabled = false;
            cos.Visible = false;
        }

        private void cautare_Enter(object sender, EventArgs e)
        {
            if (cautare.Text == "Search")
            {
                cautare.Text = "";
                cautare.ForeColor = Color.Black;
            }
        }

        private void cautare_Leave(object sender, EventArgs e)
        {
            if (cautare.Text == "")
            {
                cautare.Text = "Search";
                cautare.ForeColor = Color.Navy;
            }
        }

        private bool isSearchInProgress = false;
        private void search_Click(object sender, EventArgs e)
        {
            if (!isSearchInProgress)
            {
                isSearchInProgress = true;
                if (!cautare.Enabled)
                {
                    OpenSearchBar.RunWorkerAsync();
                }
                else
                {
                    if (cautare.Text == "Search" || cautare.Text.Length == 0)
                    {
                        CloseSearchBar.RunWorkerAsync();
                    }
                    else
                    {
                        isSearchInProgress = false;
                    }
                }
            }
        }

        private void OpenSearchBar_DoWork(object sender, DoWorkEventArgs e)
        {
            while (cautare.Width < 500)
            {
                cautare.Invoke(new Action(() =>
                {
                    cautare.Width += 20;
                    if (cautare.Width > 500)
                    {
                        cautare.Width = 500;
                    }
                }));
                Thread.Sleep(1);
            }
            cautare.Invoke(new Action(() => cautare.Enabled = true));
            isSearchInProgress = false;
        }

        private void CloseSearchBar_DoWork(object sender, DoWorkEventArgs e)
        {
            cautare.Invoke(new Action(() => cautare.Enabled = false));
            while (cautare.Width > 0)
            {
                cautare.Invoke(new Action(() =>
                {
                    cautare.Width -= 20;
                    if (cautare.Width < 0)
                    {
                        cautare.Width = 0;
                    }
                }));
                Thread.Sleep(1);
            }
            isSearchInProgress = false;
        }

        private void toAccount(object sender, EventArgs e)
        {
            this.Hide();
            MyAccount ma = new MyAccount();
            ma.ShowDialog();
        }

        private void deco(object sender, EventArgs e)
        {
            SaveLoginState(false, "");
            LoadLoginState();
            this.Hide();
            using (LoginForm lf = new LoginForm())
            {
                lf.ShowDialog();
            }
        }

        private void form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            SaveLoginState(logIn, currentUsername);
            LoadLoginState();
            SignUpForm.returnToMainPage -= handleReturn;
        }

        private void cautare_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            if (depozit != null)
            {
                string searchTerm = cautare.Text;
                var results = depozit.SearchProduse(searchTerm);
                if (!displayWorker.IsBusy)
                {
                    displayWorker.RunWorkerAsync(results);
                }
            }
        }

        private void DisplayWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Produs> results = e.Argument as List<Produs>;
            e.Result = results;
        }

        private void DisplayWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            products = e.Result as List<Produs>;
            produseListView.VirtualListSize = products.Count;
            produseListView.Invalidate();
        }

        private void ProduseListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (products != null && e.ItemIndex < products.Count)
            {
                Produs produs = products[e.ItemIndex];
                ListViewItem item = new ListViewItem(produs.Denumire);
                item.SubItems.Add("Vezi detalii");
                item.SubItems.Add(produs.Pret.ToString() + " RON");
                item.SubItems.Add(produs.Stoc.ToString());
                item.SubItems.Add("adauga in cos");


                item.Tag = produs;
                e.Item = item;
            }
        }


        private void ProduseListView_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = produseListView.HitTest(e.X, e.Y);

            if (info.Item != null && info.SubItem != null)
            {
                Produs produs = info.Item.Tag as Produs;

                if (info.SubItem.Text == "adauga in cos" && produs != null)
                {
                    if (produs.Stoc > 0)
                    {
                        if (cosProdus.ContainsKey(produs))
                        {
                            cosProdus[produs]++;
                        }
                        else
                        {
                            cosProdus.Add(produs, 1);
                        }

                        cos.VirtualListSize = cosProdus.Count + 1;
                        //Forteaza lista sa se redesenze
                        cos.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Stoc indisponibil");
                    }
                }
                else if (info.SubItem.Text == "Vezi detalii" && produs != null)
                {
                    char minErrorLevel = 'H';
                    string content = produs.DisplayInfo();
                    QR q = new QR();
                    byte[] qrCode;
                    int version;
                    char errorLevel;
                    int encodingMode;
                    int codewordsLength;
                    int maskIndex;
                    q.GetQRCode(content, minErrorLevel, out qrCode, out version, out errorLevel,
                                out encodingMode, out codewordsLength, out maskIndex);

                    var qrCodeArray = q.GetMaskedQRCode(version, qrCode, errorLevel, maskIndex);
                    Bitmap qrCodeBitmap = QR.ConvertToBitmap(qrCodeArray);

                    QR.ShowQRCodePopup(this,qrCodeBitmap);
                }
            }
        }


        private void alimentareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProducts("Alimentar");
        }

        private void nealimentareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProducts("Nealimentar");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            cos.Visible = !cos.Visible;
            if (cos.Visible)
            {
                cos.VirtualListSize = cosProdus.Count +1;
                cos.Invalidate();
            }
        }

        private void cos_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            e.Graphics.DrawString(e.Header.Text, new Font(e.Font, FontStyle.Bold), Brushes.Black, e.Bounds);
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
                e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }
        }

        private void cos_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (cosProdus != null)
            {
                if (e.ItemIndex < cosProdus.Count)
                {
                    Produs produs = cosProdus.Keys.ElementAt(e.ItemIndex);
                    int quantity = cosProdus[produs];

                    ListViewItem item = new ListViewItem((e.ItemIndex + 1).ToString());
                    item.SubItems.Add($"{produs.Denumire} ({quantity}x)");
                    item.SubItems.Add($"{(produs.Pret * quantity).ToString("F2")} RON");
                    item.SubItems.Add("plus");
                    item.SubItems.Add("minus");
                    item.Tag = produs;
                    e.Item = item;
                }
                else if (e.ItemIndex == cosProdus.Count)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add("Total:");
                    item.SubItems.Add($"{CalculateTotalPrice().ToString("F2")} RON");
                    item.SubItems.Add("buy");
                    item.SubItems.Add("");
                    e.Item = item;
                }
            }
        }

        private void cos_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ItemIndex == cosProdus.Count)
            {
                if (e.ColumnIndex == 3)
                {
                    if (File.Exists("buy.png"))
                    {
                        using (Image img = Image.FromFile("buy.png"))
                        {
                            int iconX = e.Bounds.X + (e.Bounds.Width - 30) / 2;
                            int iconY = e.Bounds.Y + (e.Bounds.Height - 30) / 2;
                            Rectangle destRect = new Rectangle(iconX, iconY, 30, 30);
                            e.Graphics.DrawImage(img, destRect);
                        }
                    }
                }
                else
                {
                    e.DrawDefault = true;
                }
                return;
            }
            if ((e.ColumnIndex == 3 || e.ColumnIndex == 4) && e.ItemIndex < cosProdus.Count)
            {
                string path;
                if(e.ColumnIndex == 3)
                {
                    path = "add-circle-plus.png";
                }
                else
                {
                    path = "minus-circle-outline.png";
                }
                if (File.Exists(path))
                {
                    using (Image img = Image.FromFile(path))
                    {
                        int iconX = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                        int iconY = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                        e.Graphics.DrawImage(img, new Point(iconX, iconY));
                    }
                }
          
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void cos_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = cos.HitTest(e.X, e.Y);

            if (info.SubItem != null )
            {
                Produs p = info.Item.Tag as Produs;
                if (p != null)
                {
                    if (info.SubItem.Text == "plus")
                    {
                        cosProdus[p]++;
                    }
                    else if (info.SubItem.Text == "minus")
                    {
                        if (cosProdus[p] == 1)
                        {
                            cosProdus.Remove(p);
                        }
                        else
                        {
                            cosProdus[p] -= 1;
                        }
                    }
                    
                }
                else if (info.SubItem.Text == "buy" && cosProdus.Count > 0)
                {
                    if (currentClient.Adresa.InformationsAreCompleted())
                    {
                        this.Hide();
                        using(Checkout co = new Checkout())
                        {
                            co.ShowDialog();
                            
                        }
                        
                    }
                    else
                    {
                        this.Hide();
                        using (MyAccount ma = new MyAccount())
                        {
                            CompleteInformations?.Invoke(this, EventArgs.Empty);
                            ma.ShowDialog();
                            

                        }
                    }
                }
                cos.VirtualListSize = cosProdus.Count + 1;
                cos.Invalidate();
            }
                
            
        }
        private float CalculateTotalPrice()
        {
            return cosProdus.Sum(kvp => kvp.Key.Pret * kvp.Value);
        }
        
        private void ClearCos(object s,EventArgs e)
        {
            
            cosProdus.Clear();
            cos.VirtualListSize = cosProdus.Count + 1;
            cos.Items.Clear();
            cos.Invalidate();
        }

        private void info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proiect Realizat de catre:\nStefu Alexandru Marian , Grupa M522\nFlorentina Panaintescu , Grupa M524\nVeronica Horopciuc , Grupa M521");
        }
    }

}
