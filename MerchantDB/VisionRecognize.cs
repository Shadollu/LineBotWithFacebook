using Microsoft.ProjectOxford.Vision.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MerchantDB
{
    public static class VisionRecognize
    {
       public static async void MakeAnalysisRequest(byte[] image,string id)
        {
            const string subscriptionKey = "0c4249a2c107480fb7af63c540d73768";
            const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze";

            HttpClient http = new HttpClient();

            // Request headers.
            http.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "visualFeatures=Categories,Description,Color&language=en";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;
            try
            {
                using (ByteArrayContent content = new ByteArrayContent(image))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    SendRequest.SendLine("分析中 ... 請稍候", id);
                    
                    response = await http.PostAsync(uri, content);


                    string contentString = await response.Content.ReadAsStringAsync();

                    Models.VRModel SerlizeData = JsonConvert.DeserializeObject<Models.VRModel>(contentString);

                    SendRequest.SendLine("圖片分析結果 ... 可能是：\n" + SerlizeData.description.captions.FirstOrDefault().text + 
                        "\n信心指數: " + SerlizeData.description.captions.FirstOrDefault().confidence, id);


                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}