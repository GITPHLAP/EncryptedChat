using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClientApp
{
    public abstract class GenericSocketClient
    {
        private readonly Socket clientSocket;
        private readonly Thread clientThread;

        public GenericSocketClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientThread = new Thread(ClientThreadEntry);
        }

        protected bool IsRunning { get; private set; } = false;
        protected int MaxBufferSize { get; set; } = 8192;
        protected IPAddress IPAddress { get; set; } = null;
        protected ushort Port { get; set; } = 1000;
        public bool IsReady = false;

        public void StartListen()
        {
            if (IPAddress == null)
            {
                ClientInfo("Failed to start server because no IPAddress was specified");
                return;
            }

            try
            {
                // Connects client socket to server
                clientSocket.Connect(new IPEndPoint(IPAddress, Port));
                IsRunning = true;

                // Start client socket thread
                clientThread.Start();
            }
            catch (Exception ex)
            {
                ClientInfo($"Failed to start listen on port {Port}: {ex.Message}");
            }
        }

        protected void SendRequest(string request)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(request));
        }

        private void ClientThreadEntry()
        {
            ClientInfo($"Client started listening on port {Port}");
            bool isFirstResponseAndRequest = true;
            while (IsRunning)
            {
                // Receives latest server response
                byte[] responseBuffer = new byte[MaxBufferSize];
                int responseSize = clientSocket.Receive(responseBuffer);

                if (!isFirstResponseAndRequest)
                {
                    // Calls server's response method
                    ServerResponse(Encoding.UTF8.GetString(responseBuffer, 0, responseSize));
                }
                else
                {
                    // Calls server's initial response method
                    IntialServerResponse(Encoding.UTF8.GetString(responseBuffer, 0, responseSize));

                    // Calls client's inital request method
                    string initialRequestData = InitialRequestData();
                    SendRequest(initialRequestData);

                    isFirstResponseAndRequest = false;
                    IsReady = true;
                }
            }
        }

        protected abstract string InitialRequestData();
        protected abstract void ClientInfo(string message);
        protected abstract void IntialServerResponse(string response);
        protected abstract void ServerResponse(string response);
    }
}
