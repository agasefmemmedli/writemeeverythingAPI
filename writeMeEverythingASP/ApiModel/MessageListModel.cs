using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using writeMeEverythingASP.Models;

namespace writeMeEverythingASP.ApiModel
{
    public class MessageListModel
    {
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public Message Message { get; set; }


        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public int limit { get; set; }



    }
}