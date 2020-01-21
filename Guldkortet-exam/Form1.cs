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
        private readonly KortLoader kortLoader;
        private readonly TcpClient client;

        public Form1()
        {
            InitializeComponent();
            kortLoader = new KortLoader();
            client = new TcpClient();

            string Kund = "";
            string Kort = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.Connected)
                {
                    NetworkStream tråden = client.GetStream();
                    foreach ()
                    {
                        byte[] bytesToSend = Encoding.Unicode.GetBytes(Kort.ToString()); //  Programmet kan skicka markerade böckers ToString till Centraldator med tostring
                        await tråden.WriteAsync(bytesToSend, 0, bytesToSend.Length); // meddelandar servern vad vi ska skicka och hur stort  
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
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
    public class KortLoader
    {
        public List<Kort> kortLista = new List<Kort>();
        public List<Kund> kundLista = new List<Kund>();

        public KortLoader()
        {
            List<string> kundLista = new List<string>();
            if (File.Exists("kundlista.txt"))
            {
                StreamReader kundReader = new StreamReader("kundlista.txt", Encoding.Default, true);

                string kund = "";
                while ((kund = kundReader.ReadLine()) != null)
                {
                    kundLista.Add(kund);
                }
                foreach (string kund in kundLista)
                {
                    string[] kundVektor = kund.Split(new string[] { "###" }, StringSplitOptions.None);
                    kundLista.Add(new kund(kundVektor[0], kundVektor[1], kundVektor[2]));
                }
            }

            List<string> kortLista = new List<string>();
            if (File.Exists("kortlista.txt"))
            {
                StreamReader kortReader = new StreamReader("kortlista.txt", Encoding.Default, true);

                string kort = "";
                while ((kort = kortReader.ReadLine()) != null)
                {
                    kortLista.Add(kort);
                }

                foreach (string sparatkort in kortLista)
                {
                    string[] kortVektor = sparatKort.Split(new string[] { "###" }, StringSplitOptions.None);
                    string Dunderkatt = kortVektor[0];
                    string Kristallhäst = kortVektor[1];
                    string Överpanda = kortVektor[2];
                    string Eldtomat = kortVektor[3];

                    Kort kort;
                    switch (typ)
                    {
                        case "Dunderkatt":
                            kort = new Dunderkatt(kortVektor[0]);
                            break;

                        case "Kristallhäst":
                            kort = new Kristallhäst(kortVektor[1]);
                            break;

                        case "Överpanda":
                            kort = new Överpanda(kortVektor[2]);
                            break;

                        case "Eldtomat":
                            kort = new Eldtomat(kortVektor[3]);
                        default:
                    }
                    kortLista.Add(Kort);
                }
            }
        }
        public class Kort
        {
            public string kortNummer;
            public string kortTyp;

            public Kort(string inDatakortNummer, string inDatakortTyp)
            {
                kortNummer = inDatakortNummer;
                kortTyp = inDatakortTyp;
            }
        }
        class Dunderkatt : Kort
        {
            public Dunderkatt(string inDatakortNummer, string inDatakortTyp) :
            base(inDatakortNummer, inDatakortTyp)
            {
                inDatakortTyp = "Dunderkatt";
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        class Kristallhäst : Kort
        {
            public Kristallhäst(string inDatakortNummer, string inDatakortTyp) :
            base(inDatakortNummer, inDatakortTyp)
            {
                inDatakortTyp = "Kristallhäst";
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        class Överpanda : Kort
        {
            public Överpanda(string inDatakortNummer, string inDatakortTyp) :
            base(inDatakortNummer, inDatakortTyp)
            {
                inDatakortTyp = "Överpanda";
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        class Eldtomat : Kort
        {
            public Eldtomat(string inDatakortNummer, string inDatakortTyp) :
            base(inDatakortNummer, inDatakortTyp)
            {
                inDatakortTyp = "Eldtomat";
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }

        public class Kund
        {
            public string kundNummer;
            public string kundNamn;
            public string kundStad;

            public Kund(string inDatakundNummer, string inDatakundNamn, string inDatakundStad)
            {
                kundNummer = inDatakundNummer;
                kundNamn = inDatakundNamn;
                kundStad = inDatakundStad;
            }
            public override string ToString()
            {
                return " Grattis " + kundNamn + " Du har vunnit " + kortTyp + " Det finns att hämta i din lokala spelbutik i " + kundStad;
            }
        }
    }
}
