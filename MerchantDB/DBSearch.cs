using MerchantDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB
{
    public class DBSearch
    {
        LineBotEntities LinebotDB = new LineBotEntities();
        /// <summary>
        /// 儲存使用者座標
        /// </summary>
        /// <param name="talkToken">使用者的UID</param>
        /// <param name="ReceivedMessage">使用者的中文地址</param>
        public void SaveLocation(string talkToken, isRock.LineBot.ReceievedMessage ReceivedMessage)
        {

            var location = LinebotDB.LAT_LON.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (location.Count() == 0)
            {
                string address = "";
                if (ReceivedMessage.events[0].message.address.ToString().Length > ReceivedMessage.events[0].message.title.ToString().Length)
                {
                    address = ReceivedMessage.events[0].message.address.ToString();
                }
                else
                {
                    address = ReceivedMessage.events[0].message.title.ToString();
                }

                var adv = new LAT_LON
                {
                    UID = talkToken,
                    LAT = ReceivedMessage.events[0].message.latitude.ToString(),
                    LON = ReceivedMessage.events[0].message.longitude.ToString(),
                    COUNTRY = address.Substring(5, 3)
                };

                LinebotDB.LAT_LON.Add(adv);
                LinebotDB.SaveChanges();
            }

        }

        public void DeleteLocation(string talkToken)
        {
            var adv = LinebotDB.PARKING.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
            LinebotDB.PARKING.Remove(adv);
            LinebotDB.SaveChanges();
        }
        /// <summary>
        /// 從DB取回使用者停車的座標
        /// </summary>
        /// <param name="talkToken"></param>
        /// <returns></returns>
        public Dictionary<string, string> returnParkingData(string talkToken)
        {
            var parkingloc = LinebotDB.PARKING.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (parkingloc.Count() > 0)
            {
                Dictionary<string, string> parkingData = new Dictionary<string, string>();

                parkingData.Add("parkingLat", parkingloc.First().LAT);
                parkingData.Add("parkingLon", parkingloc.First().LON);

                var adv = LinebotDB.PARKING.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
                LinebotDB.PARKING.Remove(adv);
                LinebotDB.SaveChanges();

                return parkingData;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 從DB取回使用者所在地的座標
        /// </summary>
        /// <param name="talkToken"></param>
        /// <returns></returns>
        public Dictionary<string, string> returnLocationData(string talkToken)
        {
            var location = LinebotDB.LAT_LON.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (location.Count() > 0)
            {
                Dictionary<string, string> LocationData = new Dictionary<string, string>();

                LocationData.Add("locationLat", location.First().LAT);
                LocationData.Add("locationLon", location.First().LON);

                var adv = LinebotDB.LAT_LON.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
                LinebotDB.LAT_LON.Remove(adv);
                LinebotDB.SaveChanges();

                return LocationData;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 使用者選擇記住停車位時,另外將座標存至Parking
        /// </summary>
        /// <param name="talkToken"></param>
        public void SaveParkingData(string talkToken)
        {
            var parkingloc = LinebotDB.PARKING.Where(x => x.UID.ToString().Equals(talkToken)).ToList();
            var location = LinebotDB.LAT_LON.Where(x => x.UID.ToString().Equals(talkToken)).ToList();

            if (parkingloc.Count() == 0)
            {
                var parking = new PARKING
                {
                    UID = location.First().UID,
                    LAT = location.First().LAT,
                    LON = location.First().LON
                };
                LinebotDB.PARKING.Add(parking);
                LinebotDB.SaveChanges();

                //確認使用者只需儲存停車座標,故把原本存在LAT_LON的資料刪除
                var adv = LinebotDB.LAT_LON.FirstOrDefault(x => x.UID.ToString().Equals(talkToken));
                LinebotDB.LAT_LON.Remove(adv);
                LinebotDB.SaveChanges();
            }
        }

        public void testSaveParkingData(string talkToken, string Lat, string Lon)
        {
            var parkingloc = LinebotDB.PARKING.Where(x => x.UID.ToString().Equals(talkToken)).ToList();


            if (parkingloc.Count() == 0)
            {
                var parking = new PARKING
                {
                    UID = talkToken,
                    LAT = Lat,
                    LON = Lon
                };
                LinebotDB.PARKING.Add(parking);
                LinebotDB.SaveChanges();

            }
        }
    }
}