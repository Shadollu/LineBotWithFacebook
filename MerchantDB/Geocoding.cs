using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MerchantDB
{
    public class Geocoding
    {
        /// <summary>
        /// 取得兩座標距離
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static double DistanceOfTwoPoints(double lat1, double lng1, double lat2, double lng2)
        {
            double radLng1 = lng1 * Math.PI / 180.0;
            double radLng2 = lng2 * Math.PI / 180.0;
            double a = radLng1 - radLng2;
            double b = (lat1 - lat2) * Math.PI / 180.0;
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                Math.Cos(radLng1) * Math.Cos(radLng2) * Math.Pow(Math.Sin(b / 2), 2))
                ) * 6378.137;
            s = Math.Round(s * 10000) / 10000;

            return s;
        }
        /// <summary>
        /// 將地址轉換為經緯度,使用googleAPI
        /// </summary>
        /// <param name="Address">中文地址</param>
        /// <returns></returns>
        public static Dictionary<string, string> getGeocoding(string Address)
        {

            string APIKey = "AIzaSyBZTjKhYY10jMfmadS3g270Q8ADc3XBPPQ";
            string EscAddress = Uri.EscapeUriString(Address);
            string startUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + EscAddress + "&key =" + APIKey;

            WebClient Client = new WebClient();
            string JsonStr = Encoding.UTF8.GetString(Client.DownloadData(new Uri(startUrl)));
            JObject obj = JsonConvert.DeserializeObject(JsonStr) as JObject;

            Dictionary<string, string> Geo = new Dictionary<string, string>();

            Geo.Add("Lat", obj.SelectToken("results[0].geometry.location.lat").ToString());
            Geo.Add("Lon", obj.SelectToken("results[0].geometry.location.lng").ToString());

            return Geo;

        }
    }
}