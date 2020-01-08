using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class FriendModel
    {

        public int FriendId { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }


        public UserModel Friend { get; set; }

    }
}