using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LAB_API
{
    public class Currencies
    {
        Dictionary<string, string> Mone;

        public Dictionary<string, string> return_Mone()
        { return Mone; }
        public void Fill_Mone()
        {
            string data = this.Connect_symbol();
            Mone = new Dictionary<string, string>();

            Dictionary<string, object> nazwa = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

            if ((bool)nazwa["success"])
            {
                var temp = (JObject)nazwa["symbols"];
                Mone=temp.ToObject<Dictionary<string, string>>();
            }
        }


        public double Convert(string from_currency, string to_currency, double amount)
        {
            string data2 = this.Connect_convert($"exchangerates_data/convert?to={to_currency}&from={from_currency}&amount={amount}");

            Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(data2);
            if ((bool)response["success"])
                return (double)response["result"];
            else
                return -1;
        }

        public string Connect_symbol()
        {
            var client = new RestClient("https://api.apilayer.com");

            var request = new RestRequest("exchangerates_data/symbols", Method.Get);
            request.AddHeader("apikey", "3zyKCBy9DYDbFx8KdLckK1D8UMDRfaSo");

            RestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Connect_convert(string url)
        {
            var client = new RestClient($"https://api.apilayer.com/");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("apikey", "3zyKCBy9DYDbFx8KdLckK1D8UMDRfaSo");

            RestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
