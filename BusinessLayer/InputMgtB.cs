using DataLayer.Models;
using System;

namespace BusinessLayer
{
    public class InputMgtB
    {
        public static Boolean CheckIfStringNotEmpty(string value)
        {
            return (value != null && value.Length > 0);
        }
        public static Boolean CheckIfPersonFieldsNotEmpty(Person person)
        {
            return (CheckIfStringNotEmpty(person.Email) &&
                CheckIfStringNotEmpty(person.Username) &&
                CheckIfStringNotEmpty(person.FirstName) &&
                CheckIfStringNotEmpty(person.LastName) &&
                CheckIfStringNotEmpty(person.Phone) &&
                CheckIfStringNotEmpty(person.Address) &&
                CheckIfStringNotEmpty(person.Password));
        }
    }
}
