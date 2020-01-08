using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class MessageModel
    {

        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        [MaxLength(500, ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "MaxLength50")]
        public string Content { get; set; }
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public DateTime CreateAt { get; set; }

        [ForeignKey("Sender")]
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public int SenderId { get; set; }

        [ForeignKey("Receiver")]
        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public int ReceiverId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public bool isDeletedFromSender { get; set; }

        [Required(ErrorMessageResourceType = (typeof(writeMeEverythingASP.Res.Messages)), ErrorMessageResourceName = "Required")]
        public bool isDeletedFromReceiver { get; set; }

    }
}