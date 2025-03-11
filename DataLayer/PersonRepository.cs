using DataLayer.Models;
using System;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PersonRepository
    {
        private readonly SqlConnection _conn = new SqlConnection(Constants.connectionString);
        private SqlCommand _command;
        private SqlDataReader _reader;
        public bool DoesUsernameExist(string username)
        {
            bool usernameExists = false;

            using (_command = new SqlCommand("SELECT 1 FROM People WHERE username = @username", _conn))
            {
                _command.Parameters.AddWithValue("@username", username);
            try
            {
                _conn.Open();
                object result = _command.ExecuteScalar();
                usernameExists = (result != null && result != DBNull.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _conn.Close();
            }
            return usernameExists;

            }
        }
        public bool RegisterPerson(Person person)
        {
            using (_command = new SqlCommand(
                 "INSERT INTO People (username, password, firstName, lastName, address, email, phone) " +
                 "VALUES (@username, @password, @firstName, @lastName, @address, @email, @phone)", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@username", person.Username);
                    _command.Parameters.AddWithValue("@password", person.Password);
                    _command.Parameters.AddWithValue("@firstName", person.FirstName);
                    _command.Parameters.AddWithValue("@lastName", person.LastName);
                    _command.Parameters.AddWithValue("@address", person.Address);
                    _command.Parameters.AddWithValue("@email", person.Email);
                    _command.Parameters.AddWithValue("@phone", person.Phone);

                    _conn.Open();
                    _command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Unutar tabele People - {ex.Message}");
                    return false;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }

        public bool LogInPerson(Person person)
        {
            using (_command = new SqlCommand("SELECT 1 FROM People WHERE username='" + person.Username + "' AND password='" + person.Password + "'", _conn))
            {
                try
                {
                    _conn.Open();

                    _reader = _command.ExecuteReader();
                    if (_reader.Read())
                    {
                        _reader.Close();
                        return true;
                    }
                    else
                    {
                        _reader.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }
        #region Method for test cleanup
        public int DeletePerson(String personUsername)
        {
            using (_command = new SqlCommand("DELETE FROM People WHERE username = '" + personUsername + "'", _conn))
            {
                try
                {
                    _conn.Open();

                    return _command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
                finally { _conn.Close(); }
            }
        }

        #endregion
    }
}
