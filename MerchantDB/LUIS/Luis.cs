using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MerchantDB.LUIS
{
    public class Luis
    {

        public static LUISSerialization getResponse(string msg)
        {
            WebClient client = new WebClient();

            var escMsg = Uri.EscapeUriString(msg);

            const string APIKEY = "de9906ad1b5245c7b5a78c8dbb1b5b00";

            string url = @"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/302dfe54-e7c0-4f94-9f67-22e63a166d06?subscription-key=" + APIKEY + "&verbose=true&timezoneOffset=480&q=" + escMsg;

            string JsonStr = Encoding.UTF8.GetString(client.DownloadData(new Uri(url)));

            LUISSerialization getData = JsonConvert.DeserializeObject<LUISSerialization>(JsonStr);

            return getData;
        }
        

       
    }
}