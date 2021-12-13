/*
 * Created by SharpDevelop.
 * User: Matyi
 * Date: 2021.12.11.
 * Time: 19:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace RendorsegiAdatbazis
{
	class Program
	{
		static bool bejelentkezett;
		static bool kilepes;
		public static List<felhasznalo> felhasznalok;
		static felhasznalo bejelentkezettUser;
		
		
		public static void Main(string[] args)
		{
			FoprogramLoop();
		}
		
		public static void FoprogramLoop(){
			bejelentkezett = false;
			kilepes = false;
			felhasznalok = new List<felhasznalo>();
			bejelentkezettUser = null;
			if (!File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"users.txt"))){
				felhasznalok.Add(new Admin("Varga Mátyás","0057SA",new DateTime(1999,12,2),"8074, Csókakő, Kossuth Lajos utca 59.","admin12345",1));
			}
			else{
				FelhasznalokBetoltese();
			}
			string temp;
			string temp2;
			while (!kilepes){
				Console.Write("Ön nincs bejelentkezve. Jelentkezzen be, vagy lépjen ki: ");
				temp = Console.ReadLine();
				temp = temp.Trim();
				if (temp == "bejelentkezes"){
					Console.Write("Adja meg a személyigazolványszámát: ");
					string felhasznaloNev = Console.ReadLine();
					Console.Write("Adja meg a jelszavát: ");
					string jelszo = Console.ReadLine();
					felhasznaloNev = felhasznaloNev.Trim();
					jelszo = jelszo.Trim();
					bool siker = Bejelentkezes(felhasznaloNev, jelszo);
					if (!siker){
						Console.WriteLine("Bejelentkezés sikertelen.");
					}
					else{
						Console.WriteLine("Üdvözöljük {0}!", bejelentkezettUser.GetNev());
					}
					while (bejelentkezett){
						Console.Write("Adjon meg egy parancsot (parancsok listázásához írjon ?-t): ");
						temp2 = Console.ReadLine();
						temp2 = temp2.Trim();
						switch (temp2){
							case "?":
								ParancsokKiirasa();
								break;
							case "kijelentkezes":
								bejelentkezettUser = null;
								bejelentkezett = false;
								Console.WriteLine("Ön kijelentkezett.");
								break;
							default:
								ParancsVegrehajtas(temp2);
								break;
						}
					}
				}
				else if (temp == "kilepes"){
					kilepes = true;
				}
				else{
					Console.WriteLine("Nem értelmezhető parancs!");
				}
			}
		}
		
		public static int KeresFelhasznalo(string szemelyAzon){
			for (int i = 0; i < felhasznalok.Count; i++){
				if (felhasznalok[i].GetIgazolvanySzam() == szemelyAzon){
					return i;
				}
			}
			return -1;
		}
		
		public static void ParancsVegrehajtas(string parancs){
			string[] parancsEsParameter = parancs.Split(' ');
			
			switch (parancsEsParameter[0]){
				case "kilistazfelhasznalo":
					if (bejelentkezettUser is Admin){
						((Admin)bejelentkezettUser).OsszesFelhasznaloKilistazasa();
					}
					else if (bejelentkezettUser is Operator){
						((Operator)bejelentkezettUser).felhasznalokListazasa();
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "ujfelhasznalo":
					if (bejelentkezettUser is Admin){
						if (parancsEsParameter.Length == 2){
							((Admin)bejelentkezettUser).UjFelhasznaloLetrehozasa(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "torolfelhasznalo":
					if (bejelentkezettUser is Admin){
						if (parancsEsParameter.Length == 2){
							if (parancsEsParameter[1] != bejelentkezettUser.GetIgazolvanySzam()){
								((Admin)bejelentkezettUser).FelhasznaloTorlese(parancsEsParameter[1]);
							}
							else{
								Console.WriteLine("Saját magát nem törölheti!");
							}
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "felhasznaloadatok":
					if (parancsEsParameter.Length == 2){
						if (bejelentkezettUser is Operator){
							((Operator)bejelentkezettUser).FelhasznaloAdatainakMegtekintese(parancsEsParameter[1]);
						}
						else if (bejelentkezettUser is Admin){
							((Admin)bejelentkezettUser).FelhasznaloAdatainakMegtekintese(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
						}
					}
					else{
						Console.WriteLine("Nem megfelelő számú paraméter!");
					}
					break;
				case "sajatadatok":
					bejelentkezettUser.sajatAdatokMegtekintese();
					break;
				case "kifizet":
					if (bejelentkezettUser is Sofor){
						if (parancsEsParameter.Length == 2){
							((Sofor)bejelentkezettUser).BirsagKifizetese(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Ön nem sofőr, nincsenek bírságai.");
					}
					break;
				case "befizet":
					if (bejelentkezettUser is Sofor){
						if (parancsEsParameter.Length == 2){
							int osszeg;
							if (int.TryParse(parancsEsParameter[1], out osszeg)){
								((Sofor)bejelentkezettUser).penzFeltoltes(osszeg);
							}
							else{
								Console.WriteLine("Nem megfelelő formátumú pénzösszeg (egész szám).");
							}
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Ön nem sofőr, nincsenek bírságai.");
					}
					break;
				case "sajatbirsagok":
					if (bejelentkezettUser is Sofor){
						((Sofor)bejelentkezettUser).sajatBirsagokMegtekintese();
					}
					else{
						Console.WriteLine("Ön nem sofőr, nincsenek bírságai.");
					}
					break;
				case "kilistazbirsag":
					if (bejelentkezettUser is Operator){
						((Operator)bejelentkezettUser).osszesBirsagMegtekintese();
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "felhasznalobirsagok":
					if (bejelentkezettUser is Operator){
						if (parancsEsParameter.Length == 2){
							((Operator)bejelentkezettUser).felhasznaloBirsagainakListazasa(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "ujbirsag":
					if (bejelentkezettUser is Operator){
						((Operator)bejelentkezettUser).birsagFelvetele();
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "torolbirsag":
					if (bejelentkezettUser is Operator){
						if (parancsEsParameter.Length == 2){
							((Operator)bejelentkezettUser).birsagTorlese(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "modositbirsag":
					if (bejelentkezettUser is Operator){
						if (parancsEsParameter.Length == 2){
							if (BirsagLetezik(parancsEsParameter[1])){
								string felhasznaloAzon = "";
								foreach (felhasznalo user in felhasznalok) {
									if (user is Sofor){
										((Sofor)user).BirsagKeresese(parancsEsParameter[1]);
										felhasznaloAzon = user.GetIgazolvanySzam();
									}
								}
								int felhaszAzon = KeresFelhasznalo(felhasznaloAzon);
								if (felhaszAzon != -1){
									((Operator)bejelentkezettUser).BirsagAdatainakModositasa(parancsEsParameter[1], felhaszAzon);
								}
							}
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "modositfelhasznalo":
					if (bejelentkezettUser is Admin){
						if (parancsEsParameter.Length == 2){
							((Admin)bejelentkezettUser).FelhasznaloModositasa(parancsEsParameter[1]);
						}
						else{
							Console.WriteLine("Nem megfelelő számú paraméter!");
						}
					}
					else{
						Console.WriteLine("Önnek nincs joga a parancs végrehajtására!");
					}
					break;
				case "modositjelszo":
					if (parancsEsParameter.Length == 2){
						bejelentkezettUser.SetJelszo(parancsEsParameter[1]);
						Program.FelhasznalokMentese();
						Console.WriteLine("Jelszó sikeresen megváltoztatva.");
					}
					else{
						Console.WriteLine("Nem megfelelő számú paraméter!");
					}
					break;
				default:
					Console.WriteLine("Nem értelmezhető parancs");
					break;
			}
		}
		
		public static bool BirsagLetezik(string id){
			foreach (felhasznalo user in felhasznalok){
				if (user is Sofor){
					Sofor driver = (Sofor)user;
					int talalat = driver.BirsagKeresese(id);
					if (talalat != -1){
						return true;
					}
				}
			}
			return false;
		}
		
		private static bool Bejelentkezes(string azonosito, string _jelszo){
			int index = KeresFelhasznalo(azonosito);
			if (index != -1){
				if (felhasznalok[index].GetJelszo() == _jelszo){
					bejelentkezettUser = felhasznalok[index];
					bejelentkezett = true;
					return true;
				}
				else{
					Console.WriteLine("Hibás jelszó!");
					return false;
				}
			}
			Console.WriteLine("Nincs ilyen felhasználó!");
			return false;
		}
		
		public static bool OperatorLetezik(int azon){
			foreach (felhasznalo user in felhasznalok) {
				if (user is Operator){
					if (((Operator)user).GetOperatorAzon() == azon){
						return true;
					}
				}
			}
			return false;
		}
		
		public static bool AdminLetezik(int azon){
			foreach (felhasznalo user in felhasznalok) {
				if (user is Admin){
					if (((Admin)user).GetAdminAzon() == azon){
						return true;
					}
				}
			}
			return false;
		}
		
		public static void ParancsokKiirasa(){
			Console.WriteLine("");
			Console.WriteLine("*************************************************");
			Console.WriteLine("Használható parancsok (parancs PARAMÉTER):");
			Console.WriteLine("-----ÁLTALÁNOS------");
			Console.WriteLine("Saját adatok kiírása			-	sajatadatok");
			Console.WriteLine("Kijelentkezés				-	kijelentkezes");
			Console.WriteLine("Jelszó módosítása			-	modositjelszo JELSZÓ");
			Console.WriteLine("-------SOFŐR--------");
			Console.WriteLine("Bírság kifizetése			-	kifizet BÍRSÁGAZONOSÍTÓ");
			Console.WriteLine("Pénz befizetése a számlára	-	befizet PÉNZÖSSZEG");
			Console.WriteLine("Saját bírságok kilistázása	-	sajatbirsagok");
			Console.WriteLine("------OPERÁTOR------");
			Console.WriteLine("Felhasználók kilistázása		-	kilistazfelhasznalo");
			Console.WriteLine("Felhasználó adatainak kiírása-	felhasznaloadatok AZONOSÍTÓ");
			Console.WriteLine("Összes bírság kilistázása	-	kilistazbirsag");
			Console.WriteLine("Felhasználó bírságainak kiírása-	felhasznalobirsagok AZONOSÍTÓ");
			Console.WriteLine("Új bírság felvétele			-	ujbirsag");
			Console.WriteLine("Bírság törlése				-	torolbirsag BÍRSÁGAZONOSÍTÓ");
			Console.WriteLine("Bírság adatainak módosítása	-	modositbirsag BÍRSÁGAZONOSÍTÓ");
			Console.WriteLine("---ADMINISZTRÁTOR---");
			Console.WriteLine("Felhasználók kilistázása		-	kilistazfelhasznalo");
			Console.WriteLine("Felhasználó adatainak kiírása-	felhasznaloadatok AZONOSÍTÓ");
			Console.WriteLine("Új felhasználó létrehozása	-	ujfelhasznalo TÍPUS");
			Console.WriteLine("Felhasználó törlése			-	torolfelhasznalo AZONOSÍTÓ");
			Console.WriteLine("Felhasználó adatainak módosítása-modositfelhasznalo AZONOSÍTÓ");
			Console.WriteLine("*************************************************");
			Console.WriteLine("");
		}
		
		public static void FelhasznalokMentese(){
			string userPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"users.txt");
			StreamWriter writer = new StreamWriter(userPath);
			foreach (felhasznalo user in felhasznalok) {
				if (user is Admin){
					writer.WriteLine("admin;" + user.GetIgazolvanySzam() + ";" + user.GetNev() + ";" + user.GetSzulDatum().ToString() + ";" + user.GetLakhely() + ";"
					                 + user.GetJelszo() + ";" + ((Admin)user).GetAdminAzon());
				}
				else if(user is Operator){
					writer.WriteLine("operator;" + user.GetIgazolvanySzam() + ";" + user.GetNev() + ";" + user.GetSzulDatum().ToString() + ";" + user.GetLakhely() + ";"
					                 + user.GetJelszo() + ";" + ((Operator)user).GetOperatorAzon());
				}
				else{
					writer.WriteLine("sofor;" + user.GetIgazolvanySzam() + ";" + user.GetNev() + ";" + user.GetSzulDatum().ToString() + ";" + user.GetLakhely() + ";"
					                 + user.GetJelszo() + ";" + ((Sofor)user).GetEgyenleg());
				}
			}
			writer.Close();
		}
		
		public static void BirsagokMentese(){
			string birsagPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"birsagok.txt");
			StreamWriter writer = new StreamWriter(birsagPath);
			foreach (felhasznalo user in felhasznalok) {
				if (user is Sofor){
					foreach (Birsag buntetes in ((Sofor)user).GetBirsagok()) {
						writer.WriteLine(buntetes.GetSzemelyigazolvanySzam() + ";" +
						                 buntetes.GetBirsagID() + ";" +
						                 buntetes.GetRendszam() + ";" +
						                 buntetes.GetMertSebesseg().ToString() + ";" +
						                 buntetes.GetRogzitesIdopontja().ToString() + ";" +
						                 buntetes.GetRogzitoAzonositoja() + ";" +
						                 buntetes.GetBirsagOsszege().ToString() + ";" +
						                 buntetes.GetKifizetett().ToString());
					}
				}
			}
			writer.Close();
			
		}
		
		public static void FelhasznalokBetoltese(){
			int adminCounter = 0;
			string userPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"users.txt");
			int counter = 0;
			foreach (string line in File.ReadLines(userPath)){
				try{
					string[] values = line.Split(';');
					DateTime szulev = DateTime.Parse(values[3]);
					int egesz = int.Parse(values[6]);
					switch (values[0]){
						case "admin":
							felhasznalok.Add(new Admin(values[2],values[1],szulev,values[4],values[5],egesz));
							adminCounter++;
							break;
						case "sofor":
							felhasznalok.Add(new Sofor(values[2],values[1],szulev,values[4],values[5],egesz));
							break;
						case "operator":
							felhasznalok.Add(new Operator(values[2],values[1],szulev,values[4],values[5],egesz));
							break;
						default:
							counter++;
							break;
					}
				}
				catch{
					counter++;
				}
			}
			Console.WriteLine("Hibás felhasználó adatok száma: " + counter);
			if (adminCounter == 0){
				felhasznalok.Add(new Admin("Varga Mátyás","0057SA",new DateTime(1999,12,2),"8074, Csókakő, Kossuth Lajos utca 59.","admin12345",1));
			}
			string birsagPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"birsagok.txt");
			int birsagHiba = 0;
			if (File.Exists(birsagPath)){
				foreach (string line in File.ReadLines(birsagPath)){
					try{
						string[] birsagErtekek = line.Split(';');
						int index = KeresFelhasznalo(birsagErtekek[0]);
						if (index != -1){
							((Sofor)felhasznalok[index]).BirsagRogzitese(new Birsag(birsagErtekek[1],int.Parse(birsagErtekek[5]),DateTime.Parse(birsagErtekek[4]),birsagErtekek[0],birsagErtekek[2],double.Parse(birsagErtekek[3]),int.Parse(birsagErtekek[6]),bool.Parse(birsagErtekek[7])));
						}
					}
					catch{
						birsagHiba++;
					}
				}
				Console.WriteLine("Hibás bírságok száma: " + birsagHiba);
			}
		}
	}
}