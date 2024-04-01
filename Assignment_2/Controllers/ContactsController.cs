using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment_2.Models;

namespace Assignment_2.Controllers
{
    public class ContactsController : ApiController
    {
        
        public IEnumerable<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            string connectionString = @"Data Source=(localdb)\MSSQLLOCALDB; Initial Catalog = Contacts; Integrated Security=True;";
            string query = "SELECT * FROM Contact";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            contacts.Add(new Contact
                            {
                                Id = (int)row["ID"],
                                FirstName = (string)row["FirstName"],
                                LastName = (string)row["LastName"],
                                Email = (string)row["Email"],
                                Phone = (string)row["Phone"]
                            });
                        }
                    }
                }
            }
            return contacts;
        }

        public IHttpActionResult GetContactsById(int id)
        {
            Contact contacts = new Contact();
            string connectionString = @"Data Source=(localdb)\MSSQLLOCALDB; Initial Catalog = Contacts; Integrated Security=True;";
            string query = "SELECT * FROM Contact WHERE id=" + id + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable != null)
                    {
                        contacts = new Contact
                        {
                            Id = (int)dataTable.Rows[0]["ID"],
                            FirstName = (string)dataTable.Rows[0]["FirstName"],
                            LastName = (string)dataTable.Rows[0]["LastName"],
                            Email = (string)dataTable.Rows[0]["Email"],
                            Phone = (string)dataTable.Rows[0]["Phone"]
                        };
                    }
                }
            }
            return Ok(contacts);
        }

        public IHttpActionResult Post([FromBody] Contact conct)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLOCALDB; Initial Catalog = Contacts; Integrated Security=True;";
            string query = "INSERT INTO Contact (FirstName, LastName, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", conct.FirstName);
                    command.Parameters.AddWithValue("@LastName", conct.LastName);
                    command.Parameters.AddWithValue("@Email", conct.Email);
                    command.Parameters.AddWithValue("@Phone", conct.Phone);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] Contact conct)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLOCALDB; Initial Catalog = Contacts; Integrated Security=True;";
            string query = "UPDATE Contact SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone WHERE id=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", conct.FirstName);
                    command.Parameters.AddWithValue("@LastName", conct.LastName);
                    command.Parameters.AddWithValue("@Email", conct.Email);
                    command.Parameters.AddWithValue("@Phone", conct.Phone);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLOCALDB; Initial Catalog = Contacts; Integrated Security=True;";
            string query = "DELETE Contact WHERE id=" + id + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return Ok();
        }
    }
}
