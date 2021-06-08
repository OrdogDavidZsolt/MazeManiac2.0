using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karakterek;

namespace Beadandó
{
    class hatizsak
    {
        public List<fegyver> weapons;
        public List<pancel> armors;

        public hatizsak()
        {
            weapons = new List<fegyver>();
            armors = new List<pancel>();
        }

        public void kiir()
        {
            Console.WriteLine("- Fegyverek");
            foreach (var i in weapons)
            {
                Console.WriteLine($"\t'{i.Nev}");
            }


            Console.WriteLine("- Ruházat");
            foreach (var i in armors)
            {
                Console.WriteLine($"\t'{i.Nev}");
            }
        }
    }

    class targy
    {
        public string Nev;
    }

    class pancel : targy
    {
        public void felvesz(jatekos player, pancel pa)
        {
            player.taska.armors.Add(pa);
        }
    }

    class Sisak : pancel
    {
        public Sisak(string nev)
        {
            Nev = nev;
        }
    }

    class fegyver : targy
    {
        public void felvesz(jatekos player, fegyver f)
        {
            player.taska.weapons.Add(f);
        }
    }

    class Kard : fegyver
    {
        public Kard(string nev)
        {
            Nev = nev;
        }
    }
}