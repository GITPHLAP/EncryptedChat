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
            server.Start(IPAddress.Parse("192.168.1.107"),8080);
        }
    }
}
