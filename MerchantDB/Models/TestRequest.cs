using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class TestRequest : ConversationEntity
    {
        [Question("請問您要請的假別是?")]
        [Order(1)]
        public string Data1 { get; set; }
        //[Question("代理人？")]
        //[Order(2)]
        //public string Data2 { get; set; }

        //[Question("請假日期？")]
        //[Order(3)]
        //public DateTime Date { get; set; }

        //[Question("什麼時候")]
        //[Order(4)]
        //public DateTime StartTime { get; set; }

        //[Question("幾個小時")]
        //[Order(5)]
        //public float hour { get; set; }
    }
}