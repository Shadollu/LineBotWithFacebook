using isRock.LineBot.Conversation;
using MerchantDB.Models;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Http;


namespace MerchantDB.Controllers
{
    public class LINEBOTController : ApiController
    {
        //doge
        public const string ChannelAccessToken = "jdoqLxieahEpwy7jE4511lu76psRpJUQGqRcjOQLapMI2gpdsD/ea7CNtXU/h9szE6PynyFmYwh11sNtHALwrUukFCgEMdH0E3WctzP/+Hpe5+jD0eqJxleaFlTIK+hi/ojGqvvW/TGy9RcIS2A+CQdB04t89/1O/w1cDnyilFU=";
        //nigga
        //public const string ChannelAccessToken = "VcC9lWBYYACb0/nxzr8rUf+CaElUkRw5dNwoz6rQCHlCySrGQs3HtZtj5RJB/qq+dIIf8vbQtH7fx23riR3SDEz1BRvpGrXz4vHwf9Tv5Y9akc2L7S+0buPuX0tvf3w3tsqY3vug7UyVWIOWlrK3YgdB04t89/1O/w1cDnyilFU=";

        [HttpGet]
        public IHttpActionResult GET()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                #region 取得一些基本資料
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //取得訊息的種類
                var Type = ReceivedMessage.events[0].type;
                //要回覆的訊息內容
                string Message = "";
                //交通部API
                PTXApi getAPIData = new PTXApi();

                //取得 UID or GID
                //狀況1.單獨與機器人交談的使用者,talkToken及senderToken都取userid,因為機器人只會回覆該使用者
                //狀況2.在群組呼叫機器人,且加了機器人好友,talkToken取userid以利資料庫存取,sendeToken取groupid,因使用狀況是在群組,故應於群組回覆
                //狀況3.在群組呼叫機器人,但沒有加機器人好友,talkToken及senderToken都取groupid,因為機器人只看得到groupid,使用者也不會和機器人私下對談
                string talkToken = "";
                string senderToken = "";
                if (ReceivedMessage.events[0].source.userId == null && ReceivedMessage.events[0].source.groupId != null)
                {
                    talkToken = ReceivedMessage.events[0].source.groupId.ToString();
                    senderToken = ReceivedMessage.events[0].source.groupId.ToString();
                }
                else if (ReceivedMessage.events[0].source.userId != null && ReceivedMessage.events[0].source.groupId == null)
                {
                    talkToken = ReceivedMessage.events[0].source.userId.ToString();
                    senderToken = ReceivedMessage.events[0].source.userId.ToString();
                }
                else
                {
                    talkToken = ReceivedMessage.events[0].source.userId.ToString();
                    senderToken = ReceivedMessage.events[0].source.groupId.ToString();
                }
                #endregion
                
                #region 判斷使用者發送的文字訊息

                if (ReceivedMessage.events[0].message.text != null)
                {
                    //狀況１ 取到完整的句子
                    switch (ReceivedMessage.events[0].message.text)
                    {
                        case "sendImageMap":
                            SendRequest.SendImageMap(senderToken);
                            break;
                        case "給我匿名網址":
                            Message = "講話不用負責任,網址給你 : https://linebotfortest.azurewebsites.net/home/index/" + senderToken;
                            break;
                        case "車子停哪":

                            DBSearch dbprocess = new DBSearch();
                            Dictionary<string, string> getParkingData = dbprocess.returnParkingData(talkToken);

                            if (getParkingData != null)
                            {
                                SendRequest.SendLocation(senderToken, getParkingData["parkingLat"], getParkingData["parkingLon"], "你車停在這啦！", "可能因天氣地形因素影響而有出入");
                            }
                            else
                            {
                                Message = "你沒有紀錄停車位置！";
                            }
                            break;

                    }
                    //狀況二,針對字串取重點字
                    if (ReceivedMessage.events[0].message.text.Length > 8 && (ReceivedMessage.events[0].message.text.Substring(0, 8) == "幫我google" || ReceivedMessage.events[0].message.text.Substring(0, 8) == "幫我GOOGLE" || ReceivedMessage.events[0].message.text.Substring(0, 8) == "幫我Google"))
                    {
                        Message = "我來幫你google:\n https://www.google.com.tw/search?hl=zh-TW&q=" + ReceivedMessage.events[0].message.text.Substring(9).Replace(" ", "%20");
                    }

                    if (ReceivedMessage.events[0].message.text.Length > 3 && ReceivedMessage.events[0].message.text.Substring(0, 3) == "我想去")
                    {
                        DBSearch dbprocess = new DBSearch();
                        Dictionary<string, string> getLoactionData = dbprocess.returnLocationData(talkToken);

                        if (getLoactionData != null)
                        {
                            string googleUrl = "https://www.google.com.tw/maps/dir/" + Uri.EscapeDataString(getLoactionData["locationLat"] + "," + getLoactionData["locationLon"]) + "/" + Uri.EscapeDataString(ReceivedMessage.events[0].message.text.Substring(3));

                            SendRequest.sendButtonTemplate(senderToken,
                                new List<isRock.LineBot.TemplateActionBase>() {
                                    new isRock.LineBot.UriAction(){ label = "點這邊開啟GoogleMap導航", uri = new Uri(googleUrl) } },
                                "幫您導航", "以下選項協助您抵達目的地", ChannelAccessToken);
                        }
                    }
                }
                #endregion

                #region 如果使用者丟圖片 ...

