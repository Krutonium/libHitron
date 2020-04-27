using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace libHitron
{
    public partial class libHitron
    {
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

    }
}