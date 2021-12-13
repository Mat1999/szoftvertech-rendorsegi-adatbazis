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
						Program.BirsagokMentese();
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
		
		public void BirsagAdatainakModositasa(string birsagAzon, int felhasznaloIndex){
			int birsagIndex = -1;
			for (int i = 0; i < ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagokSzama(); i++){
				if (((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(i).GetBirsagID() == birsagAzon){
					birsagIndex = i;
				}
			}
			Console.WriteLine("Módosítható tulajdonságok");
			Console.WriteLine("(szemig, rogido, rendszam, kifizetett, sebesseg, osszeg, rogazon)");
			Console.Write("Milyen tulajdonságot szeretne módosítani: ");
			string parancs = Console.ReadLine();
			Console.Write("Adja meg az új értéket: ");
			string value = Console.ReadLine();
			Birsag ujBirsag;
			switch (parancs) {
				case "szemig":
					int ujFelIndex = Program.KeresFelhasznalo(value);
					if (ujFelIndex != -1){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetSzemelyigazolvanySzam(value);
						((Sofor)Program.felhasznalok[ujFelIndex]).BirsagRogzitese(ujBirsag);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).BirsagTorlese(birsagIndex);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nincs ilyen azonosítójú felhasználó!");
					}
					break;
				case "rogido":
					DateTime ujIdo;
					if (DateTime.TryParse(value, out ujIdo)){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetRogzitesIdopontja(ujIdo);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nem megfelelő formátumú érték!");
					}
					break;
				case "rendszam":
					ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
					ujBirsag.SetRendszam(value);
					((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
					Console.WriteLine("A művelet sikeres.");
					break;
				case "kifizetett":
					bool ujKifizetett;
					if (bool.TryParse(value, out ujKifizetett)){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetKifizetett(ujKifizetett);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nem megfelelő formátumú érték!");
					}
					break;
				case "sebesseg":
					double ujSebesseg;
					if (double.TryParse(value, out ujSebesseg)){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetMertSebesseg(ujSebesseg);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nem megfelelő formátumú érték!");
					}
					break;
				case "osszeg":
					int ujOsszeg;
					if (int.TryParse(value, out ujOsszeg)){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetBirsagOsszege(ujOsszeg);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nem megfelelő formátumú érték!");
					}
					break;
				case "rogazon":
					int ujRogAzon;
					if (int.TryParse(value, out ujRogAzon)){
						ujBirsag = ((Sofor)Program.felhasznalok[felhasznaloIndex]).GetBirsagAt(birsagIndex);
						ujBirsag.SetRogzitoAzonositoja(ujRogAzon);
						((Sofor)Program.felhasznalok[felhasznaloIndex]).SetBirsagAt(birsagIndex, ujBirsag);
						Console.WriteLine("A művelet sikeres.");
					}
					else{
						Console.WriteLine("Nem megfelelő formátumú érték!");
					}
					break;
				default:
					Console.WriteLine("Nincs ilyen tulajdonsága a bírságnak!");
					break;
			}
			Program.BirsagokMentese();
		}
		
		public void FelhasznaloAdatainakMegtekintese(string azon){
			int index = Program.KeresFelhasznalo(azon);
			if (index != -1){
				Program.felhasznalok[index].PublikusAdatokKilistazasa();
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		
		public void birsagTorlese(string birsagID)
		{
			for (int i = 0; i < Program.felhasznalok.Count; i++){
				if (Program.felhasznalok[i] is Sofor){
					int birsagIndex = ((Sofor)Program.felhasznalok[i]).BirsagKeresese(birsagID);
					if (birsagIndex != -1){
						((Sofor)Program.felhasznalok[i]).BirsagTorlese(birsagIndex);
						Program.BirsagokMentese();
						Console.WriteLine("A bírság törlése sikeres.");
						return;
					}
				}
			}
			Console.WriteLine("Nincs ilyen azonosítójú bírság!");
		}
		public void felhasznalokListazasa() {
			foreach (felhasznalo user in Program.felhasznalok) {
				user.PublikusAdatokKilistazasa();
			}
		}
		public void felhasznaloBirsagainakListazasa(string szemIgSz) {
			int index = Program.KeresFelhasznalo(szemIgSz);
			if (index != -1){
				List<Birsag> userBirsagai = ((Sofor)Program.felhasznalok[index]).GetBirsagok();
				foreach (Birsag buntetes in userBirsagai) {
					buntetes.BirsagAdatainakKiirasa(true);
				}
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		
		public override void PublikusAdatokKilistazasa(){
			base.PublikusAdatokKilistazasa();
			Console.WriteLine("A felhasználó operátor.");
		}
		
		public override void sajatAdatokMegtekintese(){
			base.sajatAdatokMegtekintese();
			Console.WriteLine("Operátorazonosító: " + operatorAzon);
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