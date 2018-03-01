using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_w_zycie
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int[,] pole = new int[12, 12];
            int[,] sasiedztwa = new int[12, 12];

            void wyswietl(int[,] tab)
            {

                for (int i =1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                   
                        if (tab[i, j] == 1)
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
            void wyswietls(int[,] tab)
            {

                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {

                        Console.Write(tab[i, j]);
                    }
                    Console.WriteLine();

                }
            }
            void losuj(int[,] tab)
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        tab[i, j] = 0;
                    }

                }

                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        tab[i, j] = rand.Next(0, 2);
                    }

                }
            }
            void liczenie_sasiedztwa(int[,] tab)
            {
                int licznik_sasiada = 0;
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        
                      for (int k=i-1; k<i+1; k++)
                        {
                            
                            for (int l=j-1; l<j+1; l++)
                            {
                                if (tab[k, l] == 1)
                                {
                                    licznik_sasiada++;

                                }
                          
                            }
                    
                        }
                        if (tab[i, j] == 1)
                        {
                            licznik_sasiada--;
                        }
                        sasiedztwa[i, j] = licznik_sasiada;
                        licznik_sasiada = 0;
                    }

                }
            }
            void warunki(int[,] tab, int[,] tab2)
            {
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (tab2[i, j] == 0)
                        {
                            if (tab[i, j] == 3)
                            {
                                tab2[i, j] = 1;
                            }
                        }
                        if (tab2[i, j] == 1)
                        {
                            if (tab[i, j] == 2 || tab[i, j] == 3)
                            {
                                tab2[i, j] = 1;
                            }
                            if (tab[i, j] < 2)
                            {
                                tab2[i, j] = 0;
                            }
                            
                            if (tab[i, j] > 3)
                            {
                                tab2[i, j] = 0;
                            }
                        }
                        
                    }
                }
             }
            void zakonczenie(int[,] tab)
            {
                int licznik=0;
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if(tab[i,j]==1)
                        {
                            licznik++;
                        }
                    }
                }
                if (licznik == 0)
                {
                    Task.Delay(1000);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                
                }
            }
            losuj(pole);
            wyswietl(pole);
            
            while (true) {
                liczenie_sasiedztwa(pole);
                wyswietls(sasiedztwa);
                warunki(sasiedztwa, pole);
                wyswietl(pole);
                Console.ReadKey();
                zakonczenie(pole);
            }
        }
    }
}