                //if (Type == "message" && ReceivedMessage.events[0].message.type.Trim().ToLower() == "image")
                //{
                //    isRock.LineBot.Bot bot = new isRock.LineBot.Bot(ChannelAccessToken);
                //    //取得使用者上傳的圖片
                //    var imageBytes = bot.GetUserUploadedContent(ReceivedMessage.events[0].message.id.ToString());
                //    //非同步,送給VisionRecognize進行圖像辨識
                //    VisionRecognize.MakeAnalysisRequest(imageBytes, senderToken);

                //}

                #endregion


                #region CICtest 對話流程測試

                //ConversationEntity
                isRock.LineBot.Conversation.InformationCollector<CICClass> CIC = new isRock.LineBot.Conversation.InformationCollector<CICClass>(ChannelAccessToken);
                //取得流程的結果
                ProcessResult<CICClass> result;

                //篩選對話的資料,如果不符流程問題的格式則退回重問
                CIC.OnMessageTypeCheck += (s, e) =>
                {
                    string regex = @"^(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)$";
                    Regex r = new Regex(regex, RegexOptions.IgnoreCase);

                    switch (e.CurrentPropertyName)
                    {
                        case "Geocoding":
                            if (!r.IsMatch(e.ReceievedMessage))
                            {
                                e.isMismatch = true;
                                e.ResponseMessage = "只接受座標資訊 ... \n請給我你的座標資訊,或者跟我說『取消』來結束流程 ...";
                            }
                            break;
                        case "process":
                            if (e.ReceievedMessage == "幫我導航" | e.ReceievedMessage == "幫我記錄停車位置" | e.ReceievedMessage == "最近的3個公車站" | e.ReceievedMessage == "最近的3個Youbike站")
                            {
                                e.isMismatch = false;
                            }
                            else
                            {
                                e.isMismatch = true;
                                e.ResponseMessage = "請點選選單選項,或者說『取消』來結束流程";
                                // e.CurrentPropertyName = "";
                            }
                            break;
                    }
                };
                //輸入Hey!進入程序
                if (ReceivedMessage.events[0].message.text == "Hey!")
                {
                    result = CIC.Process(ReceivedMessage.events[0], true);
                    Message = "開始程序\n";

                    SendRequest.sendButtonTemplate(senderToken,
                            new List<isRock.LineBot.TemplateActionBase>() {
                        new isRock.LineBot.MessageAction(){ label = "導航", text = "幫我導航" },
                        new isRock.LineBot.MessageAction(){ label = "記錄停車位置", text = "幫我記錄停車位置" },
                        new isRock.LineBot.MessageAction(){ label = "公車站資訊", text = "最近的3個公車站" },
                        new isRock.LineBot.MessageAction(){ label = "Youbike站資訊", text = "最近的3個Youbike站" }},
                          "Hello!", "想做些什麼？", ChannelAccessToken);
                }
                else
                {
                    if (ReceivedMessage.events[0].message.type.Trim().ToLower() != "text")
                    {
                        if (ReceivedMessage.events[0].message.latitude != 0)
                        {
                            ReceivedMessage.events[0].message.text = ReceivedMessage.events[0].message.latitude + "," + ReceivedMessage.events[0].message.longitude;
                            result = CIC.Process(ReceivedMessage.events[0]);
                        }
                        else
                        {
                            ReceivedMessage.events[0].message.text = " ";
                            result = CIC.Process(ReceivedMessage.events[0]);
                        }
                    }
                    else
                    {
                        result = CIC.Process(ReceivedMessage.events[0]);
                    }
                }


                switch (result.ProcessResultStatus)
                {
                    case ProcessResultStatus.Processed:
                        Message += result.ResponseMessageCandidate;
                        break;
                    case ProcessResultStatus.Done:
                        char[] delimiterChars = { ',' };
                        string geocoding = result.ConversationState.ConversationEntity.Geocoding;
                        string[] latAndLon = geocoding.Split(delimiterChars);

                        switch (result.ConversationState.ConversationEntity.process)
                        {
                            case "最近的3個Youbike站":
                                Message = getAPIData.returnBikeData(senderToken, latAndLon[0], latAndLon[1]);
                                break;
                            case "最近的3個公車站":
                                Message = getAPIData.returnBusData(senderToken, latAndLon[0], latAndLon[1]);
                                break;
                            case "幫我導航":
                                 DBSearch dbprocess = new DBSearch();
                                 dbprocess.SaveLocation(talkToken, ReceivedMessage);
                                Message = "想去嗎哪？格式：我想去OOXXOOXX";
                                break;
                            case "幫我記錄停車位置":
                                DBSearch dbparking = new DBSearch();
                                dbparking.testSaveParkingData(talkToken, latAndLon[0], latAndLon[1]);
                                Message = "座標已經記錄完畢！查詢請打『車子停哪』";
                                break;
                        }

                        break;
                    case ProcessResultStatus.Pass:
                        //                        Message += "你說啥我看不懂";
                        break;
                    case ProcessResultStatus.Exception:
                        Message += result.ResponseMessageCandidate;
                        break;
                    case ProcessResultStatus.Break:
                        Message += result.ResponseMessageCandidate;
                        Message += "\n若需要我請再 Hey! 一次,我會出現的";
                        break;
                    case ProcessResultStatus.InputDataFitError:
                        Message += "Oops!!\n";
                        Message += result.ResponseMessageCandidate;
                        break;
                    default:
                        break;
                }
                #endregion


                if (!string.IsNullOrEmpty(Message))
                {
                    isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }

        private void CIC_OnMessageTypeCheck(object sender, OnMessageTypeCheckEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
