using DataLayer.Models;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentAPersonDataTests
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository userRepository;
        private PersonRepository personRepository;
        private Person person;
        private User user;

        [TestInitialize]
        public void Init()
        {
            person = new Person
            {
                Username = "Person2",
                Email = "person@gmail.com",
                FirstName = "TestName",
                LastName = "TestLastName",
                Address = "Test Address 123",
                Phone = "3816533453",
                Password = "Password123"
            };

            user = new User{
                UserDetails = person
            };

            personRepository = new PersonRepository();
            userRepository = new UserRepository();
        }

        [TestMethod]
        public void TestRegisterMethod()
        {
            bool registerPerson = this.personRepository.RegisterPerson(person);
            bool registerUser = this.userRepository.RegisterUser(user);

            Assert.IsTrue(registerPerson);
            Assert.IsTrue(registerUser);
        }

        [TestMethod]
        public void TestLoginMethod()
        {
            bool personRegister = this.personRepository.RegisterPerson(person);
            bool userRegister = this.userRepository.RegisterUser(user);

            List<User> users = new List<User>();
            users = userRepository.GetAllUsers()
                .Where(u => u.UserDetails.Username == user.UserDetails.Username && u.UserDetails.Password == user.UserDetails.Password).ToList();
            bool userLoggedIn = users.Count > 0;

            Assert.IsTrue(userLoggedIn);
        }

        [TestMethod]
        public void TestUpdateUserMethod()
        {
            personRepository.RegisterPerson(person);
            userRepository.RegisterUser(user);

            User newUser = userRepository.GetAllUsers().Where(u => u.UserDetails.Username == user.UserDetails.Username).ToList()[0];
            bool result = userRepository.UpdateUser(newUser);
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public void TestDeleteMethod()
        {
            userRepository.DeleteUserAndPerson(user.UserDetails.Username);
        }
    }
}