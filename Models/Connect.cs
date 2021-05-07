using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace WebAPI_Form.Models
{
    public class Connect
    {
        public static void ExecuteInsert(string sqlQuery)
        {
            string connectionString = @"Data Source=YURY-OKHRIMENKO\SQLEXPRESS;Initial Catalog=SofTrust_db;Integrated Security=True";

            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public static List<Contact> ExecuteSelectContacts(string sqlQuery)
        {
            string connectionString = @"Data Source=YURY-OKHRIMENKO\SQLEXPRESS;Initial Catalog=SofTrust_db;Integrated Security=True";

            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Contact> contacts = new List<Contact>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contacts.Add(new Contact()
                    {
                        ContactId = reader.GetInt32(0),
                        ContactName = reader.GetString(1),
                        ContactEmail = reader.GetString(2),
                        ContactPhone = reader.GetString(3)
                    });
                }
            }

            connection.Close();

            return contacts;
        }
        public static List<Topic> ExecuteSelectTopics(string sqlQuery)
        {
            string connectionString = @"Data Source=YURY-OKHRIMENKO\SQLEXPRESS;Initial Catalog=SofTrust_db;Integrated Security=True";

            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Topic> topics = new List<Topic>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    topics.Add(new Topic()
                    {
                        TopicId = reader.GetInt32(0),
                        TopicName = reader.GetString(1)
                    });
                }
            }

            connection.Close();

            return topics;
        }
    }
}
