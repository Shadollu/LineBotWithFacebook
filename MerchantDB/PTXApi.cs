using MerchantDB.Models;
using MerchantDB.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace MerchantDB
{
    /// <summary>
    /// 大眾運輸工具API串接,使用HMAC
    /// </summary>
    public class PTXApi
    {
        /// <summary>
        /// 使用HMAC方式對交通部取資料
        /// </summary>
        /// <param name="APIUrl">要呼叫的API網址</param>
        /// <returns></returns>
        private string CallAPIByHMAC(string APIUrl)
        {
            //APPID
            string APPID = "cdbf9aa863b24199bd3d4fd696524980";
            //APPKey
            string APPKey = "5xPGvUFNSd5c0mzuxYXtAE4u4gw";

            //取得當下UTC時間
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string SignDate = "x-date: " + xdate;
            //取得加密簽章
            string Signature = HMAC_SHA1.Signature(SignDate, APPKey);
            string sAuth = "hmac username=\"" + APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + Signature + "\"";

            string Result = string.Empty;

            using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                Client.DefaultRequestHeaders.Add("x-date", xdate);
                Result = Client.GetStringAsync(APIUrl).Result;
            }

            return Result;


        }
        /// <summary>
        /// 指定座標最近的三個公車站點
        /// </summary>
        /// <param name="lat">指定緯度</param>
        /// <param name="lon">指定經度</param>
        /// <returns></returns>
        public string returnBusData(string talkToken)
        {
            PTXApi getBusLocation = new PTXApi();
            string BusName = "";

            LineBotEntities LinebotDB = new LineBotEntities();

            var location = LinebotDB.LAT_LON.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (location.Count > 0)
            {
                //取得所有公車站座標
                string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bus/Station/City/Taipei?$format=JSON";
                List<BusStopModels> getData = JsonConvert.DeserializeObject<List<Models.BusStopModels>>(CallAPIByHMAC(APIUrl));

                //依照距離排序公車站
                foreach (var data in getData)
                {
                    var distance = Math.Abs(Geocoding.DistanceOfTwoPoints(Convert.ToDouble(data.StationPosition.PositionLat), Convert.ToDouble(data.StationPosition.PositionLon), Convert.ToDouble(location.First().LAT), Convert.ToDouble(location.First().LON)));
                    data.Distance = distance;
                }
                //取前三名
                var finalLocation = getData.OrderBy(x => x.Distance).Take(3);

                //組字串
                if (finalLocation.Count() > 0)
                {
                    BusName = "附近的公車站：" + Environment.NewLine;
                    foreach (var item in finalLocation)
                    {
                        BusName += item.StationName.Zh_tw + Environment.NewLine + "下一班進站公車：" + BusComing(item.StationName.Zh_tw) + Environment.NewLine;
                    }

                    var adv = LinebotDB.LAT_LON.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
                    LinebotDB.LAT_LON.Remove(adv);
                    LinebotDB.SaveChanges();

                    return BusName;
                }
                else
                    return "";

            }
            else
                return "";
        }
        /// <summary>
        /// 取得站位即將到站的公車
        /// </summary>
        /// <param name="stationName">公車站名(中文)</param>
        /// <returns></returns>
        public string BusComing(string stationName)
        {
            string data = Uri.EscapeDataString(stationName);

            PTXApi getBusLocation = new PTXApi();

            string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taipei?$filter=StopName%2FZh_tw%20eq%20'" + data + "'%20and%20StopStatus%20eq%20null&$orderby=EstimateTime&$top=30&$format=JSON";

            List<BusComingModels> getData = new List<Models.BusComingModels>();
            getData = JsonConvert.DeserializeObject<List<Models.BusComingModels>>(CallAPIByHMAC(APIUrl));
            string result = "";

            var finalData = getData.Where(x => x.StopStatus == null).OrderBy(y => y.EstimateTime);

            //if (Convert.ToInt16(getData.First().EstimateTime) /60 <= 0)
            //{
            //    result = getData.First().RouteName.Zh_tw + ", 即將進站";
            //}
            //else
            result = finalData.First().RouteName.Zh_tw + ",還有" + Math.Round(Convert.ToDecimal(finalData.First().EstimateTime) / 60, 2) + "分鐘";

            return result;
        }
        /// <summary>
        /// 取得座標最近的Youbike站,並且提供該站的車輛資訊
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public string returnBikeData(string id, string talkToken)
        {

            LineBotEntities LinebotDB = new LineBotEntities();

            var location = LinebotDB.LAT_LON.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (!string.IsNullOrEmpty(location.First().COUNTRY))
            {
                string country = "";
                switch (location.First().COUNTRY)
                {
                    case "台北市":
                        country = "Taipei";
                        break;
                    case "新北市":
                        country = "NewTaipei";
                        break;
                    case "桃園市":
                        country = "Taoyuan";
                        break;
                    case "台中市":
                        country = "Taichung";
                        break;
                    case "台南市":
                        country = "Tainan";
                        break;
                    case "高雄市":
                        country = "Kaohsiung";
                        break;
                    case "屏東縣":
                        country = "PingtungCounty";
                        break;
                    default:
                        break;
                }

                PTXApi getBusLocation = new PTXApi();

                string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bike/Station/" + country + "?$format=JSON";
                List<YoubikeModel> getData = JsonConvert.DeserializeObject<List<Models.YoubikeModel>>(CallAPIByHMAC(APIUrl));

                foreach (var data in getData)
                {
                    var distance = Math.Abs(Geocoding.DistanceOfTwoPoints(Convert.ToDouble(data.StationPosition.PositionLat), Convert.ToDouble(data.StationPosition.PositionLon), Convert.ToDouble(location.First().LAT), Convert.ToDouble(location.First().LON)));
                    data.Distance = distance;
                }

                var finalLocation = getData.OrderBy(x => x.Distance).Take(3);

                if (finalLocation.Count() > 0)
                {
                    foreach (var item in finalLocation)
                    {
                        string stationStatus = BikeStationStatus(item.StationUID, country);

                        SendRequest.SendLocation(id, item.StationPosition.PositionLat, item.StationPosition.PositionLon, item.StationName.Zh_tw, stationStatus);
                    }

                    var adv = LinebotDB.LAT_LON.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
                    LinebotDB.LAT_LON.Remove(adv);
                    LinebotDB.SaveChanges();

                    return "Youbike站台資訊如上";
                }
                else
                    return "";
            }
            else
                return "";


        }
        /// <summary>
        /// 利用UID取得該站台可租借的車數有多少
        /// </summary>
        /// <param name="stationUID"></param>
        /// <returns></returns>
        public string BikeStationStatus(string stationUID, string country)
        {
            PTXApi getBusLocation = new PTXApi();
            string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bike/Availability/" + country + "?$filter=StationUID%20eq%20'" + stationUID + "'&$format=JSON";
            List<YoubikeStationStatusModel> getData = JsonConvert.DeserializeObject<List<YoubikeStationStatusModel>>(CallAPIByHMAC(APIUrl));

            if (getData.First().ServieAvailable == "0")
            {
                return "此站台目前停止營運";
            }
            else
            {
                return "目前可租借車數：" + getData.First().AvailableRentBikes + Environment.NewLine + "可歸還數：" + getData.First().AvailableReturnBikes;
            }

        }



        public string testBikeData(string id,string lat,string lon)
        {

  
                PTXApi getBusLocation = new PTXApi();

                string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bike/Station/taipei?$format=JSON";
                List<YoubikeModel> getData = JsonConvert.DeserializeObject<List<Models.YoubikeModel>>(CallAPIByHMAC(APIUrl));

                foreach (var data in getData)
                {
                    var distance = Math.Abs(Geocoding.DistanceOfTwoPoints(Convert.ToDouble(data.StationPosition.PositionLat), Convert.ToDouble(data.StationPosition.PositionLon), Convert.ToDouble(lat), Convert.ToDouble(lon)));
                    data.Distance = distance;
                }

                var finalLocation = getData.OrderBy(x => x.Distance).Take(3);

                if (finalLocation.Count() > 0)
                {
                    foreach (var item in finalLocation)
                    {
                        string stationStatus = BikeStationStatus(item.StationUID, "taipei");
                       SendRequest.SendLocation(id, item.StationPosition.PositionLat, item.StationPosition.PositionLon, item.StationName.Zh_tw, stationStatus);
                    }
                    return "Youbike站台資訊如上";
                }
                else
                    return "";
            }

        public string testBusData(string talkToken,string lat,string lon)
        {
            PTXApi getBusLocation = new PTXApi();
            string BusName = "";


                //取得所有公車站座標
                string APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Bus/Station/City/Taipei?$format=JSON";
                List<BusStopModels> getData = JsonConvert.DeserializeObject<List<Models.BusStopModels>>(CallAPIByHMAC(APIUrl));

                //依照距離排序公車站
                foreach (var data in getData)
                {
                    var distance = Math.Abs(Geocoding.DistanceOfTwoPoints(Convert.ToDouble(data.StationPosition.PositionLat), Convert.ToDouble(data.StationPosition.PositionLon), Convert.ToDouble(lat), Convert.ToDouble(lon)));
                    data.Distance = distance;
                }
                //取前三名
                var finalLocation = getData.OrderBy(x => x.Distance).Take(3);

                //組字串
                if (finalLocation.Count() > 0)
                {
                    BusName = "附近的公車站：" + Environment.NewLine;
                    foreach (var item in finalLocation)
                    {
                        BusName += item.StationName.Zh_tw + Environment.NewLine + "下一班進站公車：" + BusComing(item.StationName.Zh_tw) + Environment.NewLine;
                    }
                    
                    return BusName;
                }
                else
                    return "";

        }

    }
    }
