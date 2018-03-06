using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_w_zycie
{
    public class Gra_w_zycie
    {
        private int[,] pole;
        private int[,] sasiedztwa;
        int rozmiar_pola;
        int populacja;

        /************************************Metody prywatne:********************************/

        //Metoda losuje rozlożenie populacji. Oznaczenia: 1-żywy osobnik 0-martwy osobnik
        private void losuj()
        {
            Random rand = new Random();
            //wyzerowanie pola
            for (int i = 0; i < rozmiar_pola; i++)
            {
                for (int j = 0; j < rozmiar_pola; j++)
                {
                    pole[i, j] = 0;
                }
            }
            //wylosowanie populacji
            for (int i = 1; i < rozmiar_pola - 1; i++)
            {
                for (int j = 1; j < rozmiar_pola - 1; j++)
                {
                    pole[i, j] = rand.Next(0, 2);
                    if (pole[i, j] == 1) populacja++;
                }
            }
        }
        //dokończyć:
        private void liczenie_sasiedztwa()
        {
            int licznik_sasiada = 0;
            for (int i = 1; i < rozmiar_pola - 1; i++)
            {
                for (int j = 1; j < rozmiar_pola - 1; j++)
                {

                    for (int k = i - 1; k <= i + 1; k++)
                    {

                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (pole[k, l] == 1)
                            {
                                licznik_sasiada++;
                            }
                        }
                    }
                    if (pole[i, j] == 1)
                    {
                        licznik_sasiada--;
                    }
                    sasiedztwa[i, j] = licznik_sasiada;
                    licznik_sasiada = 0;
                }
            }
        }
        //Metoda wyswietla populacje. Oznaczenia: "." - martwy osobnik  "#" - żywy osobnik
        private void wyswietl()
        {
            for (int i = 1; i < rozmiar_pola - 1; i++)
            {
                for (int j = 1; j < rozmiar_pola - 1; j++)
                {

                    if (pole[i, j] == 1)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
        //Metoda oblicza nowa populacje
        private void oblicz_nowa_populacje()
        {
            for (int i = 1; i < rozmiar_pola - 1; i++)
            {
                for (int j = 1; j < rozmiar_pola - 1; j++)
                {
                    //spawdzenie czy komórka byla żywa
                    if (pole[i, j] == 1)
                    {
                        // Każda żywa komórka, która posiada mniej
                        //niż dwóch żywych sąsiadów umiera
                        //(osamotnienie)
                        if (sasiedztwa[i, j] < 2)
                        {
                            pole[i, j] = 0;
                            populacja--;
                        }
                        //Każda żywa komórka, która posiada dwóch
                        //lub trzech żywych sąsiadów przeżywa
                        if (sasiedztwa[i, j] == 2 || sasiedztwa[i, j] == 3)
                        {
                            pole[i, j] = 1;
                        }
                        //Każda żywa komórka, która posiada więcej
                        //niż trzech żywych sąsiadów umiera
                        //(przeludnienie)
                        if (sasiedztwa[i, j] > 3)
                        {
                            pole[i, j] = 0;
                            populacja--;
                        }
                    }
                    //spawdzenie czy komórka byla martwa
                    else
                    {
                        //Każda martwa komórka, która posiada
                        //dokładnie trzech sąsiadów, ożywa
                        //(reprodukcja)
                        if (sasiedztwa[i, j] == 3)
                        {
                            pole[i, j] = 1;
                            populacja++;
                        }
                    }

                }
            }
        }

        /************************************Metody publiczne:********************************/
        //Metoda oblicza i wyswietla kolejna ture życia populacji
        public void zagraj_ture()
        {
            liczenie_sasiedztwa();
            oblicz_nowa_populacje();
            wyswietl();
        }

        //Meotda zwraca rozmiar populacji
        public int rozmiar_populacji()
        {
            return populacja;
        }





        public Gra_w_zycie(int rozmiar_gry = 10)
        {
            populacja = 0;
            rozmiar_pola = rozmiar_gry + 2;
            pole = new int[rozmiar_pola, rozmiar_pola];
            sasiedztwa = new int[rozmiar_pola, rozmiar_pola];
            losuj();
            wyswietl();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int rozmiar;
            int nr_tury = 1;
            Console.WriteLine("GRA W ŻYCIE: ");
            Console.WriteLine("Gra polega na obserwacji rozwoju populacji.");
            Console.WriteLine();
            Console.WriteLine("Na planszy przestawione zostana 2 rodzaje osobnikow.");
            Console.WriteLine(". - oznacza martwego osobnika");
            Console.WriteLine("# - oznacza żywego osobnika");
            Console.WriteLine();
            Console.WriteLine("Gra toczy sie według następjących zasad: ");
            Console.WriteLine("1) Każda żywa komórka, która posiada mniej niż dwóch żywych sąsiadów umiera (osamotnienie)");
            Console.WriteLine("2) Każda żywa komórka, która posiada dwóch lub trzech żywych sąsiadów przeżywa");
            Console.WriteLine("3) Każda żywa komórka, która posiada więcejniż trzech żywych sąsiadów umiera (przeludnienie)");
            Console.WriteLine("4) Każda martwa komórka, która posiada dokładnie trzech sąsiadów, ożywa (reprodukcja)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Podaj rozmiar planszy: ");

            string odczytana_linia = Console.ReadLine();

            while (!int.TryParse(odczytana_linia, out rozmiar))
            {
                Console.Clear();
                Console.WriteLine("Musisz podac liczbe! Wprowadź ponownie:");
                odczytana_linia = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Wylosowana populacja wyglada nastepujaco: ");
            Gra_w_zycie gra = new Gra_w_zycie(rozmiar);
            Console.Write("ROZMIAR POPULACJA: ");
            Console.Write(gra.rozmiar_populacji());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Wciśnij dowolny przycisk, aby rozpoczac pierwsza ture...");
            Console.ReadKey();



            do {
                Console.Clear();
                Console.Write("Tura numer ");
                Console.Write(nr_tury);
                nr_tury++;
                Console.WriteLine();
                gra.zagraj_ture();
                Console.Write("ROZMIAR POPULACJA: ");
                Console.Write(gra.rozmiar_populacji());
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Wciśnij ESC, aby zakonczyc lub dowolny przycisk, aby kontynuować... ");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            

        }
    }
}