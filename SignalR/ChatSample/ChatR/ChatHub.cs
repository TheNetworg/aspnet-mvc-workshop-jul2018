using Microsoft.AspNet.SignalR;

namespace ChatR
{
    public class ChatHub:Hub
    {
        //TODO
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}