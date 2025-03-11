using DataLayer;
using DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentAPersonDataTests
{
    [TestClass]
    public class ProviderRepositoryTest
    {
        private ProviderRepository providerRepository;
        private PersonRepository personRepository;
        private Person person;
        private Provider provider;

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

            provider = new Provider
            {
                ProviderDetails = person,
                PricePerHour = 100,
                Type = "Rent a Friend",
                Avatar = GetImageArray()
            };

            personRepository = new PersonRepository();
            providerRepository = new ProviderRepository();
        }

        [TestMethod]
        public void TestRegisterMethod()
        {
            bool registerPerson = this.personRepository.RegisterPerson(person);
            bool registerProvider = this.providerRepository.RegisterProvider(provider);

            Assert.IsTrue(registerPerson);
            Assert.IsTrue(registerProvider);
        }

        [TestMethod]
        public void TestLoginMethod()
        {
            bool personRegister = this.personRepository.RegisterPerson(person);
            bool providerRegister = this.providerRepository.RegisterProvider(provider);

            List<Provider> providers = providerRepository.GetAllProviders()
            .Where(provider => provider.ProviderDetails.Username == provider.ProviderDetails.Username && provider.ProviderDetails.Password == provider.ProviderDetails.Password).ToList();
            bool providerLoggedIn = providers.Count > 0;
            
            Assert.IsTrue(providerLoggedIn);
        }

        [TestMethod]
        public void TestUpdateProviderMethod()
        {
            personRepository.RegisterPerson(person);
            providerRepository.RegisterProvider(provider);

            Provider newProvider = providerRepository.GetAllProviders().Where(p => p.ProviderDetails.Username == provider.ProviderDetails.Username).ToList()[0];
            bool result = providerRepository.UpdateProvider(newProvider);
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public void TestDeleteMethod()
        {
            providerRepository.DeleteProviderAndPerson(provider.ProviderDetails.Username);
        }

        public static byte[] GetImageArray()
        {
            string hexString = "89504E470D0A1A0A0000000D494844520000000A0000000A080600000075AC4F2F0000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001A4944415408D763F63F0000000505D59C2E0000000000000049454E44AE426082";

            byte[] byteArray = new byte[hexString.Length / 2];
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return byteArray;
        }
    }
}
