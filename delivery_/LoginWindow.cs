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
    
    public partial class LoginWindow : Form
    {
        string username;
        string password;
        User deliveryUser;

        public LoginWindow()
        {
            InitializeComponent();
            //ServerConnection connection = new ServerConnection();
            //connection.StartClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = uname.Text;
            password = pword.Text;
            ServerConnection server = new ServerConnection();
            deliveryUser = server.getUser(username,password, UserType.DeliveryPerson);

            if (deliveryUser.empty)
            {
                System.Windows.Forms.MessageBox.Show("Felhasználónév vagy jelszó hibás!");
            }
            else
            {
                MainWindow mainWindow = new MainWindow(this, deliveryUser);
                mainWindow.Show();
                this.Hide();
            }
        }

        private void registBt_Click(object sender, EventArgs e)
        {
            RegisterWindow register = new RegisterWindow(this);
            register.Show();
            this.Hide();
        }
    }
}
