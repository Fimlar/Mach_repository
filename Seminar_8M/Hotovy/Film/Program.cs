using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Film
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Film> films = new List<Film>();
            Film pulpFiction = new Film("Pulp fiction", "Quentin", "Tarantino", 1998);
            Film oppenheimer = new Film("Oppenheimer", "Christopher", "Nolan", 2024);
            Film bulletTrain = new Film("Bullet train", "David", "Leitch", 2022);

            films.Add(pulpFiction);
            films.Add(oppenheimer);
            films.Add(bulletTrain);

            Random random = new Random();
            int randInt;
            // Přidávám 15 náhodných hodnocení ke všem filmům
            foreach (Film film in films)
            {
                for (int i = 0; i < 15; i++)
                {
                    randInt = random.Next(0, 6);
                    film.PridaniHodnoceni(randInt);
                }
            }

            double best = 0;
            int longest = 0;
            foreach (Film film in films)
            {
                if (film.Hodnoceni > best)
                    best = film.Hodnoceni;
                if (film.Nazev.Length > longest)
                    longest = film.Nazev.Length;
                
            }

            foreach (Film film in films)
            {
                if (film.Hodnoceni < 3)
                    Console.WriteLine(film.Nazev + " je odpad. Má hodnocení jen " + film.Hodnoceni);
            }

            foreach(Film film in films)
            {
                Console.WriteLine(film.ToString());
            }

            Console.ReadLine();
        }
    }
    
    /// <summary>
    /// Třída film xd
    /// </summary>
    class Film
    {
        public Film(string nazev, string jmenoRezisera, string prijmeniRezisera, int rokVzniku)
        {
            Nazev = nazev;
            JmenoRezisera = jmenoRezisera;
            PrijmeniRezisera = prijmeniRezisera;
            RokVzniku = rokVzniku;
        }

        public string Nazev {  get; }
        public string JmenoRezisera { get; }
        public string PrijmeniRezisera { get; }
        public int RokVzniku { get; }
        public double Hodnoceni { get; private set; }

        static List<int> HodnoceniList = new List<int>();

        public void PridaniHodnoceni(int novyHodnoceni)
        {
            HodnoceniList.Add(novyHodnoceni);
            double prubezneHodnoceni = 0;
            int n = HodnoceniList.Count();
            for (int i = 0; i < n; i++)
            {
                prubezneHodnoceni += HodnoceniList[i];
            }
            Hodnoceni = prubezneHodnoceni/n;
            return;
        }

        public void VsechnaHodnoceniPrint()
        {
            foreach (int i in HodnoceniList) { Console.WriteLine(i); }
        }
        public override string ToString()
        {
            return string.Format("{0} ({1} {2} {3}) {4}", Nazev, RokVzniku, PrijmeniRezisera, JmenoRezisera[0], Hodnoceni);
        }
    }
}
