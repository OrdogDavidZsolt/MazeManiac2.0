using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Beadandó;
using Karakterek;

namespace Beadandó
{
    class MapHandler
    {
        private char[,] Map;
        List<string> lines = new List<string>();
        public int MapID;
        public int ID = 0;
        public jatekos player = new jatekos();
        public Goblin goblin = new Goblin();
        public Boss főnök = new Boss();
        public Kard Vaskard = new Kard("Kard");
        public Sisak Sisak = new Sisak("Sapka");
        public bool lootolható = true;
        public bool good_ending = false;
        public bool fizettett = false;
        public int tarsoly = 0;
        Random r = new Random();

        public MapHandler(string fn)
        {
            StreamReader sr = new StreamReader(fn);
            string id = sr.ReadLine();
            if (id != "-")
            {
                MapID = int.Parse(id);
            }
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }

            Map = new char[lines.Count(), lines[0].Length];
            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Map[i, j] = lines[i][j];
                }
            }
        }
        public void LoadMap()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    switch (Map[i, j])
                    {
                        case '.': { Console.ForegroundColor = ConsoleColor.Black; break; }
                        case '#': { Console.ForegroundColor = ConsoleColor.DarkGray; break; }
                        case '~': { Console.ForegroundColor = ConsoleColor.Cyan; break; }
                        case 'H': { Console.ForegroundColor = ConsoleColor.DarkYellow; break; }
                        default: { Console.ForegroundColor = ConsoleColor.White; break; }
                    }
                    Console.Write(" " + Map[i, j]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public int[] GetPlayerLoc()
        {
            int[] loc = new int[2];
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == '@')
                    {
                        loc[0] = i;
                        loc[1] = j;
                    }
                }
            }
            return loc;
        }

        public string Next_Map()
        {
            Console.Clear();
            string ret = "";
            ret = $"Maps/{MapID + 1}Level.txt"; ID = 1; Console.Clear();
            return ret;
        }

        public string Previous_Map()
        {
            Console.Clear();
            string ret = "";
            ret = $"Maps/{MapID - 1}Level.txt"; ID = 1; Console.Clear();
            return ret;
        }

        public void Move(int i, int j)
        {
            int[] loc = GetPlayerLoc();
            int x = loc[0];
            int y = loc[1];

            if (Map[x + i, y + j] == '.')
            {
                Map[x + i, y + j] = '@';
                Map[x, y] = '.';

            }
            else if (Map[x + i, y + j] == 'x')
            {
                ID = 3;
            }
            else if (Map[x + i, y + j] == 'y')
            {
                ID = 4;
            }
            else if (Map[x + i, y + j] == 'g')
            {
                goblin.Fight(player);
                if (player.hp > 0)
                {
                    Map[x + i, y + j] = '@';
                    Map[x, y] = '.';
                }
                else
                {
                    ID = 2;
                }
            }
            else if (Map[x + i, y + j] == '~')
            {
                Console.WriteLine("Vízbe esnél");
            }
            else if (Map[x + i, y + j] == '|')
            {
                Console.WriteLine("Az ajtó bezáródott és nem nyílik");
            }
            else if (Map[x + i, y + j] == '_')
            {
                Console.WriteLine("Nem nyílik");
            }
            else if (Map[x + i, y + j] == 'H')
            {
                Console.WriteLine("Kerítés");
            }
            else if (Map[x + i, y + j] == 'o')
            {
                tarsoly++;
                Map[x + i, y + j] = '@';
                Map[x, y] = '.';
            }
            else if (Map[x + i, y + j] == 'L')
            {
                List<string> lista = new List<string>()
                { "Lady of the lake: Üdvözöllek",
                  "Lady of the lake: Gyönyörű napunk van",
                  "Lady of the lake: Kis kacsa fürdik",
                  "Lady of the lake: Ez itt a kertem",
                  "Lady of the lake: Sok szerencsét"
                };
                Console.WriteLine(lista[r.Next(0, 5)]);
            }
            else if (Map[x + i, y + j] == 'M')
            {
                List<string> lista = new List<string>()
                { "Mókus: Makk",
                  "Mókus: Makk Makk",
                  "Mókus: Mikk Makk",
                  "Mókus: Mikk Makk Mikk Makk",
                  "Mókus: Mogyoró"
                };
                Console.WriteLine(lista[r.Next(0, 5)]);
            }
            else if (Map[x + i, y + j] == 'C' && lootolható == true)
            {
                Console.WriteLine("A ládában találtál egy sisakot és egy kardot!");
                Vaskard.felvesz(player, Vaskard);
                Sisak.felvesz(player, Sisak);
                lootolható = false;
            }
            else if (Map[x + i, y + j] == 'C' && lootolható == false)
            {
                Console.WriteLine("Ebből már mindent kiszedtél");
            }
            else if (Map[x + i, y + j] == 'B')
            {
                Console.WriteLine("Béla: Van elég pénzed kifizetni a tartozásod a főnöknek? 10 coinnal tartozol! Nyomj igent (i) vagy nemet (n)");
                char m = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (m)
                {
                    case 'i':
                        {
                            Console.WriteLine("Béla: Lássuk csak");
                            if (fizettett == true) { Console.WriteLine("Béla: Csak beszélj a főnökkel, a házban vár"); good_ending = true; }
                            else if (fizettett == false && tarsoly >= 1) { tarsoly = tarsoly - 1; fizettett = true; good_ending = true; }
                            else if (tarsoly < 1 && fizettett == false) { Console.WriteLine("Béla: Megpróbálsz átverni??"); }
                            break;
                        }
                    case 'n':
                        {
                            Console.WriteLine("Béla: Rendezzétek le akkor, bent vár"); good_ending = false;
                            break;
                        }
                    default:
                        break;
                }
            }
            else if (Map[x + i, y + j] == 'D')
            {
                if (good_ending == true) { Console.WriteLine("Főnök: Tudtam, hogy bízhatok benned"); }
                else if (good_ending == false)
                {
                    főnök.Fight(player);
                    if (player.hp > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Sikeres megölted a főnököt és átvetted az irányítást a maffia felett");
                        ID = 1;
                    }
                    else
                    {
                        ID = 2;
                    }
                }
            }
            else
            {
                Console.WriteLine("Falba Ütköztél!");
            }
        }
    }
}

