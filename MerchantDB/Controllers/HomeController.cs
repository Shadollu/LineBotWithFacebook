using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MerchantDB.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(string SendValue,string id)
        {

            if (SendValue != null)
            {
                SendRequest.SendLine(SendValue, id);

                return View();
            }
            else
                return View();

        }


    }
}