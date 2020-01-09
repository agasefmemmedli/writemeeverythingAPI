using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace writeMeEverythingASP.ApiModel
{
    public class ReplyFriendRequest
    {
        public int FriendId { get; set; }
        public bool ReplyRequest { get; set; }
    }
}