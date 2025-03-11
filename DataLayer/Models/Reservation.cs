using System;

namespace DataLayer.Models
{
    public class Reservation
    {
        private int reservationId;

        public int ReservationId
        {
            get { return reservationId; }
            set { reservationId = value; }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int providerId;

        public int ProviderId
        {
            get { return providerId; }
            set { providerId = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public Reservation() { }

        public Reservation(int reservationId, int userId, int providerId, DateTime startDate, DateTime endDate, string location, string description, string status)
        {
            ReservationId = reservationId;
            UserId = userId;
            ProviderId = providerId;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Description = description;
            Status = status;
        }
    }
}

