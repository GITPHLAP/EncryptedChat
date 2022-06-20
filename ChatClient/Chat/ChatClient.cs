using ChatModels;
using CryptoLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatClientApp.Chat
{
    internal class ChatClient : GenericSocketClient
    {
        public event EventHandler MessageAdded;
        private readonly string name;
        private RSACryptoServiceProvider serverPublicKey = null;
        private RSACryptoServiceProvider clientPrivateKey = null;

        public ChatClient(string name)
        {
            //IPAddress = System.Net.IPAddress.Parse("80.71.140.165");
            IPAddress = IPAddressUtillity.GetLocalIPAddress();
            Port = 11753;
            MaxBufferSize = 1024;
            this.name = name;
        }

        public List<string> Messages { get; private set; } = new List<string>();

        public void SendMessage(string message)
        {
            SendRequest(CryptoManager.EncryptString(serverPublicKey, message));
            Messages.Add(message);
            MessageAdded?.Invoke(this, EventArgs.Empty);
        }

        protected override void ClientInfo(string message)
        {
            ////Console.WriteLine($"<{DateTime.Now}> " + message);
        }

        protected override string InitialRequestData()
        {
            clientPrivateKey = CryptoManager.GetKeyPair();
            ConnectionPackage connectionPackage = new()
            {
                Name = name,
                PublicKey = clientPrivateKey.ToXmlString(false),
            };
            return JsonConvert.SerializeObject(connectionPackage);
        }

        protected override void ServerResponse(string response)
        {
            Messages.Add(CryptoManager.DecryptString(clientPrivateKey, response));
            MessageAdded?.Invoke(this, EventArgs.Empty);
        }

        protected override void IntialServerResponse(string response)
        {
            serverPublicKey = CryptoManager.GetPublicKeyProvider(response);
        }
    }
}
