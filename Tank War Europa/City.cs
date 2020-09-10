using System;
using System.Collections.Generic;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.GameObjects;


namespace Panzer
{
    public class City : PhysicsObject
    {

        public City(double width, double height, Shape shape, double x = 0, double y = 0)
        : base(width, height, shape, x, y)
        {

        }

        public void Kaupunki()
        {
            City kaupunki = new City(100, 100, Shape.Rectangle, 0, 10);
        }
    }
}
