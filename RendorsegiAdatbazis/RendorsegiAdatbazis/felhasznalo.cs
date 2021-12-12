using System;
using System.IO;

namespace RendorsegiAdatbazis
{
	public class felhasznalo
	{
		
		protected string nev;
		protected string igazolvanySzam;
		protected DateTime szulDatum;
		protected string lakhely;
		protected string jelszo;
		
		public felhasznalo(string _nev,string _szemIgSz,DateTime _szuldate,string _lakhely,string _jelszo) {
			this.nev = _nev;
			this.igazolvanySzam = _szemIgSz;
			this.szulDatum = _szuldate;
			this.lakhely = _lakhely;
			this.jelszo = _jelszo;
		}
		
		public virtual void sajatAdatokMegtekintese() {
			Console.WriteLine("Név: "+nev);
			Console.WriteLine("Igazolványszám: " + igazolvanySzam);
			Console.WriteLine("Születési dátum: " + szulDatum);
			Console.WriteLine("Lakhely: " + lakhely);
			Console.WriteLine("Jelszo: " + jelszo);
		}
		
		public virtual void PublikusAdatokKilistazasa(){
			Console.WriteLine("Név: "+nev);
			Console.WriteLine("Igazolványszám: " + igazolvanySzam);
			Console.WriteLine("Születési dátum: " + szulDatum);
			Console.WriteLine("Lakhely: " + lakhely);
		}
		
		//********************** GETTERS **************************
		#region Getters
		public string GetNev(){
			return nev;
		}
		public string GetIgazolvanySzam(){
			return igazolvanySzam;
		}
		public DateTime GetSzulDatum(){
			return szulDatum;
		}
		public string GetLakhely(){
			return lakhely;
		}
		public string GetJelszo(){
			return jelszo;
		}
		
		#endregion
		//********************** GETTERS **************************
		#region Setters
		public void SetNev(string _nev){
			nev = _nev;
		}
		public void SetIgazolvanySzam(string _igSzam){
			igazolvanySzam = _igSzam;
		}
		public void SetSzulDatum(DateTime _szulDatum){
			szulDatum = _szulDatum;
		}
		public void SetLakhely(string _lakhely){
			lakhely = _lakhely;
		}
		public void GetJelszo(string _jelszo){
			jelszo = _jelszo;
		}
		
		#endregion

	}
}