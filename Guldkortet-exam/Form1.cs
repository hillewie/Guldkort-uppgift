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
        private readonly FileLoader FileLoader; //kort och kundklass samt kundlista och kortlista
        private readonly TcpClient client; // den här behövs ej  bara för test 
        private readonly Server server; //Här är servern
		public Form1()
        {
            InitializeComponent();
            FileLoader = new FileLoader();
            client = new TcpClient();
			server = Server(kortLoader, richTextBox1);
			button1.Enabled = false;
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
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message); //felmeddelande
			}
		}
        private async  void Button2_Click(object sender, EventArgs e)
        {
            try
            {
				if (!server.stop)
				{
					button2.Enabled = false;
					button1.Enabled = true;
					await server.StartServer();

				}
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message); //felmeddelande
            }
        }
    }
}
