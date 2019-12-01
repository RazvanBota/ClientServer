using System;
using System.Windows.Forms;

namespace ClientServer
{
    class Server : Form
    {
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private ChatServer chatServer = new ChatServer();

        public Server()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            chatServer.Listen();
            button1.Enabled = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            chatServer.StopServer();
            this.Close();
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.textBox1 = new TextBox();
            this.button2 = new Button();
            this.SuspendLayout();

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 78);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.Button1_Click);

            // 
            // textBox1
            // 
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(12, 96);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(223, 121);
            this.textBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 78);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.Button2_Click);
            // 
            // Server
            // 
            this.ClientSize = new System.Drawing.Size(250, 229);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
