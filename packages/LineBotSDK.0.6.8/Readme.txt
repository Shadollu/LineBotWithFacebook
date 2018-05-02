================================================
LineBot SDK for .NET (Line@ Messaging API supported)
這是用來開發LineBot的.net  SDK
最低.net版本支援為 .net 4.5.1
================================================

已支援:
> text, image, sticker, template message傳送, 回覆
> 群組/聊天室對談
> LineWebHookControllerBase
> LineNotify, LineLogic

使用範例:
https://github.com/isdaviddong/LineBotSdkExample

如何使用請參考套件公開說明書:
http://studyhost.blogspot.tw/search/label/LineBot

簡易使用說明:
Push Message(主動發訊息給用戶):
isRock.LineBot.Utility.PushMessage(用戶id, 文字訊息, AccessToken);

Parsing Receieved Message(Parsing 收到的JSON): 
var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);

Reply Message(回覆用戶的訊息):
isRock.LineBot.Utility.ReplyMessage(ReplyToken, 文字訊息, AccessToken);

==================================================
C# Examples:

LineBotSdk範例，包含純文字訊息、貼圖、圖片發送
https://github.com/isdaviddong/LineBotSdkExample

Line_Notify_Example
https://github.com/isdaviddong/Line_Notify_Example

Line_Login_Example
https://github.com/isdaviddong/Line_Login_Example

展示如何建立一個可以接收檔案的Line WebHook 
https://github.com/isdaviddong/LineExample_WebHookGetPicture

Carousel Template Example
https://github.com/isdaviddong/LinePushCarouselTemplateExample

Line bot 連續對談機制:
https://github.com/isdaviddong/LinebotConversationExample

Line bot 群組對談範例程式碼:
https://github.com/isdaviddong/LinebotInTheGroup

================================================
使用套件有得有失，使用前請詳閱套件公開說明書，
================================================


