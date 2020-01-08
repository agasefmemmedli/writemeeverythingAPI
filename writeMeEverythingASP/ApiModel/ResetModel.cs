using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class ResetModel
    {
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public string resetToken { get; set; }
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string password { get; set; }
    }
}