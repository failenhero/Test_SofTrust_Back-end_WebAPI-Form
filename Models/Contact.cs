using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI_Form.Models
{
    public partial class Contact
    {
        public Contact()
        {
            AllMessages = new HashSet<AllMessage>();
        }

        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public virtual ICollection<AllMessage> AllMessages { get; set; }
    }
}
