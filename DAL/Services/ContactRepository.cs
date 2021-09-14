using DAL.Entities;
using DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ContactRepository : IContactRepository
    {
        private string _connectionString;
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ContactRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");   
        }
        public void Delete(int Id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Contact WHERE Id = @MonId";
                    cmd.Parameters.AddWithValue("MonId", Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contact";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Contact
                            {
                                Id = (int)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
            }
            
        }

        public Contact GetById(int Id)
        {
            Contact c = new Contact();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contact WHERE Id = @monId";
                    cmd.Parameters.AddWithValue("monId", Id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            c.Id = (int)reader["Id"];
                            c.FirstName = reader["FirstName"].ToString();
                            c.LastName = reader["LastName"].ToString();
                            c.Email = reader["Email"].ToString();
                        }
                    }
                }
            }
            return c;
        }

        public void Insert(Contact c)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Contact (LastName, FirstName, Email) " +
                        "VALUES (@ln, @fn, @email)";

                    cmd.Parameters.AddWithValue("ln", c.LastName);
                    cmd.Parameters.AddWithValue("fn", c.FirstName);
                    cmd.Parameters.AddWithValue("email", c.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Contact c)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Contact SET LastName = @ln, FirstName = @fn, Email = @email" +
                        " WHERE Id = @id";

                    cmd.Parameters.AddWithValue("ln", c.LastName);
                    cmd.Parameters.AddWithValue("fn", c.FirstName);
                    cmd.Parameters.AddWithValue("email", c.Email);
                    cmd.Parameters.AddWithValue("id", c.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
