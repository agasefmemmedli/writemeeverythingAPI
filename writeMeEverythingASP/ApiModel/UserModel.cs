using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class UserModel
    {

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Surname { get; set; }

        public DateTime Lastseen { get; set; }

        public bool isOnline { get; set; }
        public string Avatar { get; set; }

        [MinLength(3)]
        [MaxLength(1000)]
        public string About { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
        public string City { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }

    }
}