using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class Register
    {
        [Required(ErrorMessageResourceType =(typeof(writeMeEverythingASP.Res.Messages)),ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string Password { get; set; }


        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string Surname { get; set; }
    }
}