using System;
using System.Collections.Generic;

namespace RendorsegiAdatbazis
{
	public class Operator : felhasznalo
	{
		private int operatorAzon;
		
		public Operator(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo,int _opAzon) : base(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo)
		{
			operatorAzon = _opAzon;
		}
		
		public void birsagFelvetele() {
			Console.Write("Adja meg a bírságolni kívánt felhasználó igazolványszámát: ");
			string igaSzam = Console.ReadLine();
			int index = Program.KeresFelhasznalo(igaSzam);
			if (index != -1){
				if (Program.felhasznalok[index] is Sofor){
					Console.Write("Adja meg a bírság azonosítóját: ");
					string birsagAzon = Console.ReadLine();
					if (!Program.BirsagLetezik(birsagAzon)){
						Console.Write("Adja meg szabályszegés időpontját számokban (év-hónap-nap): ");
						string rogIdo = Console.ReadLine();
						rogIdo = rogIdo.Trim();
						Console.Write("Adja meg a szabályszegő kocsi rendszámát: ");
						string rendSz = Console.ReadLine();
						rendSz = rendSz.Trim();
						Console.Write("Adja meg a mért sebességet (km/h): ");
						string mertSeb = Console.ReadLine();
						mertSeb = mertSeb.Trim();
						Console.Write("Adja meg a büntetés összegét (Ft): ");
						string birOssz = Console.ReadLine();
						birOssz = birOssz.Trim();
						DateTime cRogIdo;
						if (!DateTime.TryParse(rogIdo, out cRogIdo)){
							Console.WriteLine("A rögzítés idejét nem megfelelő formátumban adta meg! Új bírság létrehozása nem sikeres.");
							return;
						}
						int cBirOssz;
						if (!int.TryParse(birOssz, out cBirOssz)){
							Console.WriteLine("A bírság összegét nem megfelelő formátumban adta meg! Új bírság létrehozása nem sikeres.");
							return;
						}
						double cMertSeb;
						if (!double.TryParse(mertSeb, out cMertSeb)){
							Console.WriteLine("A mért sebességet nem megfelelő formátumban adta meg! Új bírság létrehozása nem sikeres.");
							return;
						}
						((Sofor)Program.felhasznalok[index]).BirsagRogzitese(new Birsag(birsagAzon,operatorAzon,cRogIdo,Program.felhasznalok[index].GetIgazolvanySzam(),rendSz, cMertSeb, cBirOssz));
					}
					else{
						Console.WriteLine("Ilyen azonosítójú bírság már létezik.");
					}
				}
				else{
					Console.WriteLine("A felhasználó nem egy sofőr. Bírság hozzáadása nem lehetséges.");
				}
			}
			else{
				Console.WriteLine("Ilyen igazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		
		public void osszesBirsagMegtekintese()
        {
			foreach (felhasznalo user in Program.felhasznalok) {
				if (user is Sofor){
					Console.WriteLine("Sofőr adatai");
					((Sofor)user).PublikusAdatokKilistazasa();
					List<Birsag> userBirsagai = ((Sofor)user).GetBirsagok();
					foreach(Birsag buntetes in userBirsagai){
						buntetes.BirsagAdatainakKiirasa(true);
					}
				}
			}
        }
		public void birsagTorlese(string birsagID)
		{
			for (int i = 0; i < Program.felhasznalok.Count; i++){
				if (Program.felhasznalok[i] is Sofor){
					int birsagIndex = ((Sofor)Program.felhasznalok[i]).BirsagKeresese(birsagID);
					if (birsagIndex != -1){
						((Sofor)Program.felhasznalok[i]).BirsagTorlese(birsagIndex);
					}
				}
			}
		}
		public void felhasznalokListazasa() {
			foreach (felhasznalo user in Program.felhasznalok) {
				user.PublikusAdatokKilistazasa();
			}
		}
		public void felhasznaloBirsagainakListazasa(string szemIgSz) {
			int index = Program.KeresFelhasznalo(szemIgSz);
			if (index != -1){
				
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		
		//********************** GETTERS **************************
		#region Getters
		public int GetOperatorAzon(){
			return operatorAzon;
		}
		
		#endregion
		//********************** GETTERS **************************
		#region Setters
		public void SetOperatorAzon(int _opAzon){
			operatorAzon = _opAzon;
		}
		
		#endregion

	}
}