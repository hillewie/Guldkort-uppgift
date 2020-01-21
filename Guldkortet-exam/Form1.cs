using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guldkortet_exam
{
    public partial class Form1 : Form
    {
        private readonly FileLoader kortLoader;
        private readonly TcpClient client;
		private readonly Server server;
		
		public Form1()
        {
            InitializeComponent();
            kortLoader = new FileLoader();
            client = new TcpClient();
			server = new Server(kortLoader);
			richTextBox1.DataBindings.Add("Text", server, "Log", true, DataSourceUpdateMode.OnPropertyChanged);
			textBox1.Text = "A2986708-K242872563";
			button1.Enabled = false;
			button3.Enabled = false;
		}

        private void Form1_Load(object sender, EventArgs e)
        {

			
			
		}

        private async void Button1_Click(object sender, EventArgs e)
        {
			try
			{
				server.Stop();
				button2.Enabled = true;
				button1.Enabled = false;
				button3.Enabled = false;
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message);
			}
		}

        private async  void Button2_Click(object sender, EventArgs e)
        {
            try
            {
				if (!server.stop)
				{
					server.Log = "server is starting";
					button2.Enabled = false;
					button1.Enabled = true;
					button3.Enabled = true;
					await server.StartServer();

				}
            }
            catch (Exception error)
            {
                
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

		private async void button3_Click(object sender, EventArgs e)
		{
			button3.Enabled = false;
			var client = new TcpClient();
			

				await client.ConnectAsync(server.IPNumeber, server.Port);
			using (NetworkStream networkStream = client.GetStream())
			{
				networkStream.ReadTimeout = 2000;

				using (var writer = new StreamWriter(networkStream))
				{
					using (var reader = new StreamReader(networkStream, Encoding.Unicode))
					{

						byte[] bytes = Encoding.Unicode.GetBytes(textBox1.Text);
						await networkStream.WriteAsync(bytes, 0, bytes.Length);

						MessageBox.Show(await reader.ReadToEndAsync());
					}
				}
			}
			button3.Enabled = true;
		}

		private async void button4_Click(object sender, EventArgs e)
		{
			if (client.Connected)
			{
				
			}
		}
	}
   
}
