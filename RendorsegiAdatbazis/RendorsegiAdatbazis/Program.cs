/*
 * Created by SharpDevelop.
 * User: Matyi
 * Date: 2021.12.11.
 * Time: 19:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
			felhasznalok.Add(new Admin("Varga Mátyás","0057SA",new DateTime(1999,12,2),"8074, Csókakő, Kossuth Lajos utca 59.","admin12345",1));
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
				case "sajatadatok":
					bejelentkezettUser.sajatAdatokMegtekintese();
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
			Console.WriteLine("Felhasználók kilistázása		-	kilistazfelhasznalo");
			Console.WriteLine("Saját adatok kiírása			-	sajatadatok");
			Console.WriteLine("Új felhasználó létrehozása	-	ujfelhasznalo TÍPUS");
			Console.WriteLine("Felhasználó törlése			-	torolfelhasznalo AZONOSÍTÓ");
			Console.WriteLine("Kijelentkezés				-	kijelentkezes");
			Console.WriteLine("*************************************************");
			Console.WriteLine("");
		}
		
		public static void FelhasznalokBetoltese(){
			
		}
	}
}