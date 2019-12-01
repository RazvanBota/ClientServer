using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Messenger
    {
        //send message to server
        public void Send(NetworkStream stream, string ussername, string message)
        {
            var data = Encoding.ASCII.GetBytes(ussername + ": " + message);
            stream.Write(data, 0, data.Length);
        }

        //accept the message from server
        public string Receive(NetworkStream stream)
        {
            var data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            return Encoding.ASCII.GetString(data, 0, bytes);
        }
    }
}
