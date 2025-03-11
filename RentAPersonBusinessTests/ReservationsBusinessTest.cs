using BusinessLayer;
using DataLayer;
using DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RentAPersonBusinessTests
{
    [TestClass]
    public class ReservationsBusinessTest
    {
        private ReservationRepository reservationRepository;
        private PersonRepository personRepository;
        private ProviderRepository providerRepository;
        private UserRepository userRepository;
        private Reservation reservation;
        private Reservation secondReservation;
        private Person mockPersonUser;
        private Person mockPersonProvider;
        private User mockUser;
        private Provider mockProvider;

        private ReservationBusiness reservationsBusiness;

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

            mockPersonProvider = new Person
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
                ProviderDetails = mockPersonProvider,
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

            reservationsBusiness = new ReservationBusiness();
        }

        [TestMethod]
        public void AreUserReservationsReturned()
        {
            bool registerPersonUser = this.personRepository.RegisterPerson(mockPersonUser);
            bool registerPersonProvider = this.personRepository.RegisterPerson(mockPersonProvider);
            bool registerProvider = this.providerRepository.RegisterProvider(mockProvider);
            bool registerUser = this.userRepository.RegisterUser(mockUser);

            Assert.IsTrue(registerPersonUser);
            Assert.IsTrue(registerPersonProvider);
            Assert.IsTrue(registerProvider);
            Assert.IsTrue(registerUser);

            User FoundUser = userRepository.GetAllUsers().Where(u => u.UserDetails.Username == mockUser.UserDetails.Username && u.UserDetails.Password == mockUser.UserDetails.Password).ToList()[0];

            Provider FoundProvider = providerRepository.GetAllProviders().Where(p => p.ProviderDetails.Username == mockProvider.ProviderDetails.Username).ToList()[0];


            this.reservation = new Reservation
            {
                ProviderId = FoundProvider.ProviderId,
                UserId = FoundUser.UserId,
                Description = "Reservation 123",
                Status = "available",
                Location = "Cacak",
                StartDate = new DateTime(2024, 1, 11, 11, 30, 00),
                EndDate = new DateTime(2024, 1, 15, 12, 00, 00)
            };

            bool reservationCreated = reservationRepository.CreateReservation(reservation);

            this.secondReservation = new Reservation
            {
                ProviderId = FoundProvider.ProviderId,
                UserId = FoundUser.UserId,
                Description = "Reservation 345",
                Status = "available",
                Location = "Cacak",
                StartDate = new DateTime(2024, 2, 1, 11, 30, 00),
                EndDate = new DateTime(2024, 2, 2, 12, 00, 00)
            };

            bool reservation2Created = reservationRepository.CreateReservation(secondReservation);

            Assert.IsTrue(reservationCreated);
            Assert.IsTrue(reservation2Created);
            Assert.IsNotNull(reservationsBusiness.GetAllReservationsFromUser(mockUser));
            Assert.IsNotNull(reservationsBusiness.GetAllReservationsFromProvider(mockProvider));
        }

        [TestCleanup]
        public void TestDeleteMethod()
        {
            Reservation reservationToDelete = reservationRepository.GetAllReservations()
            .Where(res => res.UserId == reservation.UserId && res.ProviderId == reservation.ProviderId && res.StartDate == reservation.StartDate && res.EndDate == reservation.EndDate && res.Description == reservation.Description && res.Location == reservation.Location).ToList()[0];
            reservationRepository.RemoveReservation(reservationToDelete);

            reservationToDelete = reservationRepository.GetAllReservations()
            .Where(res => res.UserId == secondReservation.UserId && res.ProviderId == secondReservation.ProviderId && res.StartDate == secondReservation.StartDate && res.EndDate == secondReservation.EndDate && res.Description == secondReservation.Description && res.Location == secondReservation.Location).ToList()[0];
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