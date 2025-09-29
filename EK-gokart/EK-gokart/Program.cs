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
            private string nev;
            private DateTime szuletesiEv;
            private bool elmult18;
            private string azonosito;
            private string email;


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
                List<string> keresztnevek = File.ReadAllLines("keresztnevek.txt").ToList();
                List<string> vezeteknevek = File.ReadAllLines("vezeteknevek.txt").ToList();

                
                Random rnd = new Random();
                int keresztnevIndex = rnd.Next(0, keresztnevek.Count);
                int vezeteknevIndex = rnd.Next(0, vezeteknevek.Count);
                string keresztnev = keresztnevek[keresztnevIndex];
                string vezeteknev = vezeteknevek[vezeteknevIndex];
                string nev = vezeteknev.Replace("'","") + " " + keresztnev.Replace("'", "");
                nev.Replace(",", "");
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

            

            for (int i = 0; i < versenyzokSzama; i++)
            {
                Console.Write($"Kérem adja meg a(z) {i + 1}. versenyző születési évét(ÉÉÉÉ.HH.NN): ");
                DateTime szuletesiEv = DateTime.Parse(Console.ReadLine());
                versenyzo ujVersenyzo = new versenyzo("", szuletesiEv, false, "", "");
                ujVersenyzo.nevgeneralas();
                ujVersenyzo.azonositogeneralas();
                ujVersenyzo.emailgeneralas();
                ujVersenyzo.eletkor();
                if (ujVersenyzo.eletkor() == true)
                {
                    Console.WriteLine("A versenyző elmúlt el 18 éves!");
                }
                else
                {
                    Console.WriteLine("A versenyző nem múlt el 18 éves!");
                }

                
            }
            for (int j = 0; j < versenyzokSzama; j++)
            {
                Console.WriteLine($"Név: {versenyzok[j].nevgeneralas()}, Születési év: {versenyzok[j]}, Azonosító: {versenyzok[j]}, Email: {versenyzok[j]}");
            }

        }
    }
}
