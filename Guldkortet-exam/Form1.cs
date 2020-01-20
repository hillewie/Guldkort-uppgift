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

        public Form1()
        {
            InitializeComponent();
            kortLoader = new FileLoader();
            client = new TcpClient();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (client.Connected)
            //    {
            //        NetworkStream tråden = client.GetStream();
            //        foreach ()
            //        {
            //            byte[] bytesToSend = Encoding.Unicode.GetBytes(Kort.ToString()); //  Programmet kan skicka markerade böckers ToString till Centraldator med tostring
            //            await tråden.WriteAsync(bytesToSend, 0, bytesToSend.Length); // meddelandar servern vad vi ska skicka och hur stort  
            //        }
            //    }
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message);
            //}
        }

        private async  void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!client.Connected)
                {
                    // Startar uppkoppling med servern och låser up button1, knappen blir grön
                    await client.ConnectAsync("127.0.0.1", 12345);
                    // byter färg och text
                    this.button2.BackColor = Color.Green;
                    this.button2.Text = "You are connected!";

                    button1.Enabled = true;
                }
                else
                {
                    // clienten är kopplad och kan avsluta uppkoppligen och låser button1, knappen byter färg till vit 
                    client.Close();
                    this.button2.BackColor = Color.White;
                    this.button2.Text = "Connect to Server";
                    button1.Enabled = false;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
   
}
