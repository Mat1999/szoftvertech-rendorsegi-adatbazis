using System;
using System.IO;

namespace RendorsegiAdatbazis
{
	public class felhasznalo
	{
		
		private string nev;
		private string igazolvanySzam;
		private DateTime szulDatum;
		private string lakhely;
		private string jelszo;
		public felhasznalo(string _nev,string _szemIgSz,DateTime _szuldate,string _lakhely,string _jelszo) {
			this.nev = _nev;
			this.igazolvanySzam = _szemIgSz;
			this.szulDatum = _szuldate;
			this.lakhely = _lakhely;
			this.jelszo = _jelszo;
		}
		public void bejelentkezes(string igazolvanyszam,string jelszo);
		
		public virtual void sajatAdatokMegtekintese() {
            Console.WriteLine("Név: "+nev+"\n"+
				"Igazolványszám: " + igazolvanySzam + "\n" +
				"Születési dátum: " + szulDatum + "\n" +
				"Lakhely: " + lakhely+ "\n" +
				"Jelszo: " + jelszo + "\n" +);
		}

	}
}