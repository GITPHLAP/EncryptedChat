using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClientApp.Chat
{
    internal class ChatClient : GenericSocketClient
    {
        public ChatClient()
        {
            IPAddress = IPAddressUtillity.GetLocalIPAddress();
            Port = 81;
            MaxBufferSize = 1024;
        }

        public void SendMessage(string message)
        {
            SendRequest(message);
        }

        protected override void ClientInfo(string message)
        {
            Console.WriteLine($"<{DateTime.Now}> " + message);
        }

        protected override string InitialRequestData()
        {
            return "Clients initial request";
        }

        protected override void ServerResponse(string response)
        {
            ClientInfo(response);
        }
    }
}
