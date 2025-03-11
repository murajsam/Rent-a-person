using BusinessLayer;
using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace RentAPersonApp
{
    public class FormInputMgt
    {
        public static bool IsAnyTextBoxEmpty(List<TextBox> listOfTextBoxes)
        {
            foreach (TextBox textBox in listOfTextBoxes)
            {
                if (textBox.Text == "")
                    return true;
            }
            return false;
        }

        public static bool IsTextBoxEmpty(TextBox textBox) {
            if (textBox.Text == "")
                return true;
            else
                return false;
        }

        public static bool IsRichTextBoxEmpty(RichTextBox textBox)
        {
            if (textBox.Text == "")
                return true;
            else
                return false;
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public  static bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        public static bool CheckPhoneFormat(TextBox textBox)
        {
            if (!FormInputMgt.IsNumeric(textBox.Text))
            {
                messageBoxRentAPerson phoneInputError = new messageBoxRentAPerson("Pogresan format! Unesite samo brojeve i pokusajte ponovo.");
                phoneInputError.ShowDialog();
                return false;
            }
            return true;
        }

        public static bool CheckEmailFormat(TextBox textBox)
        {
            if (!FormInputMgt.IsValidEmail(textBox.Text))
            {
                messageBoxRentAPerson invalidFormatEmail = new messageBoxRentAPerson("Format E-maila netacan. Koristite example@mail.com i pokusajte ponovo");
                invalidFormatEmail.ShowDialog();
                return false;
            }
            else
                return true;
        }

    }
}
