using Engine;
using System;
using System.Collections.Generic;

namespace ChatClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> messages = new List<string>();

            ConsoleEx.Create(128, 32);
            ConsoleEx.SetFont("Consolas", 8, 16);

            string inputStr = "";
            while (true)
            {
                foreach(string message in messages)
                {
                    ConsoleEx.WriteLine(message);
                }

                Draw.Rectangle(0, (int)(ConsoleEx.Height / 1.5f), ConsoleEx.Width, ConsoleEx.Height, false, '.', 0);
                Draw.Line(0, (int)(ConsoleEx.Height / 1.5f), ConsoleEx.Width, (int)(ConsoleEx.Height / 1.5f), '═');
                
                if (Input.KeyPressed(Key.RETURN))
                {
                    messages.Add("User: " + inputStr);
                    inputStr = "";
                }

                inputStr = Input.Read(inputStr);
                ConsoleEx.SetPosition(1, (int)(ConsoleEx.Height / 1.5f) + 2);
                ConsoleEx.Write("Message: ");
                ConsoleEx.Write(inputStr);
                ConsoleEx.Write("|");

                ConsoleEx.Update();
            }
        }
    }
}
