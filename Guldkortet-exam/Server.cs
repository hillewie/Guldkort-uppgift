using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guldkortet_exam
{
	public class Server
	{
		public bool stop = false;
		public string IPNumber = "127.0.0.1"; //ip-nummer
		public int Port = 12345; //port

		private readonly FileLoader _fileLoader;
        private readonly RichTextBox _richTextBox;
		TcpListener server;
        public Server(FileLoader fileLoader, RichTextBox richTextBox)
		{
			_fileLoader = fileLoader;
            _richTextBox = richTextBox;
			server = new TcpListener(IPAddress.Parse(IPNumber), Port);
		}
		public void Stop() //stoppar uppkopplingen
		{
			server.Stop();
			stop = true;
		}
		public async Task StartServer() //ansluter till serven
		{
			StringBuilder reponse = new StringBuilder();

			server.Start(); // startar servern
			stop = false;
			while (!stop)  //inväntar anslutning
			{
				TcpClient client = await server.AcceptTcpClientAsync(); //finns det en anslutning kommer serven att acceptera anslutning
                NetworkStream tråden = client.GetStream();              // skickar och mottar meddelanden

				while (client.Connected && !stop)  //medans clinet är anslutning letar while efter inkommande meddelanden
				{
					byte[] meddelande = new byte[1024]; //meddelandet mottas som byte array
					await tråden.ReadAsync(meddelande, 0, meddelande.Length);
                    _richTextBox.Text = $"Client message {Encoding.Unicode.GetString(meddelande)}";

                    var message = Encoding.Unicode.GetString(meddelande).Split('-').AsEnumerable();// skriver ut meddelandet som en string
					
					var kund = _fileLoader.Kunder.FirstOrDefault(x=> x.kundNummer == message.ElementAtOrDefault(0)); // kund listan 

                    reponse.AppendLine((kund != null)?$"Kund med id {kund.kundNummer} hittad":"Tyvärr hittade vi ingen kund med det kundnumret!"); //skriver ut meddelande vid hittat eller inte hittad kund
					if(kund != null)
					{
						var kort = _fileLoader.kortLista.FirstOrDefault(x => x.kortNummer == message.ElementAtOrDefault(1)?.Replace("\0",string.Empty)); // kortlistan 
                        reponse.AppendLine((kort != null) ? $"Grattis du vann kortet {kort.kortTyp} Du kan hämta din belöning i din närmsta spelbutik" : "Tyvärr hittade inget belöningskort med det kortnumret!"); //skriver ut meddelande vid hittat eller inte hittad kort
                    }
                    var bytemeddelande = Encoding.Unicode.GetBytes(reponse.ToString());  //alla meddelanden konverteras till byte array

					await tråden.WriteAsync(bytemeddelande, 0, bytemeddelande.Length);

                    _richTextBox.Text = reponse.ToString();
                    reponse.Clear();
				}
			}
		}
	}
}
