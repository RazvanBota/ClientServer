using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ClientServer
{
    public class ChatServer
    {
        private Thread ListenerThread, clientThread;
        private TcpListener TcpListener = null;
        private readonly ChatRoom ChatRoom = new ChatRoom();

        //Start listening on the ip
        public void Listen()
        {
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];

            try
            {
                TcpListener = new TcpListener(ipAddress, 13);
                TcpListener.Start();
            }
            catch (Exception a)
            {
                MessageBox.Show("Error: " + a.ToString());
            }

            ListenerThread = new Thread(() =>
            {
                while(true)
                {
                    TcpClient tcpClient = null;

                    try
                    { 
                        tcpClient = TcpListener.AcceptTcpClient();
                    }
                    catch (SocketException) 
                    { 
                        break; 
                    };

                    clientThread = new Thread(() =>
                    {
                        ChatParticipant newClient = new ChatParticipant(tcpClient);
                        ChatRoom.Join(newClient);
                        newClient.ServeClient();
                    });

                    clientThread.Start();
                }
            });

            ListenerThread.Start();
        }

        //Close the server
        public void StopServer()
        {
            try
            {
                TcpListener.Stop();
            }
            catch (NullReferenceException) { };
        }
    }
}
