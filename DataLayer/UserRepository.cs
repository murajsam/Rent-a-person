using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class UserRepository
    {
        private readonly SqlConnection _conn = new SqlConnection(Constants.connectionString);
        private SqlCommand _command;
        private SqlDataReader _reader;
        public bool RegisterUser(User user)
        {
            using (_command = new SqlCommand("INSERT INTO Users (username) VALUES (@username)", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@username", user.UserDetails.Username);

                    _conn.Open();
                    _command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Unutar tabele Users - {ex.Message}");
                    return false;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (_command = new SqlCommand("BEGIN TRANSACTION; UPDATE Users SET username = @username WHERE userId=@userId; "+
                                             "UPDATE People SET username = @username, password = @password, firstName = @firstName, lastName = @lastName, address = @address,email = @email,phone = @phone " +
                                             "WHERE username = @username; COMMIT TRANSACTION;", _conn))
            {
                try
                {
                    _conn.Open();
                    _command.Parameters.AddWithValue("@userId", user.UserId);
                    _command.Parameters.AddWithValue("@username", user.UserDetails.Username);
                    _command.Parameters.AddWithValue("@password", user.UserDetails.Password);
                    _command.Parameters.AddWithValue("@firstName", user.UserDetails.FirstName);
                    _command.Parameters.AddWithValue("@lastName", user.UserDetails.LastName);
                    _command.Parameters.AddWithValue("@address", user.UserDetails.Address);
                    _command.Parameters.AddWithValue("@email", user.UserDetails.Email);
                    _command.Parameters.AddWithValue("@phone", user.UserDetails.Phone);

                    return _command.ExecuteNonQuery()>0;
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.ToString()); 
                }
                finally
                {
                    _conn.Close();
                }

                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            User user;

            using (_command = new SqlCommand("SELECT u.userId, p.username, p.password, p.firstName, p.lastName, p.address, p.email, p.phone " +
                                             "FROM Users u " +
                                             "JOIN People p ON u.username = p.username", _conn))
            {
                try
                {
                    _conn.Open();

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        Person person = new Person
                        (
                            GetValueFromReader(_reader, "username"),
                            GetValueFromReader(_reader, "password"),
                            GetValueFromReader(_reader, "firstName"),
                            GetValueFromReader(_reader, "lastName"),
                            GetValueFromReader(_reader, "address"),
                            GetValueFromReader(_reader, "email"),
                            GetValueFromReader(_reader, "phone")
                        );

                        user = new User
                        (
                            Convert.ToInt32(_reader["userId"]),
                            person
                        );

                        users.Add(user);
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

            return users;
        }

        private string GetValueFromReader(SqlDataReader reader, string fieldName)
        {
            return (reader[fieldName].ToString());
        }

        #region Method for test cleanup
        public int DeleteUserAndPerson(string userUsername)
        {
            using (_command = new SqlCommand("BEGIN TRANSACTION; DELETE FROM Users WHERE username = @username; DELETE FROM People WHERE username = @username; COMMIT TRANSACTION;", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@username", userUsername);
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
