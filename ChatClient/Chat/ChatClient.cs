using ChatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatClientApp.Chat
{
    internal class ChatClient : GenericSocketClient
    {
        private readonly string name;

        public ChatClient(string name)
        {
            IPAddress = System.Net.IPAddress.Parse("80.71.140.165");
            Port = 11753;
            MaxBufferSize = 1024;
            this.name = name;
        }

        public Queue<string> Messages { get; private set; } = new Queue<string>();

        public void SendMessage(string message)
        {
            SendRequest(message);
            Messages.Enqueue(message);
        }

        protected override void ClientInfo(string message)
        {
            ////Console.WriteLine($"<{DateTime.Now}> " + message);
        }

        protected override string InitialRequestData()
        {
            ConnectionPackage connectionPackage = new()
            {
                Name = name,
                PublicKey = "xyz"
            };
            return JsonConvert.SerializeObject(connectionPackage);
        }

        protected override void ServerResponse(string response)
        {
            Messages.Enqueue(response);
        }
    }
}
