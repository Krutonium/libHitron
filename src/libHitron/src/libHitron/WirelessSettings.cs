using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

namespace libHitron
{
    public partial class libHitron
    {
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
            var WirelessSettings =
                JsonConvert.DeserializeObject<WirelessSettings[]>(result.Content.ReadAsStringAsync().Result);
            foreach (var s in WirelessSettings)
            {
                if (s.ssidName.Contains("%"))
                {
                    //It's HTML Encoded for some reason, let's decode it
                    //As far as I can tell this is only a thing on some modems and not others.
                    s.ssidName = s.ssidName.Replace("u", "");
                    s.ssidName = WebUtility.UrlDecode(s.ssidName);
                }
            }
            return WirelessSettings.ToList();
        }
    }
}