using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class BusStopModels
    {
        public string StationUID { get; set; }
        public string StationID { get; set; }
        public StationName StationName { get; set; }
        public StationPosition StationPosition { get; set; }
        public string StationAddress { get; set; }
        public string UpdateTime { get; set; }
        public string VersionID { get; set; }
        public double Distance { get; set; }
    }

    public class StationName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class StationPosition
    {
        public string PositionLat { get; set; }
        public string PositionLon { get; set; }
    }

}