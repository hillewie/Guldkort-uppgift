using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guldkortet_exam
{
	public class FileLoader
	{
        public List<Kund> Kunder = new List<Kund>(); //lista för sammanställd kundnummer filen
        public List<Kort> kortLista = new List<Kort>(); //lista för sammanställd kortnummer filen
        public FileLoader()
        {
            List<string> kort = new List<string>();
            if (File.Exists("kortlista.txt"))
            {
                StreamReader reader = new StreamReader("korlista.txt", Encoding.Default, false);

                string item = "";
                while ((item = reader.ReadLine()) != null)
                {
                    kort.Add(item);
                }
                foreach (string a in kort)
                {
                    string[] vektor = a.Split(new string[] { "###" }, StringSplitOptions.None); //splittar dem i olika kategorier i en vektorlista
                    string kortNummer = vektor[0];
                    string kortTyp = vektor[1];

                    Kort type;
                    switch (kortTyp)
                    {
                        case "Dunderkatt":
                            type = new Dunderkatt(kortNummer);
                            break;
                        case "Kristallhäst":
                            type = new Kristallhäst(kortNummer);
                            break;
                        case "Överpanda":
                            type = new Överpanda(kortNummer);
                            break;
                        default:
                            type = new Eldtomat(kortNummer);
                            break;
                    }
                    kortLista.Add(type); //lägger till varje kort i kortlistan
                }
            }
            List<string> kund = new List<string>();
            if (File.Exists("kundlista.txt"))
            {
                StreamReader reader = new StreamReader("kundlista.txt", Encoding.Default, false);

                string item = "";
                while ((item = reader.ReadLine()) != null)
                {
                    kund.Add(item);
                }
                foreach (string a in kund)
                {
                    string[] vektor = a.Split(new string[] { "###" }, StringSplitOptions.None); //splittar dem i olika kategorier i en vektorlista
                    string KundNummer = vektor[0];
                    string KundNamn = vektor[1];
                    string KundStad = vektor[2];
                }
            }
        }
        public void kortLoader()
        {

        }
        public class Kort //klass för kortnummer
		{
			public string kortNummer;
			public string kortTyp;

			public Kort(string inDatakortNummer, string inDatakortTyp)
			{
				this.kortNummer = inDatakortNummer;
				this.kortTyp = inDatakortTyp;
			}
		}
		class Dunderkatt : Kort //underklasser för belöningskorten
		{
			public Dunderkatt(string inDatakortNummer) : base(inDatakortNummer, "Dunderkatt"){
			}
			public override string ToString()
			{
				return base.ToString();
			}
		}
		class Kristallhäst : Kort
		{
			public Kristallhäst(string inDatakortNummer) : base(inDatakortNummer, "Kristallhäst"){
			}
			public override string ToString()
			{
				return base.ToString();
			}
		}
		class Överpanda : Kort
		{
			public Överpanda(string inDatakortNummer) : base(inDatakortNummer, "Överpanda"){
			}
			public override string ToString()
			{
				return base.ToString();
			}
		}
		class Eldtomat : Kort
		{
			public Eldtomat(string inDatakortNummer) : base(inDatakortNummer, "Eldtomat"){
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
				return " Grattis " + kundNamn + " Du har vunnit " + /*kortTyp*/  " Det finns att hämta i din lokala spelbutik i " + kundStad;
			}
		}
	}
}

