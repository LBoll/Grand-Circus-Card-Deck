using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /*
        public ActionResult Weather()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/");
            WR.UserAgent = ".Net Framework Test Client";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();

            StreamReader reader = new StreamReader(Response.GetResponseStream());

            string WeatherData = reader.ReadToEnd();

            ViewBag.Weather = WeatherData;
            
            JObject JsonData = JObject.Parse(WeatherData);

            ViewBag.Times = JsonData["time"]["startPeriodName"];
            ViewBag.Temps = JsonData["data"]["temperature"];
            ViewBag.Icons = JsonData["data"]["iconLink"];
            ViewBag.Descs = JsonData["data"]["weather"];
            ViewBag.Texts = JsonData["data"]["text"]; 
            return View();
        }
            */
        public ActionResult Weather()
        {
            
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string DeckData = Reader.ReadToEnd();

            JObject JsonDeck = JObject.Parse(DeckData);
            ViewBag.DeckId = JsonDeck["deck_id"];
            
            HttpWebRequest WRR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/" + ViewBag.DeckId + "/draw/?count=5");

            WRR.UserAgent = ".NET Framework Test Client";
            HttpWebResponse NewResponse = (HttpWebResponse)WRR.GetResponse();
            StreamReader NewReader = new StreamReader(NewResponse.GetResponseStream());

            string DrawData = NewReader.ReadToEnd();

            JObject NewJsonDeck = JObject.Parse(DrawData);

            ViewBag.Draw = NewJsonDeck["cards"];
            
            return View();
        }
    }
}