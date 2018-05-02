using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class BusComingModels
    {
        public string PlateNumb { get; set; }
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public StopName StopName { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public RouteName RouteName { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public SubRouteName SubRouteName { get; set; }
        public string Direction { get; set; }
        public int EstimateTime { get; set; }
        public int StopCountDown { get; set; }
        public string CurrentStop { get; set; }
        public string DestinationStop { get; set; }
        public int StopSequence { get; set; }
        public string StopStatus { get; set; }
        public string MessageType { get; set; }
        public string NextBusTime { get; set; }
        public bool IsLastBus { get; set; }
        public string DataTime { get; set; }
        public string TransTime { get; set; }
        public string SrcRecTime { get; set; }
        public string SrcTransTime { get; set; }
        public string SrcUpdateTime { get; set; }
        public string UpdateTime { get; set; }
    }

    public class RouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class SubRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

}