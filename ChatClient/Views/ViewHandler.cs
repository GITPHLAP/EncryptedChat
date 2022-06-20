using System.Collections.Generic;

namespace ChatClientApp.Views
{
    internal enum ViewKey
    {
        None,
        Username,
        Connecting,
        Chat,
    }

    internal class ViewHandler
    {
        private readonly Dictionary<ViewKey, View> views;

        public ViewHandler()
        {
            views = new()
            {
                { ViewKey.Username, new UsernameView(this) },
                { ViewKey.Connecting, new ConnectingView(this) },
                { ViewKey.Chat, new ChatView(this) },
            };
        }

        public ViewKey CurrentView { get; private set; } = ViewKey.None;

        public void ChangeView(ViewKey viewKey)
        {
            CurrentView = viewKey;
            views[CurrentView].Initialize();
        }

        public View GetCurrentView()
        {
            return views[CurrentView];
        }

        public T GetViewByKey<T>(ViewKey viewKey) where T : View
        {
            return (T)views[viewKey];
        }
    }
}
