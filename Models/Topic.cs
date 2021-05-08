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

        public static List<Topic> IfTopicExistInDataBase(int TopicId)
        {
            //using ADO.NET below:

            string sqlQuery = $"SELECT * FROM topics WHERE topic_id LIKE '{TopicId}'";

            List<Topic> topics = Connect.ExecuteSelectTopics(sqlQuery);

            //using Entity Framework below:
            
            //using SofTrust_dbContext dataBase = new SofTrust_dbContext();
            //
            //List<Topic> topics = dataBase.Topics.Where(t => t.TopicId == TopicId).ToList();

            return topics;
        }
    }
}
