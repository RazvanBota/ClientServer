using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ClientServer
{
    public class Message
    {
        private readonly string text;
        private readonly int count;
        private byte[] bytes;

        //Load message received on server
        public Message(Stream stream)
        {
            this.bytes = new byte[256];
            this.count = stream.Read(bytes, 0, bytes.Length);
            text = TranslateToString(bytes, count);
        }

        //Client close conection
        internal bool IsClosing() => text.Contains("CloseClientConection");

        //Translate message to string
        public string TranslateToString(byte[] bytesReceived, int count) => Encoding.ASCII.GetString(bytesReceived, 0, count);

        //Translate message to byte
        public byte[] TranslateToByte(string message) => Encoding.ASCII.GetBytes(message);

        //Send message inside the chat room
        internal void WriteTo(NetworkStream stream)
        {
            stream.Write(bytes, 0, count);
        }
    }
}
