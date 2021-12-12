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
			
			while (!kilepes){
				Console.Write("Adja meg a személyigazolványszámát: ");
				string felhasznaloNev = Console.ReadLine();
				Console.Write("Adja meg a jelszavát: ");
				string jelszo = Console.ReadLine();
				felhasznaloNev = felhasznaloNev.Trim();
				jelszo = jelszo.Trim();
				
				while (bejelentkezett){
					
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
		
		public static void FelhasznalokBetoltese(){
			
		}
	}
}