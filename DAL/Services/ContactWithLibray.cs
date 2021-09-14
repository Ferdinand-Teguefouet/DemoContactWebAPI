using ADOLibrary;
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
    public class ContactWithLibray : IContactRepoLibrary
    {
        private string _connectionString;
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ContactWithLibray(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
        }

        private Connection seConnecter()
        {
            return new Connection(_connectionString);
        }

        public Contact Convert(SqlDataReader reader)
        {
            return new Contact
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"] is DBNull ? null : reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString()
            };
        }

        public void Delete(int Id)
        {
            string query = "DELETE FROM Contact WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);            
            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<Contact> GetAll()
        {
            string query = "SELECT * FROM Contact";
            Command cmd = new Command(query);
            
            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public Contact GetById(int Id)
        {
            string query = "SELECT * FROM Contact WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            return seConnecter().ExecuteReader(cmd, Convert).FirstOrDefault();
        }

        public bool Insert(Contact c)
        {
            string query = "INSERT INTO Contact (FirstName, LastName, Email)" +
                " VALUES(@fn, @ln, @email)";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);
            return seConnecter().ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(Contact c)
        {
            string query = "UPDATE Contact SET FirstName = @fn, LastName = @ln, Email = @email WHERE Id = @id";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);
            cmd.AddParameter("id", c.Id);
            return seConnecter().ExecuteNonQuery(cmd) == 1;
        }
    }
}
