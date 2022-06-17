﻿using ChatClientApp.Chat;
using Engine;
using System.Collections.Generic;

namespace ChatClientApp
{
    internal class Program
    {
        static string username = null;
        static string inputStr = "";
        static List<string> messages = new List<string>();
        static ChatClient chatClient = null;

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

            if (Input.KeyPressed(Key.RETURN))
            {
                username = inputStr;
                inputStr = "";

                chatClient = new ChatClient(username);
                chatClient.StartListen();

                ConsoleEx.Create(128, 32);
                ConsoleEx.SetFont("Consolas", 8, 16);
            }
        }

        static void ChatView()
        {
            foreach (string message in chatClient.Messages)
            {
                ConsoleEx.WriteLine(message);
            }

            Draw.Rectangle(0, (int)(ConsoleEx.Height / 1.5f), ConsoleEx.Width, ConsoleEx.Height, false, '.', 0);
            Draw.Line(0, (int)(ConsoleEx.Height / 1.5f), ConsoleEx.Width, (int)(ConsoleEx.Height / 1.5f), '═');

            if (Input.KeyPressed(Key.RETURN))
            {
                string message = username + ": " + inputStr;
                chatClient.SendMessage(message);
                inputStr = "";
            }

            inputStr = Input.Read(inputStr);
            ConsoleEx.SetPosition(1, (int)(ConsoleEx.Height / 1.5f) + 2);
            ConsoleEx.Write("Message: ");
            ConsoleEx.Write(inputStr);
            ConsoleEx.Write("|");
        }
    }
}
