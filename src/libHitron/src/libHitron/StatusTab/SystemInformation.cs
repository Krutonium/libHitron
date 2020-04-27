using System.Net.Http;
using Newtonsoft.Json;

namespace libHitron
{
    public partial class libHitron
    {
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
    }
}