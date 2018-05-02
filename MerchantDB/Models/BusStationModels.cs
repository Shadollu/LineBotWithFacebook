using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    /// <summary>
    /// 台北市公車資料欄位
    /// </summary>
    public class BusStationModels
    {
        public string StopID { get; set; }
        public string StopUID { get; set; }
        public string AuthorityID { get; set; }
        public StopName StopName { get; set; }
        public StopPosition StopPosition { get; set; }
        public string StopAddress { get; set; }
        public string Bearing { get; set; }
        public string StationID { get; set; }
        public string StopDescription { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string LocationCityCode { get; set; }
        public string UpdateTime { get; set; }
        public string VersionID { get; set; }
        public double Distance { get; set; }
    }

    public class StopName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class StopPosition
    {
        public string PositionLat { get; set; }
        public string PositionLon { get; set; }
    }
}