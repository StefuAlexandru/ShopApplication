
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen
{
    public partial class LoginForm : Form
    {
        public static event EventHandler<Client> LoggedIn;
        public static bool _isLoginSuccessful = false;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !password.UseSystemPasswordChar;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (SignUpForm suf = new SignUpForm())
            {
                suf.ShowDialog();
            }
            this.Close();
        }

        private void login_Click(object sender, EventArgs e)
        {
            string enteredUsername = username.Text;
            string enteredPassword = password.Text;

            Client client = Client.LoadFromDatabase(enteredUsername);
            if(client != null)
            {
                if (client.VerifyPassword(enteredPassword))
                {
                    _isLoginSuccessful = true;
                    LoggedIn?.Invoke(this, client);             
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Parola incorecta!");
                }
            }
            else
            {
                MessageBox.Show("Nu exista informatii despre acest utilizator!");
            }
        }


        private void LoginForm_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (!_isLoginSuccessful)
            {
                SignUpForm.returnToMainPage?.Invoke(this, true);
            }
        }
    }
}