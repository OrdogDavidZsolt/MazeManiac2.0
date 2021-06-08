using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Beadandó;
using Karakterek;

namespace Beadandó
{

    class Program
    {
        static void Main(string[] args)
        {
            MapHandler maphandler;
            maphandler = new MapHandler("Maps/StartLevel.txt");
            do
            {
                maphandler.LoadMap();
                char m = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (m)
                {
                    case 'a': { maphandler.Move(0, -1); break; }
                    case 'w': { maphandler.Move(-1, 0); break; }
                    case 's': { maphandler.Move(1, 0); break; }
                    case 'd': { maphandler.Move(0, 1); break; }
                    case 't': { maphandler.player.taska.kiir(); break; }
                    case 'p': { Console.WriteLine("Tarsoly tartalma: " + maphandler.tarsoly + " érme"); break; }
                    case 'h': { Console.Clear(); Console.WriteLine("w a s d - mozgás \t t - hátizsák \t p - tarsoly"); Console.ReadLine(); Console.Clear(); break; }
                    default: { Console.WriteLine("Nincs ilyen parancs"); break; }
                }
                if (maphandler.ID == 3)
                {
                    maphandler = new MapHandler(maphandler.Next_Map());
                }
                else if (maphandler.ID == 4)
                {
                    maphandler = new MapHandler(maphandler.Previous_Map());
                }
            } while (maphandler.ID == 0);
            Console.ReadLine();
        }
    }
}