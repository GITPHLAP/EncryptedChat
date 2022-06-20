using ChatClientApp.Chat;
using ConsoleEngineCS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChatClientApp
{
    internal class Program
    {
        static string username = null;
        static string inputStr = "";
        static ChatClient chatClient = null;
        static int chatSrollIndex = 0;
        static int chatHeight = 0;

        [STAThread]
        static void Main()
        {
            ConsoleEx.Create(64, 16);
            ConsoleEx.SetFont("Consolas", 16, 32);

            while (true)
            {
                if (username == null)
                {
                    UsernameView();
                }
                else
                {
                    ChatView();
                }

                ConsoleEx.Update();
                ConsoleEx.Clear();
            }
        }

        static void UsernameView()
        {
            inputStr = Input.Read(inputStr);
            ConsoleEx.SetPosition(ConsoleEx.Width / 3, ConsoleEx.Height / 2);
            ConsoleEx.Write("Username: ");
            ConsoleEx.Write(inputStr);
            ConsoleEx.Write("|");

            if (Input.KeyPressed(Key.Enter))
            {
                username = inputStr;
                inputStr = "";

                chatClient = new ChatClient(username);
                chatClient.MessageAdded += OnMessageAdded;
                chatClient.StartListen();

                ConsoleEx.Create(128, 32);
                ConsoleEx.SetFont("Consolas", 8, 16);
            }
        }

        static void ChatView()
        {
            chatHeight = ConsoleEx.Height - 5;

            for (int i = 0; i + chatSrollIndex < chatClient.Messages.Count; i++)
            {
                ConsoleEx.WriteLine(chatClient.Messages[i + chatSrollIndex]);
            }

            Draw.Color = CColor.Black;
            Draw.Rectangle(0, chatHeight, ConsoleEx.Width, ConsoleEx.Height, false, '.');
            Draw.Color = CColor.White;
            Draw.Line(0, chatHeight, ConsoleEx.Width, chatHeight, '═');

            ConsoleEx.SetPosition(1, chatHeight + 2);
            ConsoleEx.Write("\faMessage: \ff");
            ConsoleEx.Write(inputStr);
            ConsoleEx.Write("|");

            ConsoleEx.SetPosition(0, ConsoleEx.Height - 1);
            ConsoleEx.Write("\feENTER: \ffSends message   \feARROW UP/DOWN: \ffScrolls chat   \feCTRL: \ffSend image");

            // Message input
            inputStr = Input.Read(inputStr);

            // Send message input
            if (Input.KeyPressed(Key.Enter))
            {
                string message = $"\ff<{DateTime.Now.ToLongTimeString()}> \fb[{username}]\ff: {inputStr}";
                chatClient.SendMessage(message);
                inputStr = "";
            }

            // Scroll Input
            if (Input.KeyStateDelayed(Key.Up, 75))
            {
                chatSrollIndex--;
            }
            if (Input.KeyStateDelayed(Key.Down, 75))
            {
                chatSrollIndex++;
            }
            if (chatSrollIndex < 0)
            {
                chatSrollIndex = 0;
            }
            if (chatSrollIndex > chatClient.Messages.Count)
            {
                chatSrollIndex = chatClient.Messages.Count;
            }
        }

        static void OnMessageAdded(object sender, EventArgs e)
        {
            if (chatClient.Messages.Count > chatHeight)
            {
                chatSrollIndex++;
            }
        }
    }
}
