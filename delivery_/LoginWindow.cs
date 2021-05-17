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

           // if(/*login ok*/)
            {
                    
            }
        }

        private void registBt_Click(object sender, EventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Hide();
        }
    }
}
