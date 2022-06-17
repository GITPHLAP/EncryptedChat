using System.Net;
using System.Net.Sockets;

namespace ChatModels
{
    public class IPAddressUtillity
    {
        public static IPAddress GetLocalIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress address in ipHostInfo.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address;
                }
            }
            return null;
        }
    }
}