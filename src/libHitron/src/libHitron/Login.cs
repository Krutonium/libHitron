using System;
using System.Net.Http;

namespace libHitron
{
    public partial class libHitron
    {
        private HttpClient login()
        {
            var baseAddress = _IP;
            var handler = new HttpClientHandler { UseCookies = true };
            var client = new HttpClient(handler) { BaseAddress = baseAddress };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var message = new HttpRequestMessage(HttpMethod.Get, "/goform/login?user=" + _Username + "&pws=" + _Password);
            var result = client.SendAsync(message).Result;
            if (!result.Content.ReadAsStringAsync().Result.StartsWith("success"))
            {
                Console.WriteLine("Failed to login to the modem.");
                Console.WriteLine(result);
            }
            return client;
        }
        private Boolean testlogin()
        {
            var baseAddress = _IP;
            var handler = new HttpClientHandler { UseCookies = true };
            var client = new HttpClient(handler) { BaseAddress = baseAddress };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var message = new HttpRequestMessage(HttpMethod.Get, "/goform/login?user=" + _Username + "&pws=" + _Password);
            var result = client.SendAsync(message).Result;
            if (!result.Content.ReadAsStringAsync().Result.StartsWith("success"))
            {
                return false;
            }
            return true;
        }
        
        private static Uri _IP;
        private static string _Username;
        private static string _Password;

        public Boolean Connect(string IP, string Username, string Password)
        {
            _IP = new Uri("http://" +IP);
            _Username = Username;
            _Password = Password;
            return testlogin();
        }
    }
}