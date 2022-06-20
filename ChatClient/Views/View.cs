namespace ChatClientApp.Views
{
    internal abstract class View
    {
        protected ViewHandler ViewHandler { get; }

        public View(ViewHandler viewHandler)
        {
            ViewHandler = viewHandler;
        }

        public abstract void Initialize();
        public abstract void Logic();
        public abstract void Drawing();
    }
}
