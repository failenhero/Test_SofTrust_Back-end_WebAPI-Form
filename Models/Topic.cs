using System;
using System.Collections.Generic;

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
    }
}
