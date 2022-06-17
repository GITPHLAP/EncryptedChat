using ChatModels;
using System;
using System.Collections.Generic;

namespace ChatClientApp.Chat
{
    internal class ChatClient : GenericSocketClient
    {
        public ChatClient()
        {
            IPAddress = System.Net.IPAddress.Parse("80.71.140.165");
            Port = 11753;
            MaxBufferSize = 1024;
        }

        public Queue<string> Messages { get; private set; } = new Queue<string>();

        public void SendMessage(string message)
        {
            SendRequest(message);
            Messages.Enqueue(message);
        }

        protected override void ClientInfo(string message)
        {
            //Console.WriteLine($"<{DateTime.Now}> " + message);
        }

        protected override string InitialRequestData()
        {
            return "Clients initial request";
        }

        protected override void ServerResponse(string response)
        {
            Messages.Enqueue(response);
        }
    }
}
