using ChatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoLibrary;

namespace ChatClientServer
{
    internal class Server
    {
        bool running = false;
        Socket serverSocket;
        List<User> users = new List<User>();
        RSACryptoServiceProvider rsa = CryptoManager.GetKeyPair();

        public void Start(IPAddress address, int port)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket = socket;

            serverSocket.Bind(ipe);
            serverSocket.Listen(50);

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

                    Console.WriteLine("Client connected");

                    InitialConnection(clientSocket);
                }
                catch (Exception e)
                {
                    Console.WriteLine("SOMETHING WENT WRONG, " + e.Message);
                }

            }
        }

        /// <summary>
        /// Exchanges public keys with client and receives initial package from client, then starts listening for messages
        /// </summary>
        /// <param name="clientSocket"></param>
        private void InitialConnection(Socket clientSocket)
        {
            try
            {
                //On connection, send servers public key
                clientSocket.Send(Encoding.UTF8.GetBytes(rsa.ToXmlString(false)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("COULD NOT SEND PUBLIC KEY TO CLIENT: "+ ex.Message);
                return;
            }

            //Get the initial connection message from client
            string receivedJson = ReceiveFromSocket(clientSocket);

            ConnectionPackage pack = JsonConvert.DeserializeObject<ConnectionPackage>(receivedJson);

            User user = new User(clientSocket, pack.Name, pack.PublicKey);

            users.Add(user);

            //Create a thread for receiving messages from new the client
            Thread thread = new Thread(() => WaitForMessage(user));

            thread.Start();

            Console.WriteLine("started thread");
        }

        void WaitForMessage(User user)
        {
            //Run this while socket connected
            try
            {
                while (user.ClientSocket.Connected)
                {
                    string socketMessage = ReceiveDecryptedMessage(user.ClientSocket);

                    Console.WriteLine("Message received: " + socketMessage);


                    //Send the message to all clients connected excluding the sending client
                    foreach (var item in users.Where(x => x.ClientSocket.Connected == true && x != user))
                    {
                        //Encrypt the message with the user public key
                        string messageToSend = CryptoManager.EncryptString(item.Rsa,socketMessage);

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
            byte[] buffer = new byte[8192];
            //Wait for messsage from client
            int receivedBCount = client.Receive(buffer);

            //Convert to string
            return Encoding.UTF8.GetString(buffer, 0, receivedBCount);

        }

        string ReceiveDecryptedMessage(Socket client)
        {
            string socketMessage = ReceiveFromSocket(client);

            //Decrypt with servers private key
            return CryptoManager.DecryptString(rsa, socketMessage);

        }
    }
}
