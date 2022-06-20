using ChatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatClientApp.Chat
{
    internal class ChatClient : GenericSocketClient
    {
        public event EventHandler MessageAdded;
        private readonly string name;

        public ChatClient(string name)
        {
            IPAddress = System.Net.IPAddress.Parse("80.71.140.165");
            Port = 11753;
            MaxBufferSize = 1024;
            this.name = name;
        }

        public List<string> Messages { get; private set; } = new List<string>();

        public void SendMessage(string message)
        {
            SendRequest(message);
            Messages.Add(message);
            MessageAdded?.Invoke(this, EventArgs.Empty);
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
            Messages.Add(response);
            MessageAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
