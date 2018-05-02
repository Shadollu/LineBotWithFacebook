using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class CICClass : ConversationEntity
    {

        [Question("請問你要做些什麼？")]
        [Order(1)]
        public string process { get; set; }

        [Question("請給我你的座標")]
        [Order(2)]
        public string Geocoding { get; set; }
    }
}