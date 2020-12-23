using System;
using System.Collections.Generic;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using Jypeli.Effects;

namespace Panzer
{
    public class Panssarikanuuna : PlasmaCannon
    {
        public Panssarikanuuna(double width, double height)
            : base(width, height)
        {
            CreateProjectile();
            //korjaa
        }

        protected override PhysicsObject CreateProjectile()
        {
            TimeBetweenUse = TimeSpan.FromSeconds(3);
            PhysicsObject apfsdp = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 200));
            apfsdp.Color = Color.Azure;
            apfsdp.Image = Game.LoadImage("omakranaatti");
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(2);
            Angle = Angle.FromDegrees(90);
            AttackSound = Game.LoadSoundEffect("omakanuuna");
            Add(apfsdp);
            return apfsdp;
        }
    }


    public class VihuKanuuna : Weapon
    {
        public VihuKanuuna(double width, double height)
            : base(width, height)
        {
            CreateProjectile();
        }

        protected override PhysicsObject CreateProjectile()
        {
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(3);
            Angle = Angle.FromDegrees(90);
            Image = Game.LoadImage("omakranaatti");
            AttackSound = Game.LoadSoundEffect("vihu_kanuunanlaukaus");
            TimeBetweenUse = TimeSpan.FromSeconds(3);
            PhysicsObject beam = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 10));
            beam.Color = Color.Red;
            return beam;
        }
    }

    public class Ohjus : Weapon
    {
        public Ohjus (double width, double height)
            : base(width, height)
        {
            CreateProjectile();
        }

        protected override PhysicsObject CreateProjectile()
        {
            Image = Game.LoadImage("atgm");
            AttackSound = Game.LoadSoundEffect("atgm");
            TimeBetweenUse = TimeSpan.FromSeconds(10);
            PhysicsObject missile = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 10));
            missile.Color = Color.Azure;
            FollowerBrain ohjuksenAivot = new FollowerBrain();
            
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 100;
            Angle = Angle.FromDegrees(90);
            return missile;
        }
    }

    public class Ydinase : Weapon
    {
        public Ydinase(double width, double height)
            : base(width, height)
        {
            CreateProjectile();
            
        }

        protected override PhysicsObject CreateProjectile()
        {

            TimeBetweenUse = TimeSpan.FromSeconds(30);
            PhysicsObject missile = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 50));
            missile.Color = Color.Azure;
            missile.Image = Game.LoadImage("atgm");
            AmmoIgnoresGravity = true;
            Power.DefaultValue = 100;
            TimeBetweenUse = TimeSpan.FromSeconds(30);
            Angle = Angle.FromDegrees(90);
            AttackSound = Game.LoadSoundEffect("kanuunanlaukaus");
            return missile;
        }
    }
}