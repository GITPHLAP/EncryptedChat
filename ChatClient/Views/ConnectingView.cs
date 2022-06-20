using ChatClientApp.Chat;
using ConsoleEngineCS.Core;

namespace ChatClientApp.Views
{
    internal class ConnectingView : View
    {
        public ConnectingView(ViewHandler viewHandler) : base(viewHandler)
        {
        }

        public ChatClient ChatClient { get; private set; }

        public override void Initialize()
        {
            string username = ViewHandler.GetViewByKey<UsernameView>(ViewKey.Username).Username;
            ChatClient = new ChatClient(username);
            ChatClient.StartListen();

            ConsoleEx.Create(128, 32);
            ConsoleEx.SetFont("Consolas", 8, 16);
        }

        public override void Logic()
        {
            if (ChatClient.IsReady)
            {
                ViewHandler.ChangeView(ViewKey.Chat);
            }
        }

        public override void Drawing()
        {
            for (int i = 0; i < ChatClient.ClientInfoMessages.Count; i++)
            {
                ConsoleEx.WriteLine(ChatClient.ClientInfoMessages[i]);
            }

            ConsoleEx.SetPosition(ConsoleEx.Width / 3, ConsoleEx.Height / 2);
            ConsoleEx.Write("\faConnecting...");

            ConsoleEx.Update();
            ConsoleEx.Clear();
        }
    }
}
