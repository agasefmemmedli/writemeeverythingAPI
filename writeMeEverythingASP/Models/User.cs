using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.Models
{
    public class User
    {

        public int Id { get; set; }


        [Required,MaxLength(50)]
        public string Email { get; set; }


        [Required, MaxLength(50)]
        public string Name { get; set; }


        [Required, MaxLength(50)]
        public string Surname { get; set; }


        [Required, MaxLength(100)]
        public string Password { get; set; }


        [Required]
        public bool Verify { get; set; }
        public string VerifyText { get; set; }

        public DateTime Lastseen { get; set; }
        public string Token { get; set; }

        public bool isOnline { get; set; }
        public string ResetText { get; set; }
        public string Avatar { get; set; }

        [MinLength(3)]
        [MaxLength(1000)]
        public string About { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
        public string City { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }


    }
}