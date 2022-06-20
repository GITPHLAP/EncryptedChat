using ChatClientApp.Views;

namespace ChatClientApp
{
    internal class Program
    {
        static void Main()
        {
            ViewHandler viewHandler = new();
            viewHandler.ChangeView(ViewKey.Username);
            
            while(true)
            {
                View view = viewHandler.GetCurrentView();
                view.Logic();
                view.Drawing();
            }
        }
    }
}
