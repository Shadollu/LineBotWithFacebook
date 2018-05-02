using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class YoubikeStationStatusModel
    {
        public string StationUID { get; set; }
        public string StationID { get; set; }
        public string ServieAvailable { get; set; }
        public int AvailableRentBikes { get; set; }
        public int AvailableReturnBikes { get; set; }
        public string SrcUpdateTime { get; set; }
        public string UpdateTime { get; set; }
    }
}