using System;
using System.Collections.Generic;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Panzer_War_Europa
{
    public class Panssarikanuuna : Weapon
    {
        public Panssarikanuuna(double width, double height)
            : base(width, height)
        {
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(3);
        }
        protected override PhysicsObject CreateProjectile()
        {
            Image = Game.LoadImage("omakranaatti");
            AttackSound = Game.LoadSoundEffect("omakanuuna");
            TimeBetweenUse = TimeSpan.FromSeconds(0);
            PhysicsObject beam = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 10));
            beam.Color = Color.Azure;
            return beam;
        }
    }

    public class VihuKanuuna : Weapon
    {
        public VihuKanuuna(double width, double height)
            : base(width, height)
        {
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(3);
        }
        protected override PhysicsObject CreateProjectile()
        {
            Image = Game.LoadImage("vihu_kranaatti");
            AttackSound = Game.LoadSoundEffect("vihu_kanuunanlaukaus");
            TimeBetweenUse = TimeSpan.FromSeconds(0);
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
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(10);
        }
        protected override PhysicsObject CreateProjectile()
        {
            Image = Game.LoadImage("omakranaatti");
            AttackSound = Game.LoadSoundEffect("atgm");
            TimeBetweenUse = TimeSpan.FromSeconds(0);
            PhysicsObject missile = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 10));
            missile.Color = Color.Azure;
            FollowerBrain ohjuksenAivot = new FollowerBrain("vihollinen");
            return missile;
        }
    }

    public class Ydinase : Weapon
    {
        public Ydinase(double width, double height)
            : base(width, height)
        {
            AmmoIgnoresGravity = false;
            Power.DefaultValue = 1000;
            TimeBetweenUse = TimeSpan.FromSeconds(3);
        }
        protected override PhysicsObject CreateProjectile()
        {
            Image = Game.LoadImage("omakranaatti");
            AttackSound = Game.LoadSoundEffect("kanuunanlaukaus");
            TimeBetweenUse = TimeSpan.FromSeconds(0);
            PhysicsObject beam = new PhysicsObject(new RaySegment(Vector.Zero, Vector.UnitX, 10));
            beam.Color = Color.Azure;
            return beam;
        }
    }
}