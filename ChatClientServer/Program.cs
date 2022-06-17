using ChatModels;
using System;
using System.Net;

namespace ChatClientServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Server server = new Server();
            server.Start(IPAddressUtillity.GetLocalIPAddress(),8080);
        }
    }
}
