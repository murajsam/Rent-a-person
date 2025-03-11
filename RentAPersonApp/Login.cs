using BusinessLayer;
using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class Login : Form
    {
        private MainMenu mainMenu;
        private readonly ProviderBusiness providerBusiness;
        private readonly UserBusiness userBusiness;
        private string username;
        private string password;
        private messageBoxRentAPerson message;

        private User user;
        private Provider provider;
        public Login()
        {
            this.user = new User(); 
            this.provider = new Provider(); 
            this.providerBusiness = new ProviderBusiness();
            this.userBusiness = new UserBusiness();
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string LogInErrorText = "Neuspesna prijava. Pokusajte ponovo.";

            List<TextBox> listOfTextBoxes = new List<TextBox>();
            listOfTextBoxes.Add(textBoxUsername);
            listOfTextBoxes.Add(textBoxPassword);

            if (FormInputMgt.IsAnyTextBoxEmpty(listOfTextBoxes))
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje je prazno. Unesite vrednost i pokusajte ponovo");
                textBoxEmptyMsg.ShowDialog();
            }
            else
            {

                username = textBoxUsername.Text;
                password = textBoxPassword.Text;

                user = this.userBusiness.LoginUser(username, password);
                provider = this.providerBusiness.LoginProvider(username, password);

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
                    message = new messageBoxRentAPerson(LogInErrorText);
                    message.ShowDialog();
                }
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegisterOptions registerOptions = new RegisterOptions();
            this.Hide();
            registerOptions.Show();
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
