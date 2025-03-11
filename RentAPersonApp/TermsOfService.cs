using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class TermsOfService : Form
    {

        private User user = null;
        private Provider provider = null;
        private MainMenu mainMenu;   
 


        public TermsOfService(object o)
        {
            if (o.GetType() == typeof(Provider))
            {
                this.provider = (Provider)o;
                InitializeComponent();
            }
            else if (o.GetType() == typeof(User))
            { 
                this.user = (User)o;
                InitializeComponent();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                mainMenu = new MainMenu(user);
                this.Hide();
                mainMenu.Show();
            }
            else if (provider != null)
            {
                mainMenu = new MainMenu(provider);
                this.Hide();
                mainMenu.Show();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }


    }
}
