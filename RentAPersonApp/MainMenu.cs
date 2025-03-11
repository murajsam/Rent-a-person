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
    public partial class MainMenu : Form
    {
        private readonly ProviderBusiness providerBusiness;
        private readonly ReservationBusiness reservationBusiness;
        private Provider provider = null;
        private User user = null;

        public MainMenu()
        {
            this.providerBusiness = new ProviderBusiness();
            this.reservationBusiness = new ReservationBusiness();
            InitializeComponent();
        }
        public MainMenu(Provider provider)
        {
            InitializeComponent();
            this.providerBusiness = new ProviderBusiness();
            this.reservationBusiness = new ReservationBusiness();
            this.provider = provider;
        }

        public MainMenu(User user)
        {
            InitializeComponent();
            this.providerBusiness = new ProviderBusiness();
            this.reservationBusiness = new ReservationBusiness();
            this.user = user;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            labelWelcome.Text += (user != null ? user.UserDetails.Username : provider.ProviderDetails.Username) + "!!!";
            if (user == null) //Provider je
            {
                buttonRent.Hide();
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void buttonService_Click(object sender, EventArgs e)
        {

            if (user != null)
            {
                TermsOfService service = new TermsOfService(user);
                this.Show();
                service.Show();
            }
            else
            {
                TermsOfService service = new TermsOfService(provider);
                this.Show();
                service.Show();
            }


        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {

             if (user != null)
            {
                Info info = new Info(user);
                this.Hide();
                info.Show();
            }
            else
            {
                Info info = new Info(provider);
                this.Hide();
                info.Show();
            }

        }

        private void buttonEditProfile_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                EditProfile editUser = new EditProfile(user);
                this.Hide();
                editUser.Show();
            }
            else
            {
                EditProfile editProvider = new EditProfile(provider);
                this.Hide();
                editProvider.Show();
            }
        }

        private void buttonReservations_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                ReservationMenu reservationMenu = new ReservationMenu(user);
                this.Hide();
                reservationMenu.Show();
            }
            else
            {
                ReservationMenu reservationMenu = new ReservationMenu(provider);
                this.Hide();
                reservationMenu.Show();
            }
        }

        private void buttonRent_Click(object sender, EventArgs e)
        {
            RentMenu rentMenu = new RentMenu(user);
            this.Hide();
            rentMenu.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
