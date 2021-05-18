using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delivery_
{
    public partial class RegisterWindow : Form
    {
        LoginWindow login;
        public RegisterWindow(LoginWindow login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void registerBt_Click(object sender, EventArgs e)
        {
            User regUser = new User(uname.Text, pword.Text, knev.Text, vnev.Text, tszam.Text, varos.Text, irszam.Text, utca.Text + " " + hszam.Text, "", 2, email.Text);
            ServerConnection server = new ServerConnection();
            server.registerUser(regUser);
            if (regUser != null)
            {
                System.Windows.Forms.MessageBox.Show("Regisztráció sikeres!");
                this.Hide();
                login.Show();
            }
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            //LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
            //this.Hide();
        }
    }
}
