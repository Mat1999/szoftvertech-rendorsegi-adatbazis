/*
 * Created by SharpDevelop.
 * User: Matyi
 * Date: 2021.12.11.
 * Time: 19:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace RendorsegiAdatbazis
{
	/// <summary>
	/// Description of Birsag.
	/// Szükséges adatok: sofőr személyigazolványszáma ,kocsi rendszáma, mért sebesség, bírság rögzítésének időpontja, bírság összege, kifizetés státusza, rögzítő operátor azonosítója
	/// </summary>
	public class Birsag
	{
		private bool kifizetett;
		private DateTime rogzitesIdopontja;
		private string szemelyigazolvanyszam;
		private string rogzitoAzonositoja;
		private string rendszam;
		private double mertSebesseg;
		private int birsagOsszege;
		public Birsag(string rogAzon, DateTime rogIdo, string igaSzam, string rendsz, double sebesseg, int birsag)
		{
			kifizetett = false;
			rogzitoAzonositoja = rogAzon;
			rogzitesIdopontja = rogIdo;
			szemelyigazolvanyszam = igaSzam;
			rendszam = rendsz;
			mertSebesseg = sebesseg;
			birsagOsszege = birsag;
		}
		
		public void BirsagKifizetese(){
			kifizetett = true;
		}
		
		public void BirsagAdatainakKiirasa(bool operatorFelhaszn){
			Console.WriteLine("Bírság rögzítésének időpontja: " + rogzitesIdopontja.ToString());
			Console.WriteLine("Sofőr személyigazolványszáma: {0}, Kocsijának rendszáma: {1}", szemelyigazolvanyszam, rendszam);
			Console.WriteLine("Mért sebesség: {0} km/h", mertSebesseg);
			Console.WriteLine("Bírság összege: {0} Ft", birsagOsszege);
			if (operatorFelhaszn){
				Console.WriteLine("Rögzítő operátor azonosítója: " + rogzitoAzonositoja);
			}
		}
		
		//********************** GETTERS **************************
		#region Getters
		public DateTime GetRogzitesIdopontja(){
			return rogzitesIdopontja;
		}
		public string GetSzemelyigazolvanySzam(){
			return szemelyigazolvanyszam;
		}
		public string GetRogzitoAzonositoja(){
			return rogzitoAzonositoja;
		}
		public string GetRendszam(){
			return rendszam;
		}
		public double GetMertSebesseg(){
			return mertSebesseg;
		}
		public int GetBirsagOsszege(){
			return birsagOsszege;
		}
		#endregion
		
		//********************** SETTERS **************************
		#region Setters
		public void SetRogzitesIdopontja(DateTime rogIdo){
			rogzitesIdopontja = rogIdo;
		}
		public void SetSzemelyigazolvanySzam(string igaSzam){
			szemelyigazolvanyszam = igaSzam;
		}
		public void SetRogzitoAzonositoja(string rogAzon){
			rogzitoAzonositoja = rogAzon;
		}
		public void SetRendszam(string rendsz){
			rendszam = rendsz;
		}
		public void SetMertSebesseg(double sebesseg){
			mertSebesseg = sebesseg
		}
		public void SetBirsagOsszege(int osszeg){
			birsagOsszege = osszeg;
		}
		#endregion
		
	}
}
