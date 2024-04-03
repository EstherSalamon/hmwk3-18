using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleListJSLoading
{
    public class PersonRepository
    {
        private string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAllPeople()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            List<Person> people = new List<Person>();

            while(reader.Read())
            {
                people.Add(new Person
                {
                    ID = (int)reader["ID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return people;
        }

        public void AddPerson(Person p)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People VALUES(@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            int ID = (int)(decimal)cmd.ExecuteScalar();
            p.ID = ID;
        }

        public void EditPerson(Person p)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE ID = @id";
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            cmd.Parameters.AddWithValue("@id", p.ID);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeletePerson(int ID)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", ID);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public Person GetByID(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if(!reader.Read())
            {
                return null;
            }

            Person p = new Person
            {
                ID = (int)reader["ID"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };

            return p;
        }
    }
}
