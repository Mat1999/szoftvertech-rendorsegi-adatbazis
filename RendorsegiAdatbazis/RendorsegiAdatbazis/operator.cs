using System;

namespace RendorsegiAdatbazis
{
	public class Operator
	{
		private int operatorAzon;
		public Operator(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo,int _opAzon)
		{
			operatorAzon = _opAzon;
		}
		public void birsagFelvetele(int id, string helyszin, double sebesseg, string rendszam, DateTime rogzitesDatum, double fizetendoOsszeg, string fizetesStatusz, int RogzitoOperatorAzon) {
			//
		}
		public void osszedBirsagMegtekintese()
        {
			//birsag.kiirmindent();
        }
		public void birsagTorlese(int birsagID)
		{
			//birsag.torles(birsagID)
		}
		public void felhasznalokListazasa() {
			//gondolom a "program" lesz a rendszer ami tárolja a felhasználókat listában és onnan kéne kiirni
		}
		public void birsagokListazasa(string szemIgSz) {
			//birsag.keres(szemIgSz);
		}

	}
}