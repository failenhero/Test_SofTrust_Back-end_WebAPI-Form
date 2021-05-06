using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI_Form.Models
{
    public partial class AllMessage
    {
        public int MessageId { get; set; }
        public int RfContactId { get; set; }
        public int RfTopicId { get; set; }
        public string MessageText { get; set; }

        public virtual Contact RfContact { get; set; }
        public virtual Topic RfTopic { get; set; }
    }
}
