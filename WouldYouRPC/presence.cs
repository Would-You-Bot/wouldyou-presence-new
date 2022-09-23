using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;


namespace WouldYouRPC
{
    public partial class presence : Form
    {
        public presence()
        {
            InitializeComponent();

            // Disable the user from resizing the box
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MinimizeBox = true;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			button2.Enabled = false;
			button1.Enabled = true;

		}

        public DiscordRpcClient client;

		void Initialize()
		{
			/*
			Create a Discord client
			NOTE: 	If you are using Unity3D, you must use the full constructor and define
					 the pipe connection.
			*/
			client = new DiscordRpcClient("1008861912919978026");

			//Set the logger
			client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

			//Subscribe to events
			client.OnReady += (sender, e) =>
			{
				Console.WriteLine("Received Ready from user {0}", e.User.Username);
			};

			client.OnPresenceUpdate += (sender, e) =>
			{
				Console.WriteLine("Received Update! {0}", e.Presence);
			};

			//Connect to the RPC
			client.Initialize();

			TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
			int secondsSinceEpoch = (int)t.TotalSeconds;

			//Set the rich presence
			//Call this as many times as you want and anywhere in your code.
			client.SetPresence(new RichPresence()
			{
				Details = "Invite Would You to your server!",
				State = "Increase your servers activity!",
				Timestamps = new Timestamps()
				{
					Start = DateTime.UtcNow
				},
				Assets = new Assets()
				{
					LargeImageKey = "logo",
					LargeImageText = "Invite Would You",
				},
				Buttons = new DiscordRPC.Button[]
				{
					new DiscordRPC.Button() { Label = "🔗 Invite", Url = "https://discord.com/oauth2/authorize?client_id=981649513427111957&permissions=274878294080&scope=bot%20applications.commands" },
					new DiscordRPC.Button() { Label = "⏫ Vote for me!", Url = "https://top.gg/bot/981649513427111957/vote" }
				}
			}) ;
		}



		private void button1_Click(object sender, EventArgs e)
        {
			button1.Enabled = false;
			button2.Enabled = true;
			Initialize();

        }

        private void button2_Click(object sender, EventArgs e)
        {
			button2.Enabled = false;
			button1.Enabled = true;
			client.Dispose();
		}

		private void OnApplicationQuit()
		{
			client.Dispose();
		}

	}
}
