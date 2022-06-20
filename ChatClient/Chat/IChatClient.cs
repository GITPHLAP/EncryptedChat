using System;
using System.Collections.Generic;

namespace ChatClientApp.Chat
{
    internal interface IChatClient
    {
        List<string> Messages { get; }

        event EventHandler MessageAdded;

        void SendMessage(string message);
    }
}