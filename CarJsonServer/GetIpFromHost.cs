using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CarJsonServer
{
    public static class GetIpFromHost
    {

        public static IPAddress GetIp(string hostName)
        {
            
            IPAddress[] addrs = Dns.GetHostEntry(hostName).AddressList;
            Console.WriteLine("First Ip in the list: " + addrs[0]+ " for Host: " + hostName);

            for (int i = 0; i < addrs.Length; i++)
            {
               Console.WriteLine(addrs[i]);
            }

           return addrs[0];
        }
    }
}
