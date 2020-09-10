﻿using System;
using System.Collections.Generic;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using Panzer;

namespace Panzer
{
    public class Tank_War_Europa : PhysicsGame
    {
        int kenttaNro = 1;
        const int RUUDUN_KOKO = 400;
        const double Nopeus = 200;
        Image leoVaunu = LoadImage("leo");
        Image challengerVaunu = LoadImage("challenger");
        Image amxVaunu = LoadImage("amx");
        Image omaTorni = LoadImage("leo_torni");
        Image tausta = LoadImage("grid");
        Image menutausta = LoadImage("PanzerWarTausta2");
        Image vihollisVaunu = LoadImage("punakone");
        SoundEffect nappi = LoadSoundEffect("nappi");
        Vaunu vihollinen;

        Ydinase pelaajanNuke;
        Panssarikanuuna pelaajanTykki;
        Ohjus pelaajanOhjus;

        ScoreList topLista = new ScoreList(10, false, 0);
        string[] johtajat = new string[10];

        Vaunu pelaaja;
        //PhysicsObject omatorni;


        public override void Begin()
        {

            // Kirjoita ohjelmakoodisi tähän
            Alkuvalikko();
        }

        private void AmmuTykilla(Panssarikanuuna tykki)
        {
            PhysicsObject kranu = tykki.Shoot();
        }

        private void AmmuOhjus(Ohjus atgm)
        {
            PhysicsObject raketti = atgm.Shoot();
        }

        private void AmmuYdinpommi(Ydinase nuke)
        {
            PhysicsObject joukkotuhoase = nuke.Shoot();
        }

        private void AmmuVihuaseella(VihuKanuuna vihutykki)
        {
            PhysicsObject vihollisase = vihutykki.Shoot();
        }

        public void LisaaAseet()
        {
            pelaajanOhjus = new Ohjus(0, 0);
            pelaajanTykki = new Panssarikanuuna(0, 0);
            pelaajanNuke = new Ydinase(0, 0);
            pelaaja.Add(pelaajanOhjus);
            pelaaja.Add(pelaajanTykki);
            pelaaja.Add(pelaajanNuke);
        }

        public void AloitaLeopard()
        {
            nappi.Play();
            IsPaused = false;
            MediaPlayer.Play("leopardit");
            MediaPlayer.IsRepeating = true;
            pelaaja = LisaaLeopard();
            LisaaAseet();
            pelaajanOhjus = new Ohjus(0, 0);
            pelaajanTykki = new Panssarikanuuna(0, 0);
            pelaajanNuke = new Ydinase(0, 0);
            pelaaja.Add(pelaajanOhjus);
            pelaaja.Add(pelaajanTykki);
            pelaaja.Add(pelaajanNuke);
            //omatorni = LisaaOmaTorni();

            //AxleJoint omaliitos = new AxleJoint(pelaaja, omatorni);
            LisaaNappaimet();
            LuoKentta(kenttaNro);
            Camera.Follow(pelaaja);
            Camera.ZoomFactor = 0.7;
            Camera.StayInLevel = true;
        }

        public void AloitaChallenger()
        {
            nappi.Play();
            IsPaused = false;
            MediaPlayer.Play("leopardit");
            MediaPlayer.IsRepeating = true;
            pelaaja = LisaaChallenger();
            LisaaAseet();
            //omatorni = LisaaOmaTorni();


            //AxleJoint omaliitos = new AxleJoint(pelaaja, omatorni);
            LisaaNappaimet();
            LuoKentta(kenttaNro);
            Camera.Follow(pelaaja);
            Camera.ZoomFactor = 0.7;
            Camera.StayInLevel = true;
        }

        public void AloitaAmx()
        {
            nappi.Play();
            IsPaused = false;
            MediaPlayer.Play("leopardit");
            MediaPlayer.IsRepeating = true;
            pelaaja = LisaaAmx();
            LisaaAseet();
            //omatorni = LisaaOmaTorni();
            //AxleJoint omaliitos = new AxleJoint(pelaaja, omatorni);
            LisaaNappaimet();
            LuoKentta(kenttaNro);
            Camera.Follow(pelaaja);
            Camera.ZoomFactor = 0.3;
            Camera.StayInLevel = true;
        }

        //TODO
        /*
         -Aseiden lisääminen (panssarikranaatti, hakeutuva ATGM, ydinkärki kaupunkien tuhoamiseen) TEHTY OSITTAIN
         -Ydinräjähdys
         -Vihujen tuhoaminen
         -Pyörivät tornit TEEN JOS EHDIN
         -Kaupungit, jotka pitää tuhota ydinaseella
         -Pistelaskuri
         -shiftillä toimiva boosti, joka kuluttaa energiaa
         -äänet menuun TEHTY
         -vaunuluokat (raskas, kevyt, välimalli) TEHTY, PITÄÄ REFAKTOROIDA
        */

