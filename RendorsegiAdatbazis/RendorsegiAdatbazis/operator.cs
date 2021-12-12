using System;

namespace RendorsegiAdatbazis
{
	public class Operator : felhasznalo
	{
		private int operatorAzon;
		
		public Operator(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo,int _opAzon) : base(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo)
		{
			operatorAzon = _opAzon;
		}
		public void birsagFelvetele(string ID, string rogAzon, DateTime rogIdo, string igaSzam, string rendsz, double sebesseg, int birsag, int RogzitoOperatorAzon) {
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