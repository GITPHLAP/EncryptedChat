using ChatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClientServer
{
    internal class Server
    {
        bool running = false;
        Socket serverSocket;
        List<User> users = new List<User>();
        List<Thread> threads = new List<Thread>();

        public void Start(IPAddress address, int port)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket = socket;

            serverSocket.Bind(ipe);
            serverSocket.Listen(100);

            running = true;

            Listener();
        }

        void Listener()
        {
            while (running)
            {
                Socket clientSocket;
                try
                {
                    clientSocket = serverSocket.Accept();
                    //RequestHandler(clientSocket);

                    Console.WriteLine("Client connected");

                    //Get the connection message from client
                    string receivedJson = ReceiveFromSocket(clientSocket);


                    ConnectionPackage pack = JsonConvert.DeserializeObject<ConnectionPackage>(receivedJson);
                    User user = new User(clientSocket, pack.Name, pack.PublicKey);
                    //User user = new User(clientSocket, "Jens", "1234");

                    users.Add(user);

                    //Create a thread for receiving messages from new the client
                    Thread thread = new Thread(() => WaitForMessage(user));

                    threads.Add(thread);

                    thread.Start();

                    Console.WriteLine("started thread");
                }
                catch (Exception e)
                {
                    Console.WriteLine("SOMETHING WENT WRONG, " + e.Message);
                }

            }
        }
        void WaitForMessage(User user)
        {
            //Run this while socket connected
            try
            {
                while (user.ClientSocket.Connected)
                {
                    string socketMessage = ReceiveFromSocket(user.ClientSocket);
                    Console.WriteLine("Message received: " + socketMessage);

                    //Decrypt message
                    string messageToSend = $"{socketMessage}";

                    //Send the message to all clients connected
                    foreach (var item in users.Where(x => x.ClientSocket.Connected == true && x != user))
                    {
                        try
                        {
                            //TODO: Decrypt message with the users own public key
                            item.ClientSocket.Send(Encoding.UTF8.GetBytes(messageToSend));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Could not send message to client: " + ex.Message);
                            users.Remove(item);
                        }
                    }

                }
                Console.WriteLine($"Client {user.Name} disconnected");
            }
            catch (Exception e)
            {
                Console.WriteLine("Socket error: " + e.Message);
            }
            //Remove the user from the list when it is disconnected
            users.Remove(user);
        }
        string ReceiveFromSocket(Socket client)
        {
            byte[] buffer = new byte[256];
            //Wait for messsage from client
            int receivedBCount = client.Receive(buffer);

            //Convert to string
            return Encoding.UTF8.GetString(buffer, 0, receivedBCount);

        }

        void RequestHandler(Socket clientSocket)
        {
            try
            {
                Console.WriteLine("RECEIVING MESSAGE");
                byte[] buffer = new byte[256];
                int receivedBCount = clientSocket.Receive(buffer);
                string receivedStr = Encoding.UTF8.GetString(buffer, 0, receivedBCount);

                //Get the method by getting the part of the string where method is written
                string httpMethod = receivedStr.Substring(0, receivedStr.IndexOf(" "));
                Console.WriteLine("SENDING RESPONSE");
                switch (httpMethod)
                {
                    case "GET":
                        sendResponse(clientSocket, "hello", "200 OK", "text/html");
                        break;
                    case "POST":
                        //If someone posts they should get an error response
                        sendResponse(clientSocket, "Post", "418 I'm a teapot", "text/html");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPSS, ", e.Message);
            }
        }


        void sendResponse(Socket socket, string message, string responseCode, string contentType)
        {
            //Here we put the message into the html
            string htmlMessage = $"<html><head><meta http - equiv =\"Content-Type\" content=\"text/html; charset = utf-8\"></head><body><div>{message}</div></body></html>";


            //Create the header for http response
            byte[] header = Encoding.UTF8.GetBytes($"HTTP/1.1 " + responseCode + "\r\n"
                          + "Server: Kaspers big server\r\n"
                          + "Content-Length: " + htmlMessage.Length.ToString() + "\r\n"
                          + "Connection: close\r\n"
                          + "Content-Type: " + contentType + "\r\n\r\n");

            //Send the header
            socket.Send(header);
            //Send content
            socket.Send(Encoding.UTF8.GetBytes(htmlMessage));
        }
    }
}
