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
    public partial class RegisterReservation : Form
    {
        private readonly ReservationBusiness reservationBusiness;
        private Reservation reservation;                                                                            
        private Provider provider;
        private User user;
        private messageBoxRentAPerson message;

        public RegisterReservation(Provider provider, User user)
        {
            this.reservation = new Reservation();
            this.user = user;
            this.provider = provider;
            this.reservationBusiness = new ReservationBusiness();
            InitializeComponent();
        }

        private void buttonCreateReservation_Click(object sender, EventArgs e)
        {
            if ((FormInputMgt.IsTextBoxEmpty(textBoxLocation)) || (FormInputMgt.IsRichTextBoxEmpty(richTextBoxDescription)))
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje je prazno. Unesite vrednost i pokusajte ponovo");
                textBoxEmptyMsg.ShowDialog();
            }
            else
            {
                reservation.Description = richTextBoxDescription.Text;
                reservation.Location = textBoxLocation.Text;
                reservation.Status = "pending";
                reservation.StartDate = dateTimePickerFrom.Value;
                reservation.EndDate = dateTimePickerTo.Value;
                reservation.ProviderId = provider.ProviderId;
                reservation.UserId = user.UserId;

                if (!reservationBusiness.CheckForAcceptedReservations(-1, provider.ProviderId, dateTimePickerFrom.Value, dateTimePickerTo.Value))
                {
                    if (reservationBusiness.CreateReservation(reservation) && (Math.Abs(reservation.StartDate.Hour - (reservation.EndDate.Hour)) > 1))
                    {
                        message = new messageBoxRentAPerson("Reservation has been created succesfully!");
                        message.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        message = new messageBoxRentAPerson("Reservation has not been created succesfully!");
                        message.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    message = new messageBoxRentAPerson("There is already pending/accepted reservation for this date, please select other date!");
                    message.ShowDialog();
                    this.Close();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            labelPrice.Text = "Price: ";
            labelPrice.Text += (Math.Abs(Convert.ToInt32((Convert.ToDateTime(dateTimePickerFrom.Value) - Convert.ToDateTime(dateTimePickerTo.Value)).TotalHours) * (provider.PricePerHour))).ToString() + "¥";
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            labelPrice.Text = "Price: ";
            labelPrice.Text += (Math.Abs(Convert.ToInt32((Convert.ToDateTime(dateTimePickerFrom.Value) - Convert.ToDateTime(dateTimePickerTo.Value)).TotalHours) * (provider.PricePerHour))).ToString() + "¥";
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterReservation_Load(object sender, EventArgs e)
        {
            labelPrice.ForeColor = Color.Gold;
        }
    }
}
