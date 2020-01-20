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
		//ändring 
		public List<Kort> kortLista = new List<Kort>();
		public List<Kund> Kunder = new List<Kund>();

		private IEnumerable<IEnumerable<string>> LoadFile(string path)
		{
			return (File.Exists(path))? System.IO.File.ReadAllText(path)
					.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
					.Select(x => x.Split(new string[] { "###" }, StringSplitOptions.None))
					.Select(x => x.Where(y => !string.IsNullOrEmpty(y))) 
					: new List<IEnumerable<string>>();
		}
		public Kort LoadCardType(string type, string value)
		{
			switch (type)
			{
				case "Dunderkatt":
					return new Dunderkatt(value, type);
				case "Kristallhäst":
					return new Kristallhäst(value, type);
				case "Överpanda":
					return new Överpanda(value, type);
				case "Eldtomat":
					return new Eldtomat(value, type);
				default:
					return new Kort(value, type);
			}
		}
		public void ResetLists()
		{
			if (Kunder.Count > 0)
				Kunder.Clear();

			if (kortLista.Count > 0)
				kortLista.Clear();
		}
		public void LoadFiles()
		{
			foreach (var kund in LoadFile("kundlista.txt"))
				Kunder.Add(new Kund(kund.ElementAtOrDefault(0), kund.ElementAtOrDefault(1), kund.ElementAtOrDefault(2)));
			foreach (var kort in LoadFile("korlista.txt"))
				kortLista.Add(LoadCardType(kort.ElementAtOrDefault(1), kort.ElementAtOrDefault(0)));
		}
		public FileLoader()
		{
			LoadFiles();
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
				return " Grattis " + kundNamn + " Du har vunnit " + /*kortTyp*/  " Det finns att hämta i din lokala spelbutik i " + kundStad;
			}
		}
	}
}

