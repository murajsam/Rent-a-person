using DataLayer.Models;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentAPersonDataTests
{
    [TestClass]
    public class ReservationsRepositoryTest
    {
        private ReservationRepository reservationRepository;
        private PersonRepository personRepository;
        private ProviderRepository providerRepository;
        private UserRepository userRepository;
        private Reservation reservation;
        private Person mockPersonUser;
        private Person mockProviderUser;
        private User mockUser;
        private Provider mockProvider;

        [TestInitialize]
        public void Init()
        {
            mockPersonUser = new Person
            {
                Username = "PersonUser",
                Email = "person@gmail.com",
                FirstName = "TestName",
                LastName = "TestLastName",
                Address = "Test Address 123",
                Phone = "3816533453",
                Password = "Password123"
            };

            mockProviderUser = new Person
            {
                Username = "PersonProvider",
                Email = "person@gmail.com",
                FirstName = "TestName",
                LastName = "TestLastName",
                Address = "Test Address 123",
                Phone = "3816533453",
                Password = "Password123"
            };
            mockProvider = new Provider
            {
                ProviderDetails = mockProviderUser,
                PricePerHour = 100,
                Type = "Rent a Friend",
                Avatar = GetImageArray()
            };
            mockUser = new User
            {
                UserDetails = mockPersonUser
            };

            reservationRepository = new ReservationRepository();
            personRepository = new PersonRepository();
            providerRepository = new ProviderRepository();
            userRepository = new UserRepository();
        }

        [TestMethod]
        public void TestRegisterMethod()
        {
            bool registerPersonUser = this.personRepository.RegisterPerson(mockPersonUser);
            bool registerProviderUser = this.personRepository.RegisterPerson(mockProviderUser);
            bool registerProvider = this.providerRepository.RegisterProvider(mockProvider);
            bool registerUser = this.userRepository.RegisterUser(mockUser);

            List<User> users = new List<User>();
            users = userRepository.GetAllUsers()
                .Where(u => u.UserDetails.Username == mockUser.UserDetails.Username && u.UserDetails.Password == mockUser.UserDetails.Password).ToList();
            User FoundUser = users.Count > 0 ? users[0] : null;

             Provider FoundProvider = providerRepository.GetAllProviders()
            .Where(pr => pr.ProviderDetails.Username == mockProvider.ProviderDetails.Username && pr.ProviderDetails.Password == mockProvider.ProviderDetails.Password).ToList()[0];

            this.reservation = new Reservation
            {
                ProviderId = FoundProvider.ProviderId,
                UserId = FoundUser.UserId,
                Description = "Reservation 123",
                Status = "available",
                Location = "Cacak",
                StartDate = new DateTime(2024, 1, 11, 11, 30, 00),
                EndDate = new DateTime(2024,1,15,12,00,00)
            };

            bool reservationCreated = reservationRepository.CreateReservation(reservation);

            Assert.IsTrue(registerPersonUser);
            Assert.IsTrue(registerProviderUser);
            Assert.IsTrue(registerProvider);
            Assert.IsTrue(registerUser);
            Assert.IsTrue(reservationCreated);
        }

        [TestCleanup]
        public void TestDeleteMethod()
        {
            Reservation reservationToDelete = reservationRepository.GetAllReservations()
            .Where(res => res.UserId == reservation.UserId && res.ProviderId == reservation.ProviderId && res.StartDate == reservation.StartDate && res.EndDate == reservation.EndDate && res.Description == reservation.Description && res.Location == reservation.Location).ToList()[0];

            reservationRepository.RemoveReservation(reservationToDelete);

            providerRepository.DeleteProviderAndPerson(mockProvider.ProviderDetails.Username);
            userRepository.DeleteUserAndPerson(mockUser.UserDetails.Username);
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