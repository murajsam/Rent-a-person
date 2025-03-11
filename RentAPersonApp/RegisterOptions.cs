using DataLayer.Models;
using System;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class RegisterOptions : Form
    {
        public RegisterOptions()
        {
            InitializeComponent();
        }

        private void buttonJoinAsProvider_Click(object sender, EventArgs e)
        {
            RegisterProvider provider = new RegisterProvider();
            this.Hide();
            provider.Show();
        }

        private void buttonJoinAsUser_Click(object sender, EventArgs e)
        {
            RegisterUser user = new RegisterUser();
            this.Hide();
            user.Show();
        }

        private void buttonBackToLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
