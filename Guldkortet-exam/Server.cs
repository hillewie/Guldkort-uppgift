using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Guldkortet_exam
{
    //sasddasd
	public class Server:INotifyPropertyChanged
	{
		public bool stop = false;
		string log ;
		public string IPNumeber = "127.0.0.1";
		public int Port = 12345;
		public string Log { get {
				
				return log; }
				set {

				
				log = value;
				PropertyChangeEvent("Log");
				
			} }
		private readonly FileLoader _fileLoader;
		TcpListener server;

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void PropertyChangeEvent(string property)
		{
			if(property != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}

		}

		public Server(FileLoader fileLoader)
		{
			_fileLoader = fileLoader;
			server = new TcpListener(IPAddress.Parse(IPNumeber), Port);
		}
		public void Stop()
		{
			server.Stop();
			stop = true;
		}
		public async Task StartServer()
		{
			StringBuilder reponse = new StringBuilder();

			
			// we set our IP address as server's address, and we also set the port: 9999

			server.Start();  // this will start the server
			stop = false;
			while (!stop)   //we wait for a connection
			{
				TcpClient client = await server.AcceptTcpClientAsync();  //if a connection exists, the server will accept it

				NetworkStream ns = client.GetStream();
				
				//networkstream is used to send/receive messages
				
				//sending the message

				while (client.Connected && !stop)  //while the client is connected, we look for incoming messages
				{
					byte[] msg = new byte[1024];     //the messages arrive as byte array
					await ns.ReadAsync(msg, 0, msg.Length);
					Log = $"Client message {Encoding.Unicode.GetString(msg)}";//the same networkstream reads the message sent by the client
					var message = Encoding.Unicode.GetString(msg).Split('-').AsEnumerable();//now , we write the message as string
					 
                    // kund listan 
					var kund = _fileLoader.Kunder.FirstOrDefault(x=> x.kundNummer == message.ElementAtOrDefault(0));

					reponse.AppendLine((kund != null)?$"Kund med id {kund.kundNummer} hittad":"Kunden fanns inte!");
					if(kund != null)
					{
                        // kund listan 
						var kort = _fileLoader.kortLista.FirstOrDefault(x => x.kortNummer == message.ElementAtOrDefault(1)?.Replace("\0",string.Empty));// här e helt
						reponse.AppendLine((kort != null) ? $"Grattis du fick korttyp  {kort.kortTyp} " : "vi hittade inga kort!");
					}
				
					   //any message must be serialized (converted to byte array)
					var bytemssg = Encoding.Unicode.GetBytes(reponse.ToString());  //conversion string => byte array

					await ns.WriteAsync(bytemssg, 0, bytemssg.Length);
					Log = reponse.ToString();
					
					reponse.Clear();
					// detta kan vara problemet client.Close();
				}
				
				
			}
		}
	}
}
