using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantDB.Models
{
    public class LineImageMapModel
    {
        public string to { get; set; }
        public List<ImageMapMsg> messages { get; set; }
    }


    public class BaseSize
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Area
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Action
    {
        public string type { get; set; }
        public string text { get; set; }
        public Area area { get; set; }
    }

    public class ImageMapMsg
    {
        public string type { get; set; }
        public string baseUrl { get; set; }
        public string altText { get; set; }
        public BaseSize baseSize { get; set; }
        public List<Action> actions { get; set; }
    }


}