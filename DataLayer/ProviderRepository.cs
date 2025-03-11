using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ProviderRepository
    {
        private SqlConnection _conn = new SqlConnection(Constants.connectionString);
        private SqlCommand _command;
        private SqlDataReader _reader;



        public bool RegisterProvider(Provider provider)
        {
            using (_command = new SqlCommand("INSERT INTO Providers (username, avatar, type, pricePerHour) VALUES (@username, @avatar, @type, @pricePerHour)", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@username", provider.ProviderDetails.Username);
                    _command.Parameters.AddWithValue("@avatar", provider.Avatar);
                    _command.Parameters.AddWithValue("@type", provider.Type);
                    _command.Parameters.AddWithValue("@pricePerHour", provider.PricePerHour);

                    _conn.Open();
                    _command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Unutar tabele Providers - {ex.Message}");
                    return false;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }

        public bool UpdateProvider(Provider provider)
        {
            using (_command = new SqlCommand("BEGIN TRANSACTION; UPDATE Providers SET username = @username, type = @type, avatar = @avatar, pricePerHour = @pricePerHour WHERE providerId=@providerId; " +
                                             "UPDATE People SET username = @username, password = @password, firstName = @firstName, lastName = @lastName, address = @address,email = @email,phone = @phone " +
                                             "WHERE username = @username; COMMIT TRANSACTION;", _conn))
            {
                try
                {
                    _conn.Open();
                    _command.Parameters.AddWithValue("@providerId", provider.ProviderId);
                    _command.Parameters.AddWithValue("@username", provider.ProviderDetails.Username);
                    _command.Parameters.AddWithValue("@password", provider.ProviderDetails.Password);
                    _command.Parameters.AddWithValue("@firstName", provider.ProviderDetails.FirstName);
                    _command.Parameters.AddWithValue("@lastName", provider.ProviderDetails.LastName);
                    _command.Parameters.AddWithValue("@address", provider.ProviderDetails.Address);
                    _command.Parameters.AddWithValue("@email", provider.ProviderDetails.Email);
                    _command.Parameters.AddWithValue("@phone", provider.ProviderDetails.Phone);
                    _command.Parameters.AddWithValue("@avatar", provider.Avatar);
                    _command.Parameters.AddWithValue("@type", provider.Type);
                    _command.Parameters.AddWithValue("@pricePerHour", provider.PricePerHour);

                    return _command.ExecuteNonQuery() > 0;
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


        public List<Provider> GetAllProviders()
        {
            List<Provider> providers = new List<Provider>();
            Provider provider;

            using (_command = new SqlCommand("SELECT p.username, p.password, p.firstName, p.lastName, p.address, p.email, p.phone, pr.providerId, pr.avatar, pr.type, pr.pricePerHour " +
                                             "FROM People p " +
                                             "JOIN Providers pr ON p.username = pr.username", _conn))
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

                        provider = new Provider
                        (
                            Convert.ToInt32(_reader["providerId"]),
                            _reader["avatar"] as byte[],
                            GetValueFromReader(_reader, "type"),
                            Convert.ToDouble(GetValueFromReader(_reader, "pricePerHour")),
                            person
                        );

                        providers.Add(provider);
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

            return providers;
        }


        private string GetValueFromReader(SqlDataReader reader, string fieldName)
        {
            return (reader[fieldName].ToString());
        }

        #region Method for test cleanup
        public int DeleteProviderAndPerson(string providerUsername)
        {
            using (_command = new SqlCommand("BEGIN TRANSACTION; DELETE FROM Providers WHERE username = @username; DELETE FROM People WHERE username = @username; COMMIT TRANSACTION;", _conn))
            {
                try
                {
                    _command.Parameters.AddWithValue("@username", providerUsername);
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
