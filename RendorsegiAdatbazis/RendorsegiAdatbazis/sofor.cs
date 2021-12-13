using System;
using System.Collections.Generic;

namespace RendorsegiAdatbazis
{
	public class Sofor: felhasznalo
	{
		private int egyenleg;
		List<Birsag> birsagok;
		
		public Sofor(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo, int penz) : base(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo)
		{
			egyenleg = penz;
			birsagok = new List<Birsag>();
		}
		

		public void BirsagKifizetese(string birsagID) {
			int index = BirsagKeresese(birsagID);
			if (index >= 0){
				if (birsagok[index].GetBirsagOsszege() <= egyenleg){
					egyenleg -= birsagok[index].GetBirsagOsszege();
					birsagok[index].BirsagKifizetese();
					Console.WriteLine("A bírság kifizetése sikeres.");
					Program.BirsagokMentese();
					Program.FelhasznalokMentese();
				}
				else{
					Console.WriteLine("Az egyenlegen nincsen elég pénz. Töltse fel az egyenlegét!");
				}
			}
			else{
				Console.WriteLine("Nincs ilyen azonosítójú bírság.");
			}
		}
		public void penzFeltoltes(int osszeg){
			egyenleg += osszeg;
			Console.WriteLine("Pénzfeltöltés sikeres, új egyenleg: {0} Ft",egyenleg);
			Program.FelhasznalokMentese();
		}
		
		public void sajatBirsagokMegtekintese() {
			foreach (Birsag birsag in birsagok){
				birsag.BirsagAdatainakKiirasa(false);
			}
		}
		
		public int BirsagKeresese(string birID){
			for (int i = 0; i < birsagok.Count; i++){
				if (birsagok[i].GetBirsagID() == birID){
					return i;
				}
			}
			return -1;
		}
		
		public override void sajatAdatokMegtekintese(){
			base.sajatAdatokMegtekintese();
			Console.WriteLine("Egyenleg: {0} Ft", egyenleg);
		}
		
		
		
		public void BirsagRogzitese(Birsag ujBirsag){
			birsagok.Add(ujBirsag);
		}
		
		public void BirsagTorlese(int birsagIndex){
			birsagok.RemoveAt(birsagIndex);
		}
	
		//********************** GETTERS **************************
		#region Getters
		public int GetEgyenleg(){
			return egyenleg;
		}
		
		public int GetBirsagokSzama(){
			return birsagok.Count;
		}
		
		public Birsag GetBirsagAt(int index){
			return birsagok[index];
		}
		
		public List<Birsag> GetBirsagok(){
			return birsagok;
		}
		
		#endregion
		//********************** SETTERS **************************
		#region Setters
		public void SetEgyenleg(int _egyenleg){
			egyenleg = _egyenleg;
		}
		
		public void SetBirsagAt(int index, Birsag ujBirsag){
			birsagok[index] = ujBirsag;
		}
		
		#endregion
		
	}
}