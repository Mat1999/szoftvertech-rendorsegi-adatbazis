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
			string temp;
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
					
					while (bejelentkezett){
						
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
			Console.WriteLine("*************************************************");
			Console.WriteLine("Használható parancsok:");
			
			Console.WriteLine("*************************************************");
		}
		
		public static void FelhasznalokBetoltese(){
			
		}
	}
}