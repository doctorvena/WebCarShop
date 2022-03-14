using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_CS.EF_Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string text { get; set; }
        public DateTime time { get; set; }

        public string UserID { get; set; }
        public virtual User Sender { get; set; }
    }
}
