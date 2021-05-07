using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace WebAPI_Form.Models
{
    public partial class Topic
    {
        public Topic()
        {
            AllMessages = new HashSet<AllMessage>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public virtual ICollection<AllMessage> AllMessages { get; set; }

        public static List<Topic> ifTopicExistInDataBase(int TopicId)
        {
            string sqlQuery = $"SELECT * FROM topics WHERE topic_id LIKE '{TopicId}'";

            List<Topic> topics = Connect.ExecuteSelectTopics(sqlQuery);
            //using SofTrust_dbContext dataBase = new SofTrust_dbContext();

            //string connectionString = @"Data Source=YURY-OKHRIMENKO\SQLEXPRESS;Initial Catalog=SofTrust_db;Integrated Security=True";

            //using SqlConnection connection = new SqlConnection(connectionString);

            //connection.Open();

            //List<Topic> topics = dataBase
            //                       .Topics
            //                       .FromSqlInterpolated($"SELECT * FROM topics WHERE CONVERT(INT, topic_id) = {TopicId}")
            //                       .ToList();

            //connection.Close();

            return topics;
        }
    }
}
