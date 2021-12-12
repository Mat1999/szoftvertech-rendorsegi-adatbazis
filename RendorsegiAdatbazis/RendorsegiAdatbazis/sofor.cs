using System;

namespace RendorsegiAdatbazis
{
	public class Sofor: felhasznalo
	{
		private double egyenleg;
		public Sofor(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo)
		{
		//ezt nemtudom itt hogykell :(
			this.felhasznalo(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo);
		egyenleg = 0.0;
		}

		private void birsag kifizetese(int birsagID) {
			//Birsag.BirsagKifizetese(birsagID);
		}
		private void penzFeltoltes(int osszeg) => egyenleg += osszeg;
		public void sajatBirsagokMegtekintese(string szemIgSzam) {
			//Birsag.Keres(szemIgSzam); 
		}
	public void sajatAdatokMegtekintese()
	{
		Console.WriteLine("Név: " + nev + "\n" +
			"Igazolványszám: " + igazolvanySzam + "\n" +
			"Születési dátum: " + szulDatum + "\n" +
			"Lakhely: " + lakhely + "\n" +
			"Jelszo: " + jelszo + "\n" +);
			
	}
	//getter
}
}