using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class YoubikeModel
    {
        public string StationUID { get; set; }
        public string StationID { get; set; }
        public string AuthorityID { get; set; }
        public StationName StationName { get; set; }
        public StationPosition StationPosition { get; set; }
        public StationAddress StationAddress { get; set; }
        public string StopDescription { get; set; }
        public int BikesCapacity { get; set; }
        public string SrcUpdateTime { get; set; }
        public string UpdateTime { get; set; }
        public double Distance { get; set; }
    }

    public class StationAddress
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

}