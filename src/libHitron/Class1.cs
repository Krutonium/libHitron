using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
namespace libHitron
{
    public class libHitron
    {
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
        #region Wireless Settings
        public class WirelessSettings
        {
            public int id { get; set; }
            public string band { get; set; }
            public string ssidName { get; set; }
            public string enable { get; set; }
            public string visible { get; set; }
            public string wmm { get; set; }
            public int authMode { get; set; }
            public int encryptType { get; set; }
            public int securityMode { get; set; }
            public string passPhrase { get; set; }
            public string hiddenWepAtkip { get; set; }
            public string wlsEnable { get; set; }
            public string wepKey1 { get; set; }
            public string wepKey2 { get; set; }
            public string wepKey3 { get; set; }
            public string wepKey4 { get; set; }
            public string txKey { get; set; }
        }
        public List<WirelessSettings> GetWirelessSettings()
        {
            var client = login();
            var message = new HttpRequestMessage(HttpMethod.Get, "data/wireless_ssid.asp");
            var result = client.SendAsync(message).Result;
            var WirelessSettings = JsonConvert.DeserializeObject<WirelessSettings[]>(result.Content.ReadAsStringAsync().Result);
            return WirelessSettings.ToList();
        }
        #endregion
        #region Port Forwards
        public class PortForwards
        {
            public string appName { get; set; }
            public string pubStart { get; set; }
            public string pubEnd { get; set; }
            public string priStart { get; set; }
            public string priEnd { get; set; }
            public string protocal { get; set; }
            public string localIpAddr { get; set; }
            public string remoteIpStar { get; set; }
            public string remoteIpEnd { get; set; }
            public string ruleOnOff { get; set; }
        }

        public List<PortForwards> GetPortForwards()
        {
            var client = login();
            var message = new HttpRequestMessage(HttpMethod.Get, "data/getForwardingRules.asp");
            var result = client.SendAsync(message).Result;
            var Firewall = JsonConvert.DeserializeObject<PortForwards[]>(result.Content.ReadAsStringAsync().Result);
            return Firewall.ToList();
        }

        #endregion
        #region System Info
        public class System_Info
        {
            public string hwVersion { get; set; }
            public string swVersion { get; set; }
            public string serialNumber { get; set; }
            public string rfMac { get; set; }
            public string wanIp { get; set; }
            public string systemLanUptime { get; set; }
            public string systemWanUptime { get; set; }
            public string systemTime { get; set; }
            public string timezone { get; set; }
            public string WRecPkt { get; set; }
            public string WSendPkt { get; set; }
            public string lanIp { get; set; }
            public string LRecPkt { get; set; }
            public string LSendPkt { get; set; }
        }
        public System_Info GetSystemInfo()
        {
            var client = login();
            var message = new HttpRequestMessage(HttpMethod.Get, "data/getSysInfo.asp");
            var result = client.SendAsync(message).Result;
            var json = (result.Content.ReadAsStringAsync().Result);
            var Step1 = JsonConvert.DeserializeObject<System_Info[]>(json);
            var SysInfo = Step1[0];
            return SysInfo;
        }
        #endregion
        #region Login
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
        #endregion
    }
}
