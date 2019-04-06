using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DoDoHackatonEngine
{
    public class Api
    {
        public string BaseUrl { get; set; }

        public string Token { get; set; }

        private string Post(string requestUri, string content)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            if (!string.IsNullOrEmpty(Token))
                client.DefaultRequestHeaders
                    .Add("Authorization", $"Bearer {Token}");

            return client.PostAsync(requestUri,
                new StringContent(content, Encoding.UTF8, "application/json"))
                .Result.Content.ReadAsStringAsync().Result;
        }

        private string Put(string requestUri, string content)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            if (!string.IsNullOrEmpty(Token))
                client.DefaultRequestHeaders
                    .Add("Authorization", $"Bearer {Token}");

            return client.PutAsync(requestUri,
                new StringContent(content, Encoding.UTF8, "application/json"))
                .Result.Content.ReadAsStringAsync().Result;
        }

        private string Get(string requestUri)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            if (!string.IsNullOrEmpty(Token))
                client.DefaultRequestHeaders
                    .Add("Authorization", $"Bearer {Token}");

            return client.GetAsync(requestUri).Result.Content.ReadAsStringAsync().Result;
        }

        public Mathematical GetMath()
        {
            var json = Get("/raceapi/help/math");

            return JsonConvert.DeserializeObject<Mathematical>(json);
        }

        public void Login(string userName, string password)
        {
            var json = Post("/raceapi/Auth/Login", "{'Login':'" + userName + "','Password':'" + password + "'}");

            var obj = (JObject)JsonConvert.DeserializeObject(json);

            Token = obj.GetValue("Token").Value<string>();
        }

        public MapDescription Play(string mapName)
        {
            var json = Post("/raceapi/race", "{'Map':'" + mapName + "'}");

            return JsonConvert.DeserializeObject<MapDescription>(json);
        }

        public TurnResult Move(string sessionId, Direction direction, int Acceleration)
        {
            var json = Put($"/raceapi/race/{sessionId}", "{'Direction':'" + direction.ToString() + "','Acceleration':'" + Acceleration + "'}");

            return JsonConvert.DeserializeObject<TurnResult>(json);
        }

        public void RefreshMap(string sessionId)
        {
            Get($"/raceapi/race?sessionId={sessionId}");
        }

        //public 
    }
}
