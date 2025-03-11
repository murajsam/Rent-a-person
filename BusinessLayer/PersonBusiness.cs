using DataLayer;
using DataLayer.Models;

namespace BusinessLayer
{
    public class PersonBusiness
    {
        private readonly PersonRepository personRepository;

        public PersonBusiness()
        {
            this.personRepository = new PersonRepository();
        }

        public bool RegisterPerson(Person person)
        {
            if (this.personRepository.DoesUsernameExist(person.Username) == false && (InputMgtB.CheckIfPersonFieldsNotEmpty(person)))
                if ((InputMgtB.CheckIfPersonFieldsNotEmpty(person))&&(this.personRepository.RegisterPerson(person)))
                    return true;
                else
                    return false;
            else
                return false;
        }

        public bool LoginPerson(Person person) 
        {
            if ((InputMgtB.CheckIfStringNotEmpty(person.Username)) && (InputMgtB.CheckIfStringNotEmpty(person.Password)) && (this.personRepository.LogInPerson(person)))
                return true;
            else
                return false;
        }
    }
}
