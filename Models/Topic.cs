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
            using (SofTrust_dbContext dataBase = new SofTrust_dbContext())
            {
                return dataBase
                        .Topics
                        .FromSqlInterpolated($"SELECT * FROM topics WHERE CONVERT(INT, topic_id) = {TopicId}")
                        .ToList();
            }
        }
    }
}
