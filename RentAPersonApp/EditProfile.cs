using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentAPersonApp
{
    public partial class EditProfile : Form
    {
        private Provider provider = null;
        private User user = null;
        private readonly ProviderBusiness providerBusiness;
        private readonly UserBusiness userBusiness;
        private messageBoxRentAPerson message;
        public EditProfile(object o)
        {
            if(o.GetType() == typeof(Provider))
            {
                InitializeComponent();
                this.provider = (Provider) o;
                this.providerBusiness = new ProviderBusiness();
            }
            else if(o.GetType() == typeof(User))
            {
                InitializeComponent();
                this.user = (User) o;
                this.userBusiness = new UserBusiness();

            }
        }

        private void EditProvider_Load(object sender, EventArgs e)
        {
            if (provider != null)
            {
                textBoxUsername.Show();
                textBoxFirstName.Show();
                textBoxLastName.Show();
                textBoxAddress.Show();
                textBoxPhone.Show();
                textBoxPassword.Show();
                pictureBoxProvider.Show();
                comboBoxProviderType.Show();
                UploadAvatarBtn.Show();
                textBoxPricePerHour.Show();

                textBoxUsername.Text = provider.ProviderDetails.Username;
                textBoxFirstName.Text = provider.ProviderDetails.FirstName;
                textBoxLastName.Text = provider.ProviderDetails.LastName;
                textBoxAddress.Text = provider.ProviderDetails.Address;
                textBoxPhone.Text = provider.ProviderDetails.Phone;
                textBoxPassword.Text = provider.ProviderDetails.Password;
                textBoxEmail.Text = provider.ProviderDetails.Email;
                comboBoxProviderType.Text = provider.Type;
                textBoxPricePerHour.Text = provider.PricePerHour.ToString();

                MemoryStream memStream = new MemoryStream(provider.Avatar);
                Bitmap bitmap = new Bitmap(memStream);
                pictureBoxProvider.Image = bitmap;


            }
            else if (user != null)
            {
                textBoxUsername.Show();
                textBoxFirstName.Show();
                textBoxLastName.Show();
                textBoxAddress.Show();
                textBoxPhone.Show();
                textBoxPassword.Show();
                pictureBoxProvider.Hide();
                comboBoxProviderType.Hide();
                UploadAvatarBtn.Hide();
                textBoxPricePerHour.Hide();
                labelProviderType.Hide();
                labelPricePerHour.Hide();

                textBoxUsername.Text = user.UserDetails.Username;
                textBoxFirstName.Text = user.UserDetails.FirstName;
                textBoxLastName.Text = user.UserDetails.LastName;
                textBoxAddress.Text = user.UserDetails.Address;
                textBoxPhone.Text = user.UserDetails.Phone;
                textBoxPassword.Text = user.UserDetails.Password;
                textBoxEmail.Text = user.UserDetails.Email;

            }

        }

        private void UploadAvatarBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                pictureBoxProvider.Image = new Bitmap(openFileDialog.FileName);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            List<TextBox> listOfInputBoxes = new List<TextBox>();
            listOfInputBoxes.Add(textBoxFirstName);
            listOfInputBoxes.Add(textBoxLastName);
            listOfInputBoxes.Add(textBoxAddress);
            listOfInputBoxes.Add(textBoxPhone);
            listOfInputBoxes.Add(textBoxPassword);
            listOfInputBoxes.Add(textBoxEmail);
            listOfInputBoxes.Add(textBoxUsername);

            if (FormInputMgt.IsAnyTextBoxEmpty(listOfInputBoxes))
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje je prazno. Unesite vrednost i pokusajte ponovo");
                textBoxEmptyMsg.ShowDialog();
            }
            else if (pictureBoxProvider.Image == null || pictureBoxProvider == null)
            {
                messageBoxRentAPerson textBoxEmptyMsg = new messageBoxRentAPerson("Polje za sliku je prazno. Upload-ujte sliku i pokusajte ponovo.");
                textBoxEmptyMsg.ShowDialog();
            }
            else
            {

                if (provider != null)
                {
                    provider.ProviderDetails.Username = textBoxUsername.Text;
                    provider.ProviderDetails.FirstName = textBoxFirstName.Text;
                    provider.ProviderDetails.LastName = textBoxLastName.Text;
                    provider.ProviderDetails.Address = textBoxAddress.Text;
                    provider.ProviderDetails.Phone = textBoxPhone.Text;
                    provider.ProviderDetails.Email = textBoxEmail.Text;
                    provider.ProviderDetails.Password = textBoxPassword.Text;
                    provider.PricePerHour = Convert.ToDouble(textBoxPricePerHour.Text);
                    provider.Avatar = ConvertImageToBytes(pictureBoxProvider.Image);
                    provider.Type = comboBoxProviderType.GetItemText(comboBoxProviderType.SelectedItem).ToString();
                    if (!providerBusiness.UpdateProvider(provider))
                    {
                        message = new messageBoxRentAPerson("Profile has not been edited successfully.");
                        message.ShowDialog();
                    }
                    else
                        ShowMenuForm(provider);

                }
                else if (user != null)
                {
                    user.UserDetails.Username = textBoxUsername.Text;
                    user.UserDetails.FirstName = textBoxFirstName.Text;
                    user.UserDetails.LastName = textBoxLastName.Text;
                    user.UserDetails.Address = textBoxAddress.Text;
                    user.UserDetails.Phone = textBoxPhone.Text;
                    user.UserDetails.Email = textBoxEmail.Text;

                    if (!userBusiness.UpdateUser(user))
                    {
                        message = new messageBoxRentAPerson("Profile has not been edited successfully.");
                        message.ShowDialog();
                    }
                    else
                        ShowMenuForm(user);

                }
            }
        }
        private byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private void buttonBackToLogin_Click(object sender, EventArgs e)
        {
            if(user != null)
            {
                ShowMenuForm(user);
            }
            else if(provider != null) 
            {
                ShowMenuForm(provider);
            }

        }

        private void ShowMenuForm(object o)
        {
            if(o.GetType() == typeof(Provider))
            {
                this.Hide();
                MainMenu mainMenu = new MainMenu((Provider) o);
                mainMenu.Show();
            }else if(o.GetType() == typeof(User))
            {
                this.Hide();
                MainMenu mainMenu = new MainMenu((User)o);
                mainMenu.Show();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            FormInputMgt.CheckEmailFormat(textBoxEmail);
        }

        private void textBoxPhone_Validating(object sender, CancelEventArgs e)
        {
            FormInputMgt.CheckPhoneFormat(textBoxPhone);    
        }
    }
}