using System;
using System.Collections.Generic;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Panzer
{

    public class Vaunu : PhysicsObject
    {
        public int HP;
        public int vaunuID;
        Image leoVaunu = Game.LoadImage("leo");
        Image challengerVaunu = Game.LoadImage("challenger");
        Image amxVaunu = Game.LoadImage("amx");

        public Vaunu(double width, double height, int life, int vaunutunnus) : base(width, height)
        {
            HP = life;
            vaunuID = vaunutunnus;
        }

        public Vaunu Leopard() 
        {
            Vaunu leopard = new Vaunu(100, 200, 3, 0);
            leopard.Image = leoVaunu;
            Add(leopard);
            return leopard;
        }

        public Vaunu Challenger()
        {
            Vaunu challenger = new Vaunu(110, 220, 4, 1);
            challenger.Image = challengerVaunu;
            Add(challenger);
            return challenger;
            
        }

        public Vaunu Amx()
        {
            Vaunu amx = new Vaunu(100, 200, 3, 2);
            amx.Image = amxVaunu;
            Add(amx);
            return amx;
        }
    }
}
