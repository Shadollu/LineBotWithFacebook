using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class FacebookSendMsgModels
    {
        public recipient recipient { get; set; }
        public message message { get; set; }
    }

    public class recipient

    {
        public string id { get; set; }
    }

    public class message
    {
        public string text { get; set; }
    }
}