using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer.Models;

namespace RentAPersonApp
{
    public partial class RentMenu : Form
    {
        private User user;
        private readonly ProviderBusiness providerBusiness;

        private Provider sendProviderObjToForm;
        private PictureBox selectedPersonItem = null;

        public RentMenu(User user)
        {
            InitializeComponent();
            this.user = user;
            this.sendProviderObjToForm = new Provider();
            providerBusiness = new ProviderBusiness();
        }

        private void DisplayProviders()
        {
            flowLayoutPanelProviders.Controls.Clear();
            foreach (var provider in providerBusiness.GetAllProviders())
            {
                PictureBox providerItem = new PictureBox();
                providerItem.Width = 280;
                providerItem.Height = 250;
                providerItem.BackgroundImageLayout = ImageLayout.Stretch;
                providerItem.BorderStyle = BorderStyle.Fixed3D;

                Label providerName = new Label();
                providerName.Text = provider.ProviderDetails.FirstName + " " + provider.ProviderDetails.LastName;
                providerName.Font = new Font(providerName.Font.FontFamily, 10, FontStyle.Bold);
                Color color = Color.FromArgb(49, 50, 50);
                providerName.BackColor = Color.Gold;
                providerName.ForeColor = color;
                providerName.Dock = DockStyle.Bottom;

                Label providerPrice = new Label();
                providerPrice.Text = provider.PricePerHour.ToString() + "¥/hour";
                providerPrice.BackColor = Color.Gold;
                providerPrice.ForeColor = color;
                providerPrice.Font = new Font(providerPrice.Font.FontFamily, 10, FontStyle.Bold);

                MemoryStream memStream = new MemoryStream(provider.Avatar);
                Bitmap bitmap = new Bitmap(memStream);
                providerItem.BackgroundImage = bitmap;

                providerItem.Controls.Add(providerPrice);
                providerItem.Controls.Add(providerName);

                providerName.Click += (sender, e) => ProviderItem_Click(sender, e, provider, providerItem);
                providerPrice.Click += (sender, e) => ProviderItem_Click(sender, e, provider, providerItem);
                providerItem.Click += (sender, e) => ProviderItem_Click(sender, e, provider, providerItem);

                providerItem.Cursor = Cursors.Hand;
                flowLayoutPanelProviders.Controls.Add(providerItem);
            }
        }

        private void ProviderItem_Click(object sender, EventArgs e, Provider provider, PictureBox clickedPersonItem)
        {
            if (selectedPersonItem != null)
                selectedPersonItem.BorderStyle = BorderStyle.None;

            clickedPersonItem.BorderStyle = BorderStyle.Fixed3D;
            clickedPersonItem.BackColor = Color.Red;
            clickedPersonItem.Padding = new Padding(5);

            selectedPersonItem = clickedPersonItem;
            sendProviderObjToForm = provider;
        }

        private void buttonRent_Click(object sender, EventArgs e)
        {
            this.Hide();
            RentAProvider rentAProvider = new RentAProvider(sendProviderObjToForm, user);
            rentAProvider.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu(user);
            this.Hide();
            menu.Show();
        }

        private void RentMenu_Load(object sender, EventArgs e)
        {
            DisplayProviders();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
