using DataLayer;
using DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RentAPersonDataTests
{
    [TestClass]
    public class PersonRepositoryTest
    {
        private Person person;
        private PersonRepository personRepository;

        [TestInitialize]
        public void Init()
        {
            person = new Person
            {
                Username = "Person1",
                Email = "person@gmail.com",
                FirstName = "TestName",
                LastName = "TestLastName",
                Address = "Test Address 123",
                Phone = "3816533453",
                Password = "Password123"
            };

            personRepository = new PersonRepository();
        }

        [TestMethod]
        public void TestRegisterMethod()
        {
            bool personInserted = this.personRepository.RegisterPerson(person);

            Assert.IsTrue(personInserted);
        }

        [TestMethod]
        public void TestLoginMethod()
        {
            bool personRegister = this.personRepository.RegisterPerson(person);
            bool personLoggedIn = this.personRepository.LogInPerson(person);    

            Assert.IsTrue(personLoggedIn);
        }

        [TestMethod]
        public void TestDoesUsernameExist()
        {
            bool personRegister = this.personRepository.RegisterPerson(person);
            bool usernameexists = this.personRepository.DoesUsernameExist(this.person.Username);

            Assert.IsTrue(usernameexists);

            bool usernamedoesntexits = this.personRepository.DoesUsernameExist(Guid.NewGuid().ToString());

            Assert.IsFalse(usernamedoesntexits);
        }

        [TestCleanup]
        public void CleanUp()
        {
            personRepository.DeletePerson(this.person.Username);
        }
    }
}
