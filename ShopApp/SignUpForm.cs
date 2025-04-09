using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Examen
{
    public partial class SignUpForm : Form
    {
        private bool[] fields = new bool[5];
        private BackgroundWorker ErrorsWorker;
        private bool areDetailsCorrect = false;
        public static EventHandler<bool> returnToMainPage;
        public SignUpForm()
        {
            InitializeComponent();

            ErrorsWorker = new BackgroundWorker();
            ErrorsWorker.WorkerReportsProgress = true;
            ErrorsWorker.DoWork += ErrorsWorker_DoWork;
            ErrorsWorker.ProgressChanged += ErrorsWorker_ProgressChanged;
            ErrorsWorker.RunWorkerAsync();

        
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !password.UseSystemPasswordChar;
        }

        private void OpenLogin()
        {
            this.Hide();
            using (LoginForm lf = new LoginForm())
            {
                lf.ShowDialog();
            }
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            OpenLogin();
        }

        private void ErrorsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!areDetailsCorrect)
            {
                UpdateFieldsArray();
                ErrorsWorker.ReportProgress(0, new { Field = "Nume", IsValid = fields[0] });
                ErrorsWorker.ReportProgress(0, new { Field = "Prenume", IsValid = fields[1] });
                ErrorsWorker.ReportProgress(0, new { Field = "Email", IsValid = fields[2] });
                ErrorsWorker.ReportProgress(0, new { Field = "Username", IsValid = fields[3] });
                ErrorsWorker.ReportProgress(0, new { Field = "Password", IsValid = fields[4] });

                areDetailsCorrect = fields.All(field => field);
  
                Thread.Sleep(500);
            }
        }

        private void ErrorsWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progressInfo = (dynamic)e.UserState;
            string field = progressInfo.Field;
            bool isValid = progressInfo.IsValid;

            switch (field)
            {
                case "Nume":
                    txtNume.ForeColor = isValid ? Color.White : Color.Goldenrod;
                    break;
                case "Prenume":
                    txtPrenume.ForeColor = isValid ? Color.White : Color.Goldenrod;
                    break;
                case "Email":
                    txtEmail.ForeColor = isValid ? Color.White : Color.Goldenrod;
                    break;
                case "Username":
                    txtUsername.ForeColor = isValid ? Color.White : Color.Goldenrod;
                    break;
                case "Password":
                    txtPassword.ForeColor = isValid ? Color.White : Color.Goldenrod;
                    break;
            }
        
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            if (areDetailsCorrect)
            {
                if (Client.UsernameExists(username.Text))
                {
                    MessageBox.Show("Acest utilizator exista deja.");
                    txtUsername.ForeColor = Color.Goldenrod;
                }
                else
                {
                    Client client = new Client(nume.Text, prenume.Text, email.Text, username.Text, password.Text);
                    client.SaveToDatabase();
                   
                    OpenLogin();
                }
            }
            else
            {
                MessageBox.Show("Ceva nu a mers bine! Incearca din nou!");
            }
        }


        private bool IsValidEmail()
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email.Text);
                return address.Address == email.Text;
            }
            catch
            {
                return false;
            }

            
        }

        private bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        private void UpdateFieldsArray()
        {
            fields[0] = !string.IsNullOrEmpty(nume.Text) && IsNameValid(nume.Text);
            fields[1] = !string.IsNullOrEmpty(prenume.Text) && IsNameValid(prenume.Text);
            fields[2] = !string.IsNullOrEmpty(email.Text) && IsValidEmail();
            fields[3] = !string.IsNullOrEmpty(username.Text) && username.TextLength >= 8;
            fields[4] = password.TextLength >= 8;
        }


        private void SignUpForm_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (!LoginForm._isLoginSuccessful)
            {
                returnToMainPage?.Invoke(this, true);
            }
        }
    }
}
