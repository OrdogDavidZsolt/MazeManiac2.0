using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beadandó;

namespace Karakterek
{
    class jatekos
    {
        public int atk;
        public int hp;
        public hatizsak taska;
        public jatekos()
        {
            atk = 100;
            hp = 2000;
            taska = new hatizsak();
        }
    }

    class NPC
    {
        public int hp;
        public int atk;
        public string name;
    }

    class Enemy : NPC
    {
        public void Fight(jatekos player)
        {
            do
            {
                this.hp -= player.atk;
                Console.WriteLine($"Megütöd a {name} -t és {player.atk} -t sebzel");
                player.atk -= this.atk;
                Console.WriteLine($"A {name} megütött és {atk} -t sebzett");
            } while (player.hp > 0 && this.hp > 0);
        }
    }

    class Goblin : Enemy
    {
        public Goblin()
        {
            name = "Goblin";
            atk = 4;
            hp = 12;
        }
    }

    class Boss : Enemy
    {
        public Boss()
        {
            name = "Boss";
            atk = 40;
            hp = 120;
        }
    }
}