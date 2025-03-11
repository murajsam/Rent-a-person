using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ReservationRepository
    {
        private readonly SqlConnection _conn = new SqlConnection(Constants.connectionString);
        private SqlCommand _command;
        private SqlDataReader _reader;

        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            Reservation reservation;

            using (_command = new SqlCommand("SELECT reservationId, userId, providerId, startDate, endDate, location, description, status " +
                                             "FROM Reservations", _conn))
            {
                try
                {
                    _conn.Open();

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        reservation = new Reservation
                        (
                            Convert.ToInt32(_reader["reservationId"]),
                            Convert.ToInt32(_reader["userId"]),
                            Convert.ToInt32(_reader["providerId"]),
                            Convert.ToDateTime(_reader["startDate"]),
                            Convert.ToDateTime(_reader["endDate"]),
                            GetValueFromReader(_reader, "location"),
                            GetValueFromReader(_reader, "description"),
                            GetValueFromReader(_reader, "status")
                        );

                        reservations.Add(reservation);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    if (_reader != null)
                        _reader.Close();

                    _conn.Close();
                }
            }

            // Return null if the list is empty
            return reservations;
        }

        public bool CreateReservation(Reservation reservation)
        {
            bool success = false;

            using (_command = new SqlCommand("INSERT INTO Reservations (userId, providerId, startDate, endDate, location, description, status) " +
                                             "VALUES (@userId, @providerId, @startDate, @endDate, @location, @description, @status)", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@userId", reservation.UserId);
                    _command.Parameters.AddWithValue("@providerId", reservation.ProviderId);
                    _command.Parameters.AddWithValue("@startDate", reservation.StartDate);
                    _command.Parameters.AddWithValue("@endDate", reservation.EndDate);
                    _command.Parameters.AddWithValue("@location", reservation.Location);
                    _command.Parameters.AddWithValue("@description", reservation.Description);
                    _command.Parameters.AddWithValue("@status", reservation.Status);

                    _conn.Open();

                    int rowsAffected = _command.ExecuteNonQuery();

                    success = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    _conn.Close();
                }
            }

            return success;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            using (_command = new SqlCommand("UPDATE Reservations SET userId = @userId, providerId = @providerId, startDate = @startDate, endDate = @endDate, location = @location, description = @description, status = @status " +
                                             "WHERE reservationId = @reservationId", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@userId", reservation.UserId);
                    _command.Parameters.AddWithValue("@providerId", reservation.ProviderId);
                    _command.Parameters.AddWithValue("@startDate", reservation.StartDate);
                    _command.Parameters.AddWithValue("@endDate", reservation.EndDate);
                    _command.Parameters.AddWithValue("@location", reservation.Location);
                    _command.Parameters.AddWithValue("@description", reservation.Description);
                    _command.Parameters.AddWithValue("@status", reservation.Status);
                    _command.Parameters.AddWithValue("@reservationId", reservation.ReservationId);

                    _conn.Open();

                    return _command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    _conn.Close();
                }
                return false;
            }
        }

        public bool RemoveReservation(Reservation reservation)
        {
            using (_command = new SqlCommand("DELETE FROM Reservations WHERE reservationId = @reservationId", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@reservationId", reservation.ReservationId);

                    _conn.Open();

                    return _command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    _conn.Close();
                }
                return false;
            }
        }


        private string GetValueFromReader(SqlDataReader reader, string fieldName)
        {
            return (reader[fieldName].ToString());
        }
    }
}
