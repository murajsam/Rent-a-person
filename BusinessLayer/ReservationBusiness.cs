using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class ReservationBusiness
    {
        private readonly ReservationRepository reservationRepository;

        public ReservationBusiness()
        {
            this.reservationRepository = new ReservationRepository(); 
        }

        public bool CreateReservation(Reservation reservation)
        {
            return(this.reservationRepository.CreateReservation(reservation));
        }

        public bool UpdateReservation(Reservation Reservation)
        {
            return(this.reservationRepository.UpdateReservation(Reservation));    
        }

        public bool DeleteReservation(Reservation Reservation)
        {
            return (this.reservationRepository.RemoveReservation(Reservation));
        }

        public List<Reservation> GetAllReservationsFromProvider(Provider provider)
        {
            List<Reservation> reservations = this.reservationRepository.GetAllReservations()
                .Where(reservation => reservation.ProviderId == provider.ProviderId)
                .ToList();
            return reservations != null ? reservations : null;
        }

        public List<Reservation> GetAllReservationsFromUser(User user)
        {
            List<Reservation> reservations = this.reservationRepository.GetAllReservations()
                .Where(reservation => reservation.UserId == user.UserId)
                .ToList();
            return reservations != null ? reservations : null;
        }

        public bool CheckForAcceptedReservations(int reservationId, int providerId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<Reservation> reservations = this.reservationRepository.GetAllReservations();

                if (reservations != null && reservations.Any())
                {
                    bool hasAcceptedReservations = reservations.Any(r =>
                        (r.Status == "accepted" || r.Status == "pending")
                        && r.ProviderId == providerId
                        && r.ReservationId != reservationId
                        && r.StartDate <= EndDate && r.EndDate >= StartDate
                    );

                    return hasAcceptedReservations;
                }
                else
                {
                    // Handle the case when reservations list is null or empty.
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking reservations: {ex.Message}");
                // Handle the exception or log it as needed.
                return false;
            }
        }


    }
}
