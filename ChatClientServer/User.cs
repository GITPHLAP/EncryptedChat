using CryptoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientServer
{
    public class User
    {
        public Socket ClientSocket { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Only used for encrypting users messages, wont work with decrypting
        /// </summary>
        public RSACryptoServiceProvider Rsa { get; set; }

        public User(Socket clientSocket, string name, string publicKey)
        {
            ClientSocket = clientSocket;
            Name = name;
            Rsa = CryptoManager.GetPublicKeyProvider(publicKey);
        }

    }
}
