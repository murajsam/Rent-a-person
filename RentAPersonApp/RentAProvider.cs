using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentAPersonApp
{
    public partial class RentAProvider : Form
    {
        private Provider provider;
        private User user;
        private ReservationBusiness reservationBusiness;
        public RentAProvider()
        {
            InitializeComponent();
        }
        public RentAProvider(Provider provider, User user)
        {
            InitializeComponent();
            this.provider = provider;
            this.user = user;
            reservationBusiness = new ReservationBusiness();
        }
        private void RentAProvider_Load(object sender, EventArgs e)
        {
            SetProviderInfoLabels();
        }

        private static Bitmap ConvertByteToBitmap(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bitmap = new Bitmap(mStream, false);
            mStream.Dispose();
            return bitmap;
        }

        private void buttonReserve_Click(object sender, EventArgs e)
        {
            RegisterReservation editReservation = new RegisterReservation(provider, user);
            editReservation.ShowDialog();
        }

        private void SetProviderInfoLabels()
        {
            labelFullname.Text += String.Concat(provider.ProviderDetails.FirstName, ' ', provider.ProviderDetails.LastName);
            labelAddress.Text += provider.ProviderDetails.Address;
            labelEmail.Text += provider.ProviderDetails.Email;
            labelPhone.Text += provider.ProviderDetails.Phone;

            pictureBoxProvider.Image = ConvertByteToBitmap(provider.Avatar);
        }

        private void buttonBack_Click(object sender, EventArgs e)
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
