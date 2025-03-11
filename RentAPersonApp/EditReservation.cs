using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class EditReservation : Form
    {

        private readonly ReservationBusiness reservationBusiness;
        private readonly ProviderBusiness providerBusiness;
        private Reservation reservation;
        private Provider provider;
        private messageBoxRentAPerson message;
        private object o;
        public EditReservation(Reservation reservation, object o)
        {
            this.o = o;
            this.reservationBusiness = new ReservationBusiness();
            this.reservation = reservation;
            providerBusiness = new ProviderBusiness();
            this.provider = providerBusiness.GetProviderFromReservation(reservation);
            this.reservationBusiness = new ReservationBusiness();
            InitializeComponent();
        }

        private void buttonEditReservation_Click(object sender, EventArgs e)
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
                reservation.Status = comboBoxStatus.GetItemText(comboBoxStatus.SelectedItem).ToString();
                reservation.StartDate = dateTimePickerFrom.Value;
                reservation.EndDate = dateTimePickerTo.Value;

                if (!reservationBusiness.CheckForAcceptedReservations(reservation.ReservationId, reservation.ProviderId, dateTimePickerFrom.Value, dateTimePickerTo.Value) || reservation.Status == "declined")
                {
                    if (reservationBusiness.UpdateReservation(reservation) && (Math.Abs(reservation.StartDate.Hour - (reservation.EndDate.Hour)) > 1))
                    {
                        message = new messageBoxRentAPerson("Reservation has been edited succesfully!");
                        message.ShowDialog();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

                    else
                    {
                        message = new messageBoxRentAPerson("Reservation has not been edited succesfully!");
                        message.ShowDialog();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    message = new messageBoxRentAPerson("There is already pending/accepted reservation for this date" + (o.GetType() == typeof(Provider) ? "!" : ", please select other date!"));
                    message.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                labelPrice.ForeColor = Color.Gold;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void getReservationData() 
        {
            richTextBoxDescription.Text = reservation.Description;
            textBoxLocation.Text = reservation.Location;
            labelPrice.Text += (Math.Abs(Convert.ToInt32((Convert.ToDateTime(reservation.StartDate) - Convert.ToDateTime(reservation.EndDate)).TotalHours) * (provider.PricePerHour))).ToString() + "¥";
            comboBoxStatus.SelectedItem = reservation.Status;
            dateTimePickerFrom.Value = reservation.StartDate;
            dateTimePickerTo.Value = reservation.EndDate;

            if (reservation.Status != "pending")
            {
                richTextBoxDescription.Enabled = false;
                textBoxLocation.Enabled = false;
                comboBoxStatus.Enabled = false;
                dateTimePickerFrom.Enabled = false;
                dateTimePickerTo.Enabled = false;
                buttonEditReservation.Enabled = false;
            }
            labelPrice.ForeColor = Color.Gold;
        }

        private void EditReservation_Load(object sender, EventArgs e)
        {
            labelPrice.ForeColor = Color.Gold;
            if (o.GetType() == typeof(Provider))
            {
                richTextBoxDescription.Enabled = false;
                textBoxLocation.Enabled = false;
                dateTimePickerFrom.Enabled = false;
                dateTimePickerTo.Enabled = false;
            }
            else
            {
                comboBoxStatus.Enabled = false;
            }
            getReservationData();
            
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            labelPrice.Text = "Price: ";
            labelPrice.Text += (Math.Abs(Convert.ToInt32((Convert.ToDateTime(dateTimePickerFrom.Value) - Convert.ToDateTime(dateTimePickerTo.Value)).TotalHours) * (provider.PricePerHour))).ToString() + "¥";
            labelPrice.ForeColor = Color.Gold;
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            labelPrice.Text = "Price: ";
            labelPrice.Text += (Math.Abs(Convert.ToInt32((Convert.ToDateTime(dateTimePickerFrom.Value) - Convert.ToDateTime(dateTimePickerTo.Value)).TotalHours) * (provider.PricePerHour))).ToString() + "¥";
            labelPrice.ForeColor = Color.Gold;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
