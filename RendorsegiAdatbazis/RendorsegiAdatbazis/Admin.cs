using System;
namespace RendorsegiAdatbazis
{
	public class Admin : felhasznalo
	{
		private int adminAzon;
		public Admin(string _nev, string _szemIgSz, DateTime _szuldate, string _lakhely, string _jelszo,int _adminAzon) : base(_nev, _szemIgSz, _szuldate, _lakhely, _jelszo)
		{
			adminAzon = _adminAzon;
		}
		public void FelhasznaloTorlese(string SzemIgSzam) { 
			int index = Program.KeresFelhasznalo(SzemIgSzam);
			if (index != -1){
				Program.felhasznalok.RemoveAt(index);
				Program.FelhasznalokMentese();
				Console.WriteLine("Felhasználó törlése sikeres.");
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelekező felhasználó nem létezik! Törlés sikertelen.");
			}
		}
		
		public void FelhasznaloModositasa(string userAzon){
			int index = Program.KeresFelhasznalo(userAzon);
			if (index != -1){
				if (Program.felhasznalok[index] is Sofor){
					Console.WriteLine("Módosítható tulajdonságok");
					Console.WriteLine("(nev, szemelyazon, szuldatum, lakhely, jelszo, egyenleg)");
					Console.Write("Milyen tulajdonságot szeretne módosítani: ");
					string parancs = Console.ReadLine();
					Console.Write("Adja meg az új értéket: ");
					string value = Console.ReadLine();
					switch (parancs){
						case "nev":
							Program.felhasznalok[index].SetNev(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "szemelyazon":
							int letezikindex = Program.KeresFelhasznalo(value);
							if (letezikindex == -1){
								((Sofor)Program.felhasznalok[index]).SetIgazolvanySzam(value);
								for (int i = 0; i < ((Sofor)Program.felhasznalok[index]).GetBirsagokSzama(); i++){
									Birsag ujBirsag = ((Sofor)Program.felhasznalok[index]).GetBirsagAt(i);
									ujBirsag.SetSzemelyigazolvanySzam(value);
									((Sofor)Program.felhasznalok[index]).SetBirsagAt(i,ujBirsag);
								}
								Program.BirsagokMentese();
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Ilyen személyigazolványszámmal már létezik felhasználó.");
							}
							break;
						case "szuldatum":
							DateTime ujIdo;
							if (DateTime.TryParse(value, out ujIdo)){
								Program.felhasznalok[index].SetSzulDatum(ujIdo);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
						case "lakhely":
							Program.felhasznalok[index].SetLakhely(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "jelszo":
							Program.felhasznalok[index].SetJelszo(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "egyenleg":
							int ujEgyenleg;
							if (int.TryParse(value, out ujEgyenleg)){
								((Sofor)Program.felhasznalok[index]).SetEgyenleg(ujEgyenleg);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
					}
					
				}
				else if (Program.felhasznalok[index] is Operator){
					Console.WriteLine("Módosítható tulajdonságok");
					Console.WriteLine("(nev, szemelyazon, szuldatum, lakhely, jelszo, opazon)");
					Console.Write("Milyen tulajdonságot szeretne módosítani: ");
					string parancs = Console.ReadLine();
					Console.Write("Adja meg az új értéket: ");
					string value = Console.ReadLine();
					switch (parancs){
						case "nev":
							Program.felhasznalok[index].SetNev(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "szemelyazon":
							int letezikindex = Program.KeresFelhasznalo(value);
							if (letezikindex == -1){
								Program.felhasznalok[index].SetIgazolvanySzam(value);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Ilyen személyigazolványszámmal már létezik felhasználó.");
							}
							break;
						case "szuldatum":
							DateTime ujIdo;
							if (DateTime.TryParse(value, out ujIdo)){
								Program.felhasznalok[index].SetSzulDatum(ujIdo);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
						case "lakhely":
							Program.felhasznalok[index].SetLakhely(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "jelszo":
							Program.felhasznalok[index].SetJelszo(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "opazon":
							int ujAzon;
							if (int.TryParse(value, out ujAzon)){
								bool letezik = false;
								foreach (felhasznalo user in Program.felhasznalok) {
									if (user is Operator){
										if (((int)(((Operator)Program.felhasznalok[index]).GetOperatorAzon())) == ujAzon){
											letezik = true;
										}
									}
								}
								if (!letezik){
									((Operator)Program.felhasznalok[index]).SetOperatorAzon(ujAzon);
									Console.WriteLine("A művelet sikeres volt.");
								}
								else{
									Console.WriteLine("Ilyen azonosítójú operátor már létezik!");
								}
								
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
					}
				}
				else if (Program.felhasznalok[index] is Admin){
					Console.WriteLine("Módosítható tulajdonságok");
					Console.WriteLine("(nev, szemelyazon, szuldatum, lakhely, jelszo, adminazon)");
					Console.Write("Milyen tulajdonságot szeretne módosítani: ");
					string parancs = Console.ReadLine();
					Console.Write("Adja meg az új értéket: ");
					string value = Console.ReadLine();
					switch (parancs){
						case "nev":
							Program.felhasznalok[index].SetNev(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "szemelyazon":
							int letezikindex = Program.KeresFelhasznalo(value);
							if (letezikindex == -1){
								Program.felhasznalok[index].SetIgazolvanySzam(value);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Ilyen személyigazolványszámmal már létezik felhasználó.");
							}
							break;
						case "szuldatum":
							DateTime ujIdo;
							if (DateTime.TryParse(value, out ujIdo)){
								Program.felhasznalok[index].SetSzulDatum(ujIdo);
								Console.WriteLine("A művelet sikeres volt.");
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
						case "lakhely":
							Program.felhasznalok[index].SetLakhely(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "jelszo":
							Program.felhasznalok[index].SetJelszo(value);
							Console.WriteLine("A művelet sikeres volt.");
							break;
						case "adminazon":
							int ujAzon;
							if (int.TryParse(value, out ujAzon)){
								bool letezik = false;
								foreach (felhasznalo user in Program.felhasznalok) {
									if (user is Admin){
										if (((int)(((Admin)Program.felhasznalok[index]).GetAdminAzon())) == ujAzon){
											letezik = true;
										}
									}
								}
								if (!letezik){
									((Admin)Program.felhasznalok[index]).SetAdminAzon(ujAzon);
									Console.WriteLine("A művelet sikeres volt.");
								}
								else{
									Console.WriteLine("Ilyen azonosítójú adminisztrátor már létezik!");
								}
								
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú érték!");
							}
							break;
					}
				}
				Program.FelhasznalokMentese();
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		
		public void OsszesFelhasznaloKilistazasa(){
			foreach (felhasznalo user in Program.felhasznalok) {
				Console.WriteLine("--------------------------------------");
				user.sajatAdatokMegtekintese();
			}
		}
		
		public void FelhasznaloAdatainakMegtekintese(string azon){
			int index = Program.KeresFelhasznalo(azon);
			if (index != -1){
				Program.felhasznalok[index].sajatAdatokMegtekintese();
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó nem létezik.");
			}
		}
		/*
		private void felhasznaloModositas(int szemigsz) {
            Console.WriteLine("Kérem irja be a felhasználó személy igazolvány számát akit módositani szeretne");
		
			Console.WriteLine("Kérem írjon be egy számot hogy milyen adatot szeretne módositani: ");
			
		}*/
		
		public void UjFelhasznaloLetrehozasa(string tipus){
			string[] elfogadottTipusok = {"sofor", "operator", "admin"};
			bool elfogadott = false;
			for (int i = 0; i < elfogadottTipusok.Length; i++){
				if (elfogadottTipusok[i] == tipus){
					elfogadott = true;
				}
			}
			if (!elfogadott){
				Console.WriteLine("Ilyen típusú felhasználót nem lehet létrehozni.");
				return;
			}
			Console.Write("Adja meg a felhasználó személyigazolványszámát: ");
			string igaSzam = Console.ReadLine();
			igaSzam = igaSzam.Trim();
			int index = Program.KeresFelhasznalo(igaSzam);
			if (index != -1){
				Console.WriteLine("Ilyen személyigazolványszámmal rendelkező felhasználó már létezik! Felhasználó létrehozása sikertelen.");
				return;
			}
			Console.Write("Adja meg az új felhasználó nevét: ");
			string ujNev = Console.ReadLine();
			ujNev = ujNev.Trim();
			Console.Write("Adja meg az új felhasználó születési dátumát (év-hónap-nap): ");
			string datumString = Console.ReadLine();
			datumString = datumString.Trim();
			DateTime ujSzulDat;
			if (!DateTime.TryParse(datumString,out ujSzulDat)){
				Console.WriteLine("Nem megfelelő formátumban megadott dátum! Felhasználó létrehozása sikertelen.");
				return;
			}
			Console.Write("Adja meg az új felhasználó lakcímét: ");
			string ujLak = Console.ReadLine();
			ujLak = ujLak.Trim();
			Console.Write("Adja meg az új felhasználó jelszavát: ");
			string ujJelszo = Console.ReadLine();
			ujJelszo = ujJelszo.Trim();
			string temp;
			switch (tipus){
				case "sofor":
					Program.felhasznalok.Add(new Sofor(ujNev, igaSzam, ujSzulDat, ujLak, ujJelszo,0));
					Program.FelhasznalokMentese();
					Console.WriteLine("Új felhasználó létrehozása sikeres!");
					break;
				case "operator":
					Console.Write("Adja meg az operátor azonosítóját (egész szám): ");
					temp = Console.ReadLine();
					int ujOpAzon;
					if (!int.TryParse(temp, out ujOpAzon)){
						Console.WriteLine("Nem megfelelő formátumú azonosító! Felhasználó létrehozása sikertelen.");
						return;
					}
					if (!Program.OperatorLetezik(ujOpAzon)){
						Program.felhasznalok.Add(new Operator(ujNev, igaSzam, ujSzulDat, ujLak, ujJelszo, ujOpAzon));
						Program.FelhasznalokMentese();
						Console.WriteLine("Új felhasználó létrehozása sikeres!");
					}
					else{
						Console.WriteLine("Ilyen azonosítóval rendelkező operátor már létezik! Felhasználó létrehozása sikertelen.");
						return;
					}
					break;
				case "admin":
					Console.Write("Adja meg az operátor azonosítóját (egész szám): ");
					temp = Console.ReadLine();
					int ujAdminAzon;
					if (!int.TryParse(temp, out ujAdminAzon)){
						Console.WriteLine("Nem megfelelő formátumú azonosító! Felhasználó létrehozása sikertelen.");
						return;
					}
					if (!Program.AdminLetezik(ujAdminAzon)){
						Program.felhasznalok.Add(new Admin(ujNev, igaSzam, ujSzulDat, ujLak, ujJelszo, ujAdminAzon));
						Program.FelhasznalokMentese();
						Console.WriteLine("Új felhasználó létrehozása sikeres!");
					}
					else{
						Console.WriteLine("Ilyen azonosítóval rendelkező adminisztrátor már létezik! Felhasználó létrehozása sikertelen.");
						return;
					}
					break;
			}
		}
		
		public override void sajatAdatokMegtekintese(){
			base.sajatAdatokMegtekintese();
			Console.WriteLine("Admin azonosító: " + adminAzon);
		}
		
		public override void PublikusAdatokKilistazasa(){
			base.PublikusAdatokKilistazasa();
			Console.WriteLine("A felhasználó admin.");
		}
		
		//********************** GETTERS **************************
		#region Getters
		public int GetAdminAzon(){
			return adminAzon;
		}
		
		#endregion
		//********************** GETTERS **************************
		#region Setters
		public void SetAdminAzon(int ujAzon){
			adminAzon = ujAzon;
		}
		
		#endregion
	}
}
