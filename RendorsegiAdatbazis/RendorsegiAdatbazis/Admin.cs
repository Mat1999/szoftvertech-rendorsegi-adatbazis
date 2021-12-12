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
			}
			else{
				Console.WriteLine("Ilyen személyigazolványszámmal rendelekező felhasználó nem létezik! Törlés sikertelen.");
			}
		}
		
		public void OsszesFelhasznaloKilistazasa(){
			foreach (felhasznalo user in Program.felhasznalok) {
				Console.WriteLine("--------------------------------------");
				user.sajatAdatokMegtekintese();
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
					Program.felhasznalok.Add(new Sofor(ujNev, igaSzam, ujSzulDat, ujLak, ujJelszo));
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
