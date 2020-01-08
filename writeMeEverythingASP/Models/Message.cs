using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public int ChatId { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }

        [ForeignKey("Sender")]
        [Required]
        public int SenderId { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        [Required]
        public bool isDeletedFromSender { get; set; }

        [Required]
        public bool isDeletedFromReceiver { get; set; }

        public User Sender { get; set; }
        
        public User Receiver { get; set; }

        public Chat Chat { get; set; }
    }
}