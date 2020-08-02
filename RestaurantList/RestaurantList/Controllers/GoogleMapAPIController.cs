using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RestaurantList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleMapAPIController : ControllerBase
    {
        private readonly ILogger<GoogleMapAPIController> _logger;

        public GoogleMapAPIController(ILogger<GoogleMapAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GoogleMapAPI.GoogleMapModel Get(string keyword)
        {
            //call google map api
            string url = String.Format("https://maps.googleapis.com/maps/api/place/textsearch/json?radius=500&type=restaurant&query={0}&key=AIzaSyATZMe2AtqASrEjwarmlVVckfGZvUR1ZHY", keyword);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream outputStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(outputStream, Encoding.UTF8);
            string output = reader.ReadToEnd();
            response.Close();
            outputStream.Close();
            reader.Close();

            //parse to JSON
            GoogleMapAPI.GoogleMapModel myDeserializedClass = JsonConvert.DeserializeObject<GoogleMapAPI.GoogleMapModel>(output);

            return myDeserializedClass;
        }
    }
}
