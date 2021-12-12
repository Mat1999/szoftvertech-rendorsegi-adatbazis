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
		bool bejelentkezett;
		bool kilepes;
		List<felhasznalo> felhasznalok;
		
		
		public static void Main(string[] args)
		{
			
			
		}
		
		public void FoprogramLoop(){
			bejelentkezett = false;
			kilepes = false;
			felhasznalok = new List<felhasznalo>();
			
			while (!kilepes){
				Console.Write("Adja meg a személyigazolványszámát: ");
				string felhasznaloNev = Console.ReadLine();
				Console.Write("Adja meg a jelszavát: ");
				string jelszo = Console.ReadLine();
				while (bejelentkezett){
					
				}
			}
		}
		
		public bool KeresFelhasznalo(){
			return false;
		}
		public void FelhasznalokBetoltese(){
			
		}
	}
}