using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientServer
{
    public class User
    {
        public Socket ClientSocket { get => ClientSocket; set => ClientSocket = value; }
        public string Name { get; set; }
        public string PublicKey { get; set; }

        public User(Socket clientSocket, string name, string publicKey)
        {
            ClientSocket = clientSocket;
            Name = name;
            PublicKey = publicKey;
        }

    }
}
