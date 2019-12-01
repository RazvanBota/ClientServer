using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        Thread receiveMessageThread = null;

        public Client()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            //Validation to send the message
            if (textBox2.Text == "")
            {
                MessageBox.Show("The text can't be an empty string");
                return;
            }

            //Send message if is valid
            try
            {
                new Messenger().Send(stream, textBox3.Text, textBox2.Text);
            }
            catch (ArgumentNullException a)
            {
                MessageBox.Show("ArgumentNullException: " + a.ToString());
            }
            catch (SocketException a)
            {
                MessageBox.Show("SocketException: " + a.ToString());
            }

        }

        //Close client conection and window
        private void Button2_Click(object sender, EventArgs e)
        {
            receiveMessageThread.Abort();
            new Conection().Close(stream, client, textBox3.Text);
            this.Close();
        }

        //Start a client conection
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals("Chose a ussername."))
            {
                MessageBox.Show("You need to chose a ussername.");
                return;
            }

            try
            {
                client = new TcpClient("localhost", 13);
                stream = client.GetStream();
                new Conection().Join(stream, client, textBox3.Text);

                //Start listening thread
                if (Equals(receiveMessageThread, null) || !receiveMessageThread.IsAlive)
                {
                    receiveMessageThread = new Thread(() =>
                    {
                        bool threadWorking = true;
                        while (threadWorking)
                        {
                            //Encode received message
                            var receiveData = new Messenger().Receive(stream);

                            //Show received message
                            Invoke((MethodInvoker)delegate
                            {
                                if (receiveData.Contains("CloseClientConection"))
                                    receiveData = receiveData.Replace(": CloseClientConection", " has quit.");
                                if (receiveData.Contains("JoinNewClient"))
                                    receiveData = receiveData.Replace(": JoinNewClient", " has join to the chat room.");
                                textBox1.AppendText(receiveData + Environment.NewLine);
                            });
                        }
                    });
                    receiveMessageThread.Start();
                }

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = false;

            }
            catch (SocketException)
            {
                MessageBox.Show("The server is offline.");
            }

        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());
        }
    }
}
