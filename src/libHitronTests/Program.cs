using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libHitron;

namespace libHitronTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var P = new libHitron.libHitron();
            Console.WriteLine(P.Connect("192.168.0.1", "cusadmin", "password")); //Default IP Address, Username, and Password.
            Console.WriteLine("Login Credentials Verified. Getting Public IP...");
            var Temp1 = P.GetSystemInfo();
            Console.WriteLine(Temp1.wanIp);
            Console.WriteLine("Getting First Item from list of Port Forwards...");
            var Temp2 = P.GetPortForwards();
            Console.WriteLine(Temp2[0].appName + " " + Temp2[0].localIpAddr + ":" + Temp2[0].priStart +  " " + Temp2[0].protocal);
            
            Console.WriteLine("Name and Password of your main network...");
            var Temp3 = P.GetWirelessSettings();
            Console.WriteLine(Temp3[0].ssidName + " " + Temp3[0].passPhrase);
            Console.WriteLine(Temp3[1].ssidName + " " + Temp3[1].passPhrase);
            //Console.WriteLine(Temp3[0].hiddenWepAtkip);  //So if one wanted to, they could force the modem to broadcast and use WEP... Hooray? I don't know why this exists.
            Console.WriteLine("DOCSIS Info");
            var Temp4 = P.GetDocsisInfo();
            Console.WriteLine("DHCP Status: " + Temp4.dhcp);
            Console.ReadKey();
        }
    }
}