        public void Alkuvalikko()
        {
            ClearAll();
            MultiSelectWindow alkuvalikko = new MultiSelectWindow("WILKOMMEN, KOMMANDANT", "Neue Schlacht", "Verlassen");
            MediaPlayer.Play("menumusa");
            MediaPlayer.IsRepeating = true;
            Level.Background.Image = menutausta;
            alkuvalikko.Color = Color.AshGray;
            Add(alkuvalikko);
            alkuvalikko.AddItemHandler(0, Vaunuvalikko);
            //alkuvalikko.AddItemHandler(1, topLista);
            alkuvalikko.AddItemHandler(1, Exit); //vaihdettava 2:ksi kun toplista toimii
                                                 // "Die Beste Panzerkommandanten",
            IsPaused = true;
        }

        public void Vaunuvalikko()
        {
            nappi.Play();
            MultiSelectWindow vaunuvalikko = new MultiSelectWindow("Wählen Sie Ihren Panzer", "Leopard 2", "Challenger 1", "AMX-30");
            Level.Background.Image = menutausta;
            vaunuvalikko.Color = Color.AshGray;
            Add(vaunuvalikko);
            vaunuvalikko.AddItemHandler(0, AloitaLeopard);
            vaunuvalikko.AddItemHandler(1, AloitaChallenger);
            vaunuvalikko.AddItemHandler(2, AloitaAmx);
            IsPaused = true;
        }


        public Vaunu LisaaLeopard()
        {
            Vaunu pelaaja = new Vaunu(100, 200, 3, 0);
            pelaaja.Shape = Shape.Rectangle;
            Vector paikka = new Vector(800, 10);
            pelaaja.Position = paikka;
            pelaaja.Angle = Angle.FromDegrees(-90);
            pelaaja.Image = leoVaunu;
            pelaaja.CollisionIgnoreGroup = 1;
            pelaaja.Tag = "pelaaja";
            //pelaaja.Mass = 65;
            Add(pelaaja);
            return pelaaja;
        }

        public Vaunu LisaaChallenger()
        {
            Vaunu pelaaja = new Vaunu(110, 220, 4, 1);
            pelaaja.Shape = Shape.Rectangle;
            Vector paikka = new Vector(800, 100);
            pelaaja.Position = paikka;
            pelaaja.Angle = Angle.FromDegrees(-90);
            pelaaja.Image = challengerVaunu;
            pelaaja.CollisionIgnoreGroup = 1;
            pelaaja.Tag = "pelaaja";
            //pelaaja.Mass = 65;
            Add(pelaaja);
            return pelaaja;
        }


        public Vaunu LisaaAmx()
        {
            Vaunu pelaaja = new Vaunu(95, 190, 2, 2);
            pelaaja.Shape = Shape.Rectangle;
            Vector paikka = new Vector(800, 10);
            pelaaja.Position = paikka;
            pelaaja.Angle = Angle.FromDegrees(-90);
            pelaaja.Image = amxVaunu;
            pelaaja.CollisionIgnoreGroup = 1;
            pelaaja.Tag = "pelaaja";
            //pelaaja.Mass = 65;
            Add(pelaaja);
            return pelaaja;
        }



        //public PhysicsObject LisaaOmaTorni()
        //{
        //    PhysicsObject torni = new PhysicsObject(100, 200);
        //    torni.Shape = Shape.Rectangle;
        //    Vector paikka = new Vector(100, 110);
        //    torni.Position = paikka;
        //    torni.Image = omaTorni;
        //    omatorni.CollisionIgnoreGroup = 1;
        //    Add(torni, 2);
        //    return torni;
        //}

        public Vaunu LisaaVihollinen(int vihunumero)
        {
            Vaunu vihollinen = new Vaunu(100, 200, 1, 99);
            vihollinen.Shape = Shape.Rectangle;
            Random randomY = new Random();
            Random randomX = new Random();
            double yluku = randomY.NextDouble() * 9999;
            double xluku = randomX.NextDouble() * 1920;
            Vector paikka = new Vector(xluku, yluku);
            VihuKanuuna t55Ase = new VihuKanuuna(0, 0);
            vihollinen.Position = paikka;
            vihollinen.Image = vihollisVaunu;
            vihollinen.CollisionIgnoreGroup = 2;
            vihollinen.Add(t55Ase);
            vihollinen.Tag = "vihollinen";
            FollowerBrain vihuaivot = new FollowerBrain(pelaaja);
            vihollinen.Brain = vihuaivot;
            vihuaivot.DistanceClose = 500;
            vihuaivot.StopWhenTargetClose = true;
            vihuaivot.DistanceToTarget.AddTrigger(1000, TriggerDirection.Irrelevant, Huuda);
            Add(vihollinen);
            return vihollinen;
        }


