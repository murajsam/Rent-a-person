using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class RegisterUser : Form
    {
        private readonly PersonBusiness peopleBusiness;
        private readonly UserBusiness userBusiness;

        private messageBoxRentAPerson message;

        public RegisterUser()
        {
            this.peopleBusiness = new PersonBusiness();
            this.userBusiness = new UserBusiness();
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string RegistrationSuccessMessage = "Uspesno ste se registrovali kao Korisnik.";
            string RegistrationUnsuccessfulMessage = "Greska! Registracija korisnika neuspesna. Pokusajte ponovo.";

            Person person = new Person();
            User user = new User();

            List<TextBox> listOfInputBoxes = new List<TextBox>();

            listOfInputBoxes.Add(textBoxFirstName);
            listOfInputBoxes.Add(textBoxLastName);
            listOfInputBoxes.Add(textBoxUsername);
            listOfInputBoxes.Add(textBoxPassword);
            listOfInputBoxes.Add(textBoxPhone);
            listOfInputBoxes.Add(textBoxEmail);

            if (FormInputMgt.IsAnyTextBoxEmpty(listOfInputBoxes))
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje je prazno. Unesite vrednost i pokusajte ponovo");
                textBoxEmptyMsg.ShowDialog();
            }
            else
            {

                person.Username = textBoxUsername.Text;
                person.Password = textBoxPassword.Text;
                person.Phone = textBoxPhone.Text;
                person.LastName = textBoxLastName.Text;
                person.FirstName = textBoxFirstName.Text;
                person.Address = textBoxAddress.Text;
                person.Email = textBoxEmail.Text;

                user.UserDetails = person;

                if (this.peopleBusiness.RegisterPerson(person))
                    if (this.userBusiness.RegisterUser(user))
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
                else
                {
                    message = new messageBoxRentAPerson(RegistrationUnsuccessfulMessage);
                    message.ShowDialog();
                }
            }
        }

        private void buttonBackToLogin_Click(object sender, EventArgs e)
        {
            RegisterOptions registerOptions = new RegisterOptions();
            this.Hide();
            registerOptions.Show();
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
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
