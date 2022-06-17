using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClientApp
{
    internal class ChatClient : GenericSocketClient
    {
        public ChatClient()
        {
            IPAddress = IPAddressUtillity.GetLocalIPAddress();
            Port = 81;
            MaxBufferSize = 1024;
        }

        protected override void ClientInfo(string message)
        {
            Console.WriteLine($"<{DateTime.Now}> " + message);
        }

        protected override void SendInitialRequest(Socket clientSocket)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes("Clients initial request"));
        }

        protected override void SendRequest(string response, Socket clientSocket)
        {
            ClientInfo(response);
            Thread.Sleep(1000);
            clientSocket.Send(Encoding.UTF8.GetBytes("Some client data"));
        }
    }
}
