using System;
using System.IO;

namespace RendorsegiAdatbazis
{
	public class felhasznalo
	{
		
		protected string nev;
		protected string igazolvanySzam;
		protected DateTime szulDatum;
		protected string lakhely;
		protected string jelszo;
		
		public felhasznalo(string _nev,string _szemIgSz,DateTime _szuldate,string _lakhely,string _jelszo) {
			this.nev = _nev;
			this.igazolvanySzam = _szemIgSz;
			this.szulDatum = _szuldate;
			this.lakhely = _lakhely;
			this.jelszo = _jelszo;
		}
		
		public virtual void sajatAdatokMegtekintese() {
			Console.WriteLine("Név: "+nev);
			Console.WriteLine("Igazolványszám: " + igazolvanySzam);
			Console.WriteLine("Születési dátum: " + szulDatum);
			Console.WriteLine("Lakhely: " + lakhely);
			Console.WriteLine("Jelszo: " + jelszo);
		}

	}
}