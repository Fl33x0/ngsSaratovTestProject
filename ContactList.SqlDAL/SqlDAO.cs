using ContactList.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace ContactList.SqlDAL
{
    public class SqlDAO
    {
        private static string _connectionString = @"Data Source=HOME-PC\SQLEXPRESS;Database=ContactsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection _connection = new SqlConnection(_connectionString);

        public Contact AddContact(string name, string phoneNumber)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO dbo.Contacts(Name, PhoneNumber) VALUES(@Name, @PhoneNumber) SELECT CAST(scope_identity() AS INT) AS NewID";
                var command = new SqlCommand(query, _connection);

                if (!phoneNumber.StartsWith('+') | !char.IsDigit(phoneNumber[0]))
                {
                    for (int i = 1; i < phoneNumber.Length; i++)
                    {
                        if (!char.IsDigit(phoneNumber[i]))
                        {
                            throw new InvalidOperationException(
                             string.Format("Cannot add Contact with parameters: {0}, {1};",
                             name, phoneNumber));
                        }                        
                    }
                }                

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                _connection.Open();

                var result = command.ExecuteScalar();

                if (result != null)
                {
                    return new Contact(
                        id: (int)result,
                        name: name,
                        phoneNumber: phoneNumber);
                }
                    

                throw new InvalidOperationException(
                    string.Format("Cannot add Contact with parameters: {0}, {1};",
                    name, phoneNumber));
            }
        }

        public Contact GetContact(int id)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                var stProc = "Contact_GetByID";

                var command = new SqlCommand(stProc, _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Contact(
                        id: (int)reader["ID"],
                        name: reader["Name"] as string,
                        phoneNumber: (string)reader["PhoneNumber"]);
                }

                throw new InvalidOperationException("Cannot find Contact with ID = " + id);
            }
        }

        public IEnumerable<Contact> GetContacts(bool orderedByName = true)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT ID, Name, PhoneNumber FROM dbo.Contacts " +
                    (orderedByName ? "ORDER BY Name" : "");

                var command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Contact(
                        id: (int)reader["ID"],
                        name: reader["Name"] as string,
                        phoneNumber: reader["PhoneNumber"] as string);
                }
            }
        }

        public int DeleteContact(int id)
        {           
            using (_connection = new SqlConnection(_connectionString))
            {                
                var query = "DELETE FROM dbo.Contacts WHERE ID = @ID";
                                
                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@ID", id);

                _connection.Open();

                var result = command.ExecuteNonQuery();

                if (result != 0)
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException("Cannot delete Contact with ID = " + id);
                }
            }
        }

        public int EditContact(int id, string newName, string newNumber)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE dbo.Contacts " +
                    "SET Name = @newName, PhoneNumber = @newNumber " +
                    "WHERE ID = @id";

                var command = new SqlCommand(query, _connection);

                if (!newNumber.StartsWith('+') | !char.IsDigit(newNumber[0]))
                {
                    for (int i = 1; i < newNumber.Length; i++)
                    {
                        if (!char.IsDigit(newNumber[i]))
                        {
                            throw new InvalidOperationException(
                             string.Format("Cannot edit Contact with parameters: {0}, {1};",
                             newName, newNumber));
                        }
                    }
                }

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@newName", newName);
                command.Parameters.AddWithValue("@newNumber", newNumber);

                _connection.Open();

                var result = command.ExecuteNonQuery();

                if (result != 0)
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException("Cannot edit Contact with ID = " + id);
                }
            }
        }

        public IEnumerable<Contact> GetContactByLetters(string firstLetters)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                var stProc = "Contact_GetByLetters";

                var command = new SqlCommand(stProc, _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@firstLetters", firstLetters);

                _connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Contact(
                        id: (int)reader["ID"],
                        name: reader["Name"] as string,
                        phoneNumber: reader["PhoneNumber"] as string);
                }
            }
        }
    }
}