        void Huuda()
        {
            //jottei pelaajan korvat särkyisi, soitetaan huutoa vähän harvemmin
            DateTime foo = DateTime.UtcNow; //https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds(); // ^
            if (unixTime % 9 == 0)
            {
                SoundEffect huuto1 = LoadSoundEffect("vihuhuuto1");
                huuto1.Play();
            }
            /*if (unixTime % 4 == 0)
            {
                SoundEffect huuto2 = LoadSoundEffect("vihuhuuto2");
                huuto2.Play();
            }
            if (unixTime % 6 == 0)
            {
                SoundEffect huuto3 = LoadSoundEffect("vihuhuuto3");
                huuto3.Play();
            }*/

        }

        void LuoKentta(int kenttaNro)
        {
            //TileMap kentta = TileMap.FromLevelAsset(kenttaNro.ToString());
            //kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
            kenttaNro++;
            Level.Size = new Vector(1920, 10000);
            Level.CreateBorders();
            Level.Background.Image = tausta;
            List<int> vihut = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                vihut.Add(i);
            }
            for (int i = 0; i < 9; i++)
            {
                LisaaVihollinen(vihut[i]);
            }
            LisaaKaupunki();
            
        }

        public City LisaaKaupunki()
        {
            City kaupunki = new City(100, 100, Shape.Rectangle, 750, 8000);
            Random random = new Random();
            Add(kaupunki);
            return kaupunki;
        }

        void LisaaNappaimet()
        {
            Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Anweisungen anzeigen");
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, Alkuvalikko, "Zurück zum Hauptmenü");

            Keyboard.Listen(Key.S, ButtonState.Down,
            LiikutaPelaajaa, "Vorwärts", pelaaja, -700.0);
            Keyboard.Listen(Key.D, ButtonState.Down,
              KaannaPelaajaa, "Recht", pelaaja, -4.0);
            Keyboard.Listen(Key.W, ButtonState.Down,
              LiikutaPelaajaa, "Zurücknehmen", pelaaja, 700.0);
            Keyboard.Listen(Key.A, ButtonState.Down,
              KaannaPelaajaa, "Linke", pelaaja, 4.0);
            Keyboard.Listen(Key.G, ButtonState.Down, AmmuTykilla, "Kampfwagenkanone", pelaajanTykki);
            Keyboard.Listen(Key.Space, ButtonState.Down, AmmuOhjus, "Panzerabwehrlenkrakete", pelaajanOhjus);
            Keyboard.Listen(Key.F, ButtonState.Down, AmmuYdinpommi, "Panzerabwehrlenkrakete", pelaajanNuke);


            //Keyboard.Listen(Key.Right, ButtonState.Down,
            //  Pyorita, "Turret Right", omatorni, 1);
            //Keyboard.Listen(Key.Left, ButtonState.Down,
            //  Pyorita, "Turret Right", omatorni, -1);

            PhoneBackButton.Listen(ConfirmExit, "Beenden Sie sofort");
        }

        void VaihdaAse()
        {
            //Keyboard.Listen(Key.F, ButtonState.Down, AmmuTykilla, "Kampfwagenkanone", pelaajanTykki);
        }

        void LiikutaPelaajaa(PhysicsObject pelihahmo, double nopeus)
        {
            Vector pelaajanSuunta = Vector.FromLengthAndAngle(nopeus, (pelihahmo.Angle - Angle.FromDegrees(-90)));

            pelihahmo.Push(pelaajanSuunta);
            pelihahmo.LinearDamping = 0.9;
            pelihahmo.MaxVelocity = 450;
        }

        void KaannaPelaajaa(PhysicsObject vaunu, double nopeus)
        {
            vaunu.AngularVelocity = nopeus;
            vaunu.AngularDamping = 0.7;
        }

        //Implementoin pyörivät tornit jos ehdin
        void Pyorita(PhysicsObject pelaaja, int torniNopeus)
        {
            pelaaja.AngularVelocity = torniNopeus;
            pelaaja.LinearDamping = 0.8;
        }

        void AmmusOsui(Panssarikanuuna ammus, Vaunu kohde)
        {
            ammus.Destroy();
            kohde.HP--;
            if (kohde.HP == 0)
            {
                kohde.Destroy();
                if (kohde.Tag == "pelaaja")
                {
                    PelaajaKuolema();//vaihda PelaajaKuolema()
                }
                if (kohde.Tag == "vihollinen")
                {
                    kohde.Destroy();
                }
            }
        }

        void NukeOsui(Ydinase ammus, City kohde)
        {
            ammus.Destroy();
            kohde.Destroy();
        }

        void PelaajaKuolema()
        {
            pelaaja.Destroy();
            ClearAll();
            Alkuvalikko();
            //topLista.EnterAndShow(pisteLaskuri.Value); // Lisää pistelaskuri
            //topLista.HighScoreWindow.Closed += AloitaLeopard;
        }


    }
}
