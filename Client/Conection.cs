using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Conection
    {
        //closing connection with server
        public void Close(NetworkStream stream, TcpClient client, string ussername)
        {
            new Messenger().Send(stream, ussername, "CloseClientConection");
            Thread.Sleep(100);
            stream.Close();
            client.Close();
        }

        //When a new member join.
        public void Join(NetworkStream stream, TcpClient client, string ussername)
        {
            new Messenger().Send(stream, ussername, "JoinNewClient");
        }
    }
}
