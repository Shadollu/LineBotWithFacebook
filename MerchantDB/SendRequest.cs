using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MerchantDB
{
    public class SendRequest
    {
        #region Line

        const string apikey = "Bearer " + Controllers.LINEBOTController.ChannelAccessToken;
        /// <summary>
        /// 發送匿名訊息用
        /// </summary>
        /// <param name="textValue">網頁上的文字方塊訊息</param>
        /// <param name="id">指定發送的ID,從route上的id擷取</param>
        public static void SendLine(string textValue, string id)
        {
            string mystr = "https://api.line.me/v2/bot/message/push";

            //  JSON
            JObject obj = new JObject();
            JObject m1 = new JObject();
            JArray msg = new JArray();

            obj.Add("to", id);
            m1.Add("type", "text");
            m1.Add("text", textValue);
            msg.Add(m1);
            obj.Add("messages", msg);

            string obj_S = JsonConvert.SerializeObject(obj);

            //POST
            Uri myuri = new Uri(mystr);
            var data = Encoding.UTF8.GetBytes(obj_S);
            SendReq(myuri, data, "application/json", "POST", apikey);

        }
        /// <summary>
        /// 發送ButtonTemplate
        /// </summary>
        /// <param name="id">指定發送的ID,此method是取groupid</param>
        /// <param name="Action">ButtonTemplate下的按鈕選單</param>
        /// <param name="buttonTitle">Template上的標題</param>
        /// <param name="buttonText">Template上的附標題</param>
        /// <param name="ChannelAccessToken">API Token</param>
        public static void sendButtonTemplate(string id, List<isRock.LineBot.TemplateActionBase> Action, string buttonTitle, string buttonText, string ChannelAccessToken)
        {

            var BtnTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                altText = "看不到去看手機啦",
                title = buttonTitle,
                text = buttonText,
                thumbnailImageUrl = new Uri("https://i.imgur.com/Z8qy7wY.png"),
                  actions = Action //設定回覆動作
            };

            isRock.LineBot.Utility.PushTemplateMessage(id, BtnTemplate, ChannelAccessToken);
        }
        /// <summary>
        /// 發送位置訊息
        /// </summary>
        /// <param name="id">定發送的ID,此method是取groupid</param>
        /// <param name="lat">緯度</param>
        /// <param name="lon">經度</param>
        public static void SendLocation(string id, string lat, string lon, string title, string address)
        {
            string mystr = "https://api.line.me/v2/bot/message/push";
            //  JSON
            JObject obj = new JObject();
            JObject m1 = new JObject();
            JArray msg = new JArray();

            obj.Add("to", id);
            m1.Add("type", "location");
            m1.Add("title", title);
            m1.Add("address", address);
            m1.Add("latitude", lat);
            m1.Add("longitude", lon);
            msg.Add(m1);
            obj.Add("messages", msg);

            string obj_S = JsonConvert.SerializeObject(obj);

            //POST
            Uri myuri = new Uri(mystr);
            var data = Encoding.UTF8.GetBytes(obj_S);
            SendReq(myuri, data, "application/json", "POST", apikey);
        }
        /// <summary>
        /// 發送與取得回傳訊息,
        /// </summary>
        /// <param name="uri">要POST的網址</param>
        /// <param name="jsonDataBytes">轉成byte[]的資訊</param>
        /// <param name="contentType">ContentType,JSON</param>
        /// <param name="method">使用的方法,POST,GET ...etc</param>
        /// <param name="authorization">Access Token</param>
        /// <returns></returns>
        /// 
        public static string SendReq(Uri uri, byte[] jsonDataBytes, string contentType, string method, string authorization)
        {
            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.ContentType = contentType;
                req.Method = method;
                req.ContentLength = jsonDataBytes.Length;
                req.Headers.Add("Authorization", authorization);

                var stream = req.GetRequestStream();
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
                stream.Close();

                WebResponse response = req.GetResponse();
                stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responseFromServer = reader.ReadToEnd();

                return responseFromServer;
            }
            catch (Exception ex)
            {
                return "OK";
            }

        }

        #endregion


        #region Facebook
        /// <summary>
        /// 發送訊息給粉絲專頁
        /// </summary>
        /// <param name="psid">交談ID</param>
        /// <param name="Value">訊息</param>
        /// <returns></returns>
        public static string sendFacebookMsg(string psid, string Value)
        {
            var request = new Models.FacebookSendMsgModels()
            {
                message = new Models.message { text = Value },
                recipient = new Models.recipient { id = psid }
            };


            try
            {
                var FANPAGE_TOKEN = "EAANZBRNCZBwLABAPGGYbVLgDEZARwziLxISDCEPM14zRhk6ujXT4UIwNdmb6WJcjJ3f5ZC29ZB4KceZANIMLqnwORkCRAWKurmQxun1ZBu2HpvCQffHUjvMT2SMbjFVxDvvUBjHq0JFHZA7Fh83iUTIpPScDQV5ZBZCDHmlOE6UYWAzwZDZD";

                var path = "https://graph.facebook.com/v2.6/me/messages?access_token=" + FANPAGE_TOKEN;

                var req = (HttpWebRequest)WebRequest.Create(path);
                req.ContentType = "application/json";
                req.Method = "POST";

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(request));
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (var response = req.GetResponse() as HttpWebResponse)
                {
                    if (req.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine(result);
                        }
                    }
                }

                return "OK";

            }
            catch (Exception ex)
            {
                return "OK";
            }


        }

        public void SendFacebookFeedByLine()
        {

        }
        #endregion


        #region Test

        public static void SendImageMap(string id)
        {
            {
                string mystr = "https://api.line.me/v2/bot/message/push";

                Models.LineImageMapModel result = new Models.LineImageMapModel()
                {
                    to = id,
                    messages = new List<Models.ImageMapMsg>
                    {
                        new Models.ImageMapMsg
                        {
                            type = "imagemap",
                            altText = "test1234",
                            baseUrl = "https://i.imgur.com/DbLrZgv.png",
                            baseSize = new Models.BaseSize
                            {
                                height = 1040,
                                width = 1040
                            },
                            actions = new List<Models.Action>
                            {
                                new Models.Action
                                {
                                    type = "message",
                                    text = "停車紀錄",
                                    area = new Models.Area
                                    {
                                        x=0,y=520,width=520,height=520
                                    }
                                },
                                new Models.Action
                                {
                                    type = "message",
                                    text = "導航",
                                    area = new Models.Area
                                    {
                                        x=0,y=0,width=520,height=520
                                    }
                                },
                                new Models.Action
                                {
                                    type = "message",
                                    text = "找Youbike",
                                    area = new Models.Area
                                    {
                                        x=520,y=0,width=520,height=520
                                    }
                                },
                               new Models.Action
                                {
                                    type = "message",
                                    text = "附近公車站",
                                    area = new Models.Area
                                    {
                                        x=520,y=520,width=520,height=520
                                    }
                                }
                            }
                        }
                    }
                };


                string obj_S = JsonConvert.SerializeObject(result);

                //POST
                Uri myuri = new Uri(mystr);
                var data = Encoding.UTF8.GetBytes(obj_S);
                SendReq(myuri, data, "application/json", "POST", apikey);
            }
            #endregion
        }
    }
}