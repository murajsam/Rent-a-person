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

namespace RentAPersonApp
{
    public partial class ReservationMenu : Form
    {
        private readonly ReservationBusiness reservationBusiness;
        private readonly ProviderBusiness providerBusiness;
        private readonly UserBusiness userBusiness;
        private User user = null;
        private Provider provider = null;
        private List<Reservation> reservations;
        private PictureBox selectedReservationItem = null;
        private Reservation selectedReservation;
        public ReservationMenu(object o)
        {
            if (o.GetType() == typeof(Provider))
            {
                this.providerBusiness = new ProviderBusiness();
                this.userBusiness = new UserBusiness();
                this.reservations = new List<Reservation>();
                this.reservationBusiness = new ReservationBusiness();
                this.provider = (Provider)o;
                InitializeComponent();
            }
            else if (o.GetType() == typeof(User))
            {
                this.providerBusiness = new ProviderBusiness();
                this.reservations = new List<Reservation>();
                this.userBusiness = new UserBusiness();
                this.reservationBusiness = new ReservationBusiness();
                this.user = (User)o;
                InitializeComponent();
            } 
        }

        public ReservationMenu()
        {
            InitializeComponent();
        }

        public void showReservations(string param)
        {
            flowLayoutPanelReservations.Controls.Clear();

            if (provider != null)
            {
                reservations = reservationBusiness.GetAllReservationsFromProvider(provider);
            }
            else
            {
                reservations = reservationBusiness.GetAllReservationsFromUser(user);
            }
            

            if (reservations != null)
            {
                switch (param)
                {
                    case "pending":
                        reservations = reservations.Where(reservation => reservation.Status == "pending").ToList();
                        break;
                    case "declined":
                        reservations = reservations.Where(reservation => reservation.Status == "declined").ToList();
                        break;
                    case "accepted":
                        reservations = reservations.Where(reservation => reservation.Status == "accepted").ToList();
                        break;
                    default: break;
                }
                foreach (var reservation in reservations)
                {
                    PictureBox reservationItem = new PictureBox();
                    reservationItem.Width = 370;
                    reservationItem.Height = 300;
                    reservationItem.BackgroundImageLayout = ImageLayout.Stretch;
                    reservationItem.BorderStyle = BorderStyle.Fixed3D;

                    Label reservationInfo = new Label();
                    reservationInfo.Text =
                         providerBusiness.GetProviderFromReservation(reservation).ProviderDetails.FirstName + " " + providerBusiness.GetProviderFromReservation(reservation).ProviderDetails.LastName
                         + ": "
                         + (Math.Abs(Convert.ToInt32((Convert.ToDateTime(reservation.StartDate) - Convert.ToDateTime(reservation.EndDate)).TotalHours) * (providerBusiness.GetProviderFromReservation(reservation).PricePerHour))).ToString() + "¥"
                         + " - "
                         + reservation.Location;
                    reservationInfo.Font = new Font(reservationInfo.Font.FontFamily, 10, FontStyle.Bold);
                    Color color = Color.FromArgb(49, 50, 50);
                    reservationInfo.ForeColor = Color.Black;
                    reservationInfo.Dock = DockStyle.Bottom;

                    Label reservationStatus = new Label();
                    reservationStatus.Text = reservation.Status;
                    reservationStatus.ForeColor = Color.Black;
                    reservationStatus.Font = new Font(reservationStatus.Font.FontFamily, 10, FontStyle.Bold);
                    switch (reservation.Status)
                    {
                        case "declined":
                            reservationInfo.BackColor = Color.Red;
                            reservationStatus.BackColor = Color.Red;
                            break;
                        case "accepted":
                            reservationInfo.BackColor = Color.Green;
                            reservationStatus.BackColor = Color.Green; 
                            break;
                        default:
                            reservationInfo.BackColor = Color.Gold;
                            reservationStatus.BackColor = Color.Gold;
                            break;
                    }

                    MemoryStream memStream = new MemoryStream(providerBusiness.GetProviderFromReservation(reservation).Avatar);
                    Bitmap bitmap = new Bitmap(memStream);
                    reservationItem.BackgroundImage = bitmap;

                    reservationItem.Controls.Add(reservationInfo);
                    reservationItem.Controls.Add(reservationStatus);

                    reservationItem.Click += (sender, e) => reservationItem_Click(sender, e, reservationItem, reservation);
                    reservationInfo.Click += (sender, e) => reservationItem_Click(sender, e, reservationItem, reservation);
                    reservationItem.Cursor = Cursors.Hand;
                    flowLayoutPanelReservations.Controls.Add(reservationItem);
                }
            }
            
        }

        private void reservationItem_Click(object sender, EventArgs e, PictureBox reservationItem, Reservation reservation)
        {
            if (selectedReservationItem != null)
            {
                selectedReservationItem.BorderStyle = BorderStyle.None;
            }
            reservationItem.BorderStyle = BorderStyle.Fixed3D;
            reservationItem.Padding = new Padding(5);

            selectedReservationItem = reservationItem;
            selectedReservation = reservation;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            switch(comboBoxFilter.GetItemText(comboBoxFilter.SelectedItem).ToString())
            {
                case "pending": showReservations("pending");break;
                case "declined": showReservations("declined"); break;
                case "accepted": showReservations("accepted"); break;
                default: showReservations("all"); break;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (provider != null)
            {
                MainMenu menu = new MainMenu(provider);
                this.Hide();
                menu.Show();
            }
            else
            {
                MainMenu menu = new MainMenu(user);
                this.Hide();
                menu.Show();
            }
        }

        private void ReservationMenu_Load(object sender, EventArgs e)
        {
            comboBoxFilter.SelectedIndex = 0;
            showReservations("all");
        }

        private void buttonEditReservation_Click(object sender, EventArgs e)
        {
            if (provider != null)
            {
                EditReservation editReservation = new EditReservation(selectedReservation, provider);
                //editReservation.ShowDialog();
                if (editReservation.ShowDialog() == DialogResult.OK)
                {
                    comboBoxFilter.SelectedIndex = 0;
                    showReservations("all");
                }

            }
            else
            {
                EditReservation editReservation = new EditReservation(selectedReservation, user);
                if (editReservation.ShowDialog() == DialogResult.OK)
                {
                    comboBoxFilter.SelectedIndex = 0;
                    showReservations("all");
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
