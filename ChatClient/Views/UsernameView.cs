using ConsoleEngineCS.Core;

namespace ChatClientApp.Views
{
    internal class UsernameView : View
    {
        public UsernameView(ViewHandler viewHandler) : base(viewHandler)
        {
        }

        public string Username { get; private set; } = "";

        public override void Initialize()
        {
            ConsoleEx.Create(64, 16);
            ConsoleEx.SetFont("Consolas", 16, 32);
        }

        public override void Logic()
        {
            Username = Input.Read(Username);

            if (Input.KeyPressed(Key.Enter))
            {
                ViewHandler.ChangeView(ViewKey.Connecting);
            }
        }

        public override void Drawing()
        {
            ConsoleEx.SetPosition(ConsoleEx.Width / 3, ConsoleEx.Height / 2);
            ConsoleEx.Write("Username: ");
            ConsoleEx.Write(Username);
            ConsoleEx.Write("|");

            ConsoleEx.Update();
            ConsoleEx.Clear();
        }
    }
}
