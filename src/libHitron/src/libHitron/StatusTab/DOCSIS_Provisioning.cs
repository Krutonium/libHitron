using System.Net.Http;
using Newtonsoft.Json;

namespace libHitron
{
    public partial class libHitron
    {
        //http://192.168.0.1/data/getCMInit.asp
        public class DOCSIS_Info
        {
            public string hwInit { get; set; }
            public string findDownstream { get; set; }
            public string ranging { get; set; }
            public string dhcp { get; set; }
            public string timeOfday { get; set; }
            public string downloadCfg { get; set; }
            public string registration { get; set; }
            public string eaeStatus { get; set; }
            public string bpiStatus { get; set; }
            public string networkAccess { get; set; }
            public string trafficStatus { get; set; }
        }

        public DOCSIS_Info GetDocsisInfo()
        {
            var client = login();
            var message = new HttpRequestMessage(HttpMethod.Get, "data/getCMInit.asp");
            var result = client.SendAsync(message).Result;
            var json = (result.Content.ReadAsStringAsync().Result);
            var Step1 = JsonConvert.DeserializeObject<DOCSIS_Info[]>(json);
            return Step1[0];
        }
    }
}