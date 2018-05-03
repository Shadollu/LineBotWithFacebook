using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using MerchantDB.Models;
using System.Text.RegularExpressions;

namespace MerchantDB.Controllers
{
    public class FACEBOOKBOTController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GET()
        {
            string hub_mode = "";
            string hub_challenge = "";
            string hub_verify_token = "";
            string channelAccessToken = "EAANZBRNCZBwLABAPGGYbVLgDEZARwziLxISDCEPM14zRhk6ujXT4UIwNdmb6WJcjJ3f5ZC29ZB4KceZANIMLqnwORkCRAWKurmQxun1ZBu2HpvCQffHUjvMT2SMbjFVxDvvUBjHq0JFHZA7Fh83iUTIpPScDQV5ZBZCDHmlOE6UYWAzwZDZD";
            try
            {
                IEnumerable<KeyValuePair<string, string>> queryString = Request.GetQueryNameValuePairs();

                foreach (KeyValuePair<string, string> item in queryString)
                {
                    switch (item.Key)
                    {
                        case "hub.mode":
                            hub_mode = item.Value;
                            break;
                        case "hub.challenge":
                            hub_challenge = item.Value;
                            break;
                        case "hub.verify_token":
                            hub_verify_token = item.Value;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (hub_verify_token == channelAccessToken)
            {
                string result = hub_challenge;
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");
                return resp;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }


        }
        [HttpPost]
        public IHttpActionResult POST()
        {
            //U6ff789d36d6f22b7484a0ad6d8b32d5d
            try
            {
                string postData = Request.Content.ReadAsStringAsync().Result;

                FacebookMsgModels getData = JsonConvert.DeserializeObject<FacebookMsgModels>(UnicodeConverter(postData));

                string result = "發送者：" + getData.entry.First().messaging.First().sender.id + "\n" + "私訊內容：" + getData.entry.First().messaging.First().message.text;
                Geocoding.test = getData.entry.First().messaging.First().sender.id;
                SendRequest.SendLine(result, "U6ff789d36d6f22b7484a0ad6d8b32d5d");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok();
        }

        public void logging(string status)
        {
            DateTime Date = DateTime.Now;
            string TodyMillisecond = Date.ToString("yyyy-MM-dd HH:mm:ss");
            string Tody = Date.ToString("yyyy-MM-dd");

            if (!Directory.Exists("D:\\FACEBOOK_LOG"))
            {
                //新增資料夾
                Directory.CreateDirectory("D:\\FACEBOOK_LOG");
            }

            File.AppendAllText("D:\\FACEBOOK_LOG\\" + Tody + ".txt", "\r\n" + TodyMillisecond + "：" + status);
        }


        private string UnicodeConverter(string str)
        {
            str = Regex.Replace(str, "\\\\u\\w{4}",

                      delegate (Match m)
                      {
                          return ((char)(int.Parse(m.Value.Substring(2), System.Globalization.NumberStyles.HexNumber))).ToString();
                      });

            return str;
        }
    }
}
