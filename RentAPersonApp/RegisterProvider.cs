using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class RegisterProvider : Form
    {
        private readonly PersonBusiness peopleBusiness;
        private readonly ProviderBusiness providerBusiness;

        private messageBoxRentAPerson message;
        public RegisterProvider()
        {
            this.peopleBusiness = new PersonBusiness();
            this.providerBusiness = new ProviderBusiness();
            InitializeComponent();
        }

        private byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Image ConvertBytesToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string RegistrationSuccessMessage = "Uspesno ste se registrovali kao Provider.";
            string RegistrationUnsuccessfulMessage = "Greska! Registracija korisnika neuspesna. Pokusajte ponovo.";

            Person person = new Person();
            Provider provider = new Provider();

            List<TextBox> listOfInputBoxes = new List<TextBox>();

            listOfInputBoxes.Add(textBoxFirstName);
            listOfInputBoxes.Add(textBoxLastName);
            listOfInputBoxes.Add(textBoxUsername);
            listOfInputBoxes.Add(textBoxAddress);
            listOfInputBoxes.Add(textBoxPassword);
            listOfInputBoxes.Add(textBoxPhone);
            listOfInputBoxes.Add(textBoxEmail);
            listOfInputBoxes.Add(textBoxPricePerHour);


            if (FormInputMgt.IsAnyTextBoxEmpty(listOfInputBoxes))
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje je prazno. Unesite vrednost i pokusajte ponovo");
                textBoxEmptyMsg.ShowDialog();
                return;
            }
            if (pictureBoxProvider.Image == null || pictureBoxProvider == null)
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje za sliku je prazno. Upload-ujte sliku i pokusajte ponovo.");
                textBoxEmptyMsg.ShowDialog();
                return;
            }
            if (!FormInputMgt.CheckPhoneFormat(textBoxPhone))
                return;

            if (!FormInputMgt.CheckEmailFormat(textBoxEmail))
                return;

            person.FirstName = textBoxFirstName.Text;
            person.Address = textBoxAddress.Text;
            person.LastName = textBoxLastName.Text;
            person.Username = textBoxUsername.Text;
            person.Password = textBoxPassword.Text;
            person.Phone = textBoxPhone.Text;
            person.Email = textBoxEmail.Text;

            provider.ProviderDetails = person;
            provider.Type = comboBoxProviderType.GetItemText(comboBoxProviderType.SelectedItem).ToString();
            provider.Avatar = ConvertImageToBytes(pictureBoxProvider.Image);
            provider.PricePerHour = Convert.ToDouble(textBoxPricePerHour.Text);

            if (this.peopleBusiness.RegisterPerson(person))
            {
                if (this.providerBusiness.RegisterProvider(provider))
                {
                    message = new messageBoxRentAPerson(RegistrationSuccessMessage);
                    message.ShowDialog();
                    Login login = new Login();
                    this.Hide();
                    login.Show();
                }
                else
                {
                    message = new messageBoxRentAPerson(RegistrationUnsuccessfulMessage);
                    message.ShowDialog();
                }
            }
            else
            {
                message = new messageBoxRentAPerson(RegistrationUnsuccessfulMessage);
                message.ShowDialog();
            }
        }
        private void UploadAvatarBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxProvider.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void buttonBackToLogin_Click(object sender, EventArgs e)
        {
            RegisterOptions registerOptions = new RegisterOptions();
            this.Hide();
            registerOptions.Show();
        }

        private void labelProviderType_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxProvider_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            FormInputMgt.CheckPhoneFormat(textBoxPhone);
        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelPhone_Click(object sender, EventArgs e)
        {

        }

        private void labelAddress_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelEmail_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelLastName_Click(object sender, EventArgs e)
        {

        }

        private void labelPassword_Click(object sender, EventArgs e)
        {

        }

        private void labelFirstName_Click(object sender, EventArgs e)
        {

        }

        private void labelRegister_Click(object sender, EventArgs e)
        {

        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormInputMgt.CheckEmailFormat(textBoxEmail);
        }

        private void textBoxPhone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormInputMgt.CheckPhoneFormat(textBoxPhone);
        }
    }
}
