using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.Models
{
    public class Friend
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        [Required]
        public int SenderId { get; set; }

        [ForeignKey("Receiver")]
        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public bool isFriend { get; set; }

        [Required]
        public bool isSenderBlocked { get; set; }

        [Required]
        public bool isReceiverBlocked { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }


        public User Sender { get; set; }

        public User Receiver { get; set; }
    }
}