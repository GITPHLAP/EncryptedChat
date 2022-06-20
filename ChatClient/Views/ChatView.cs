using ChatClientApp.Chat;
using ConsoleEngineCS.Core;
using System;

namespace ChatClientApp.Views
{
    internal class ChatView : View
    {
        private string username = "";
        private int chatScrollIndex = 0;
        private int chatHeight = 10;
        private string currentMessage = "";
        private ChatClient chatClient;

        public ChatView(ViewHandler viewHandler) : base(viewHandler)
        {
        }

        public override void Initialize()
        {
            ConsoleEx.Create(128, 32);
            ConsoleEx.SetFont("Consolas", 8, 16);

            username = ViewHandler.GetViewByKey<UsernameView>(ViewKey.Username).Username;
            chatClient = ViewHandler.GetViewByKey<ConnectingView>(ViewKey.Connecting).ChatClient;
            chatClient.MessageAdded += OnMessageAdded;
            chatHeight = ConsoleEx.Height - 5;
        }

        public override void Logic()
        {
            // Message input
            currentMessage = Input.Read(currentMessage);

            // Send message input
            if (Input.KeyPressed(Key.Enter))
            {
                string message = $"\ff<{DateTime.Now.ToLongTimeString()}> \fb[{username}]\ff: {currentMessage}";
                chatClient.SendMessage(message);
                currentMessage = "";
            }

            // Scroll Input
            if (Input.KeyStateDelayed(Key.Up, 75))
            {
                chatScrollIndex--;
            }
            if (Input.KeyStateDelayed(Key.Down, 75))
            {
                chatScrollIndex++;
            }

            // Limits scroll index to be in range
            if (chatScrollIndex < 0)
            {
                chatScrollIndex = 0;
            }
            if (chatScrollIndex > chatClient.Messages.Count)
            {
                chatScrollIndex = chatClient.Messages.Count;
            }
        }

        public override void Drawing()
        {

            // Draws chat messages
            for (int i = 0; i + chatScrollIndex < chatClient.Messages.Count; i++)
            {
                ConsoleEx.WriteLine(chatClient.Messages[i + chatScrollIndex]);
            }

            // Draws lower section seperation
            Draw.Color = CColor.Black;
            Draw.Rectangle(0, chatHeight, ConsoleEx.Width, ConsoleEx.Height, false, '.');
            Draw.Color = CColor.White;
            Draw.Line(0, chatHeight, ConsoleEx.Width, chatHeight, '═');

            // Draws input message
            ConsoleEx.SetPosition(1, chatHeight + 2);
            ConsoleEx.Write("\faMessage: \ff");
            ConsoleEx.Write(currentMessage);
            ConsoleEx.Write("|");

            // Draws key control hints
            ConsoleEx.SetPosition(0, ConsoleEx.Height - 1);
            ConsoleEx.Write("\feENTER: \ffSends message   \feARROW UP/DOWN: \ffScrolls chat");

            ConsoleEx.Update();
            ConsoleEx.Clear();
        }

        private void OnMessageAdded(object sender, EventArgs e)
        {
            // Makes the chat scroll when the messages exceeds the line limit
            if (chatClient.Messages.Count > chatHeight)
            {
                chatScrollIndex++;
            }
        }
    }
}
