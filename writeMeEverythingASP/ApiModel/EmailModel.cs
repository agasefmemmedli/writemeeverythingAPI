using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class EmailModel
    {
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public string email { get; set; }
    }
}