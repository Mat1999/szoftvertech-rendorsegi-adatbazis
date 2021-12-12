using System;
using System.Collections.Generic;

namespace RendorsegiAdatbazis
{
	public class Sofor: felhasznalo
	{
		private int egyenleg;
		List<Birsag> birsagok;
		
		public Sofor(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo) : base(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo)
		{
			egyenleg = 0;
			birsagok = new List<Birsag>();
		}
		

		private void BirsagKifizetese(string birsagID) {
			int index = BirsagKeresese(birsagID);
			if (index >= 0){
				if (birsagok[index].GetBirsagOsszege() <= egyenleg){
					egyenleg -= birsagok[index].GetBirsagOsszege();
					birsagok[index].BirsagKifizetese();
				}
				else{
					Console.WriteLine("Az egyenlegen nincsen elég pénz. Töltse fel az egyenlegét!");
				}
			}
			else{
				Console.WriteLine("Nincs ilyen azonosítójú bírság.");
			}
		}
		private void penzFeltoltes(int osszeg) => egyenleg += osszeg;
		
		public void sajatBirsagokMegtekintese() {
			foreach (Birsag birsag in birsagok){
				birsag.BirsagAdatainakKiirasa();
			}
		}
		
		public int BirsagKeresese(string birID){
			for (int i = 0; i < birsagok.Count; i++){
				if (birsagok[i].GetBirsagID == birID){
					return i;
				}
			}
			return -1;
		}
		
		public void sajatAdatokMegtekintese(){
			base.sajatAdatokMegtekintese();
			Console.WriteLine("Egyenleg: {0} Ft", egyenleg);
		}
	
		//getter
		
	}
}