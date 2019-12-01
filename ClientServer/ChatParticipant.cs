using System;
using System.Net.Sockets;

namespace ClientServer
{
    class ChatParticipant
    {
        private TcpClient TcpClient;
        public event Action<ChatParticipant> OnLeave;
        public event Action<Message> OnMessage;

        public ChatParticipant(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }

        //Show message in chat room
        public void ServeClient()
        {
            while (true)
            {
                var message = new Message(TcpClient.GetStream());
                OnMessage?.Invoke(message);
                if (message.IsClosing())
                    OnLeave?.Invoke(this); 
            }
        }

        //Send a new message from the client to server
        public void Send(Message message)
        {
            message.WriteTo(TcpClient.GetStream());
        }
    }
}
       