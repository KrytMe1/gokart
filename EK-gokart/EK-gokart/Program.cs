using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EK_gokart
{
    internal class Program
    {
        public class versenyzo
        {
            public string nev;
            public DateTime szuletesiEv;
            public bool elmult18;
            public string azonosito;
            public string email;

            private static Random rnd = new Random();
            public versenyzo(string nev, DateTime szuletesiEv, bool elmult18, string azonosito, string email)
            {
                this.nev = nev;
                this.szuletesiEv = szuletesiEv;
                this.elmult18 = elmult18;
                this.azonosito = azonosito;
                this.email = email;
            }

            public DateTime SzuletesiEv { get => szuletesiEv; set => szuletesiEv = value; }

            
            public string nevgeneralas()
            {
                string kereszt = File.ReadAllText("keresztnevek.txt");
                string vezetek = File.ReadAllText("vezeteknevek.txt");
                string[] keresztnevek = kereszt.Split(new[] { ',' });
                string[] vezeteknevek = vezetek.Split(new[] { ',' });
                string keresztnev = keresztnevek[rnd.Next(0, keresztnevek.Length)];
                string vezeteknev = vezeteknevek[rnd.Next(0, vezeteknevek.Length)];
                keresztnev = keresztnev.Remove(0, 1);
                string nev = vezeteknev.Replace("'","") + " " + keresztnev.Replace("'", "");
                nev = nev.Remove(0, 1);
                return nev;
            }
            public string azonositogeneralas()
            {
                string accentedStr;
                accentedStr = nev;
                byte[] tempBytes;
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
                string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
                asciiStr = asciiStr.Replace(" ", "");
                string szuletesv = szuletesiEv.ToString("d");
                szuletesv = szuletesv.Replace(".", "");
                szuletesv = szuletesv.Replace(" ", "");
                string azonosito = "GO-" + asciiStr + "-" + szuletesv;
                return azonosito;
            }
            public string emailgeneralas()
            {
                string accentedStr;
                accentedStr = nev.ToLower();
                byte[] tempBytes;
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
                string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
                asciiStr = asciiStr.Replace(" ", ".");
                string email = asciiStr + "@gmail.com";
                return email;
            }
            public bool eletkor()
            {
                DateTime maiDatum = DateTime.Now;
                int eletkor = maiDatum.Year - szuletesiEv.Year;
                bool elmult18;
                if (maiDatum < szuletesiEv.AddYears(eletkor))
                {
                    eletkor--;
                }
                if (eletkor >= 18)
                {
                    elmult18 = true;
                }
                else
                {
                    elmult18 = false;
                }
                return elmult18;
            }
        }
        static void Main(string[] args)
        {
            // Ecsedi Kristóf 09.15. Gokart

            /* Izsáki Citrom Sziget GOkart pálya
                6070 Izsák, Citrom u. 1.
                06 70 947 3030
                citromgokart@gmail.com*/

            /*public override string ToString()
            {
                return $"{nev} {szuletesiEv} {elmult18} {azonosito} {email}";
            }*/

            List<versenyzo> versenyzok = new List<versenyzo>();
            Console.Write("Kérem adja meg a versenyzők számát(8-20): ");
            int versenyzokSzama = int.Parse(Console.ReadLine());

            while (versenyzokSzama < 8 || versenyzokSzama > 20)
            {
                Console.Write("Hibás adat! Kérem adja meg a versenyzők számát(8-20): ");
                versenyzokSzama = int.Parse(Console.ReadLine());
            }


            for (int i = 0; i < versenyzokSzama; i++)
            {
                Console.Write($"Kérem adja meg a(z) {i + 1}. versenyző születési évét(ÉÉÉÉ.HH.NN): ");
                DateTime szuletesiEv = DateTime.Parse(Console.ReadLine());
                versenyzo ujVersenyzo = new versenyzo("", szuletesiEv, false, "", "");
                ujVersenyzo.nev = ujVersenyzo.nevgeneralas(); 
                ujVersenyzo.azonosito = ujVersenyzo.azonositogeneralas();
                ujVersenyzo.email = ujVersenyzo.emailgeneralas();
                ujVersenyzo.elmult18 = ujVersenyzo.eletkor();
                if (ujVersenyzo.elmult18)
                {
                    versenyzok.Add(ujVersenyzo);
                }
                else
                {
                    Console.WriteLine("A versenyző nem múlt el 18 éves, így nem regisztrálható!");
                    i--;
                }
            }

            foreach (var item in versenyzok)
            {
                Console.WriteLine($"{item.nev} {item.SzuletesiEv.ToString("d")} {item.azonosito} {item.email}");
            }

            List<string> berles = new List<string>();
            Random rnd = new Random();
            
            for (int i = 0; i < versenyzok.Count; i++)
            {
                int bereltIdo = rnd.Next(1, 3);
                string berelt = $"{versenyzok[i].nev} {bereltIdo} óra";
                berles.Add(berelt);
                Console.WriteLine(berles[i]);
            }


        }
    }
}
