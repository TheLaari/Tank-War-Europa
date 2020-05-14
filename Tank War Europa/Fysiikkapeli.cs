using Jypeli;
using Jypeli.Widgets;



public class Tank_War_Europa : PhysicsGame
{

    int kenttaNro = 1;
    const int RUUDUN_KOKO = 400;
    const double Nopeus = 200;
    Image omaVaunu = LoadImage("leo");
    Image omaTorni = LoadImage("leo_torni");
    Image tausta = LoadImage("grid");
    Image menutausta = LoadImage("PanzerWarTausta2");
    Image vihollisVaunu = LoadImage("punakone");
    EasyHighScore topLista = new EasyHighScore();
    PhysicsObject pelaaja;
    //PhysicsObject omatorni;


    public override void Begin()
    {
        // Kirjoita ohjelmakoodisi tähän
        Alkuvalikko();
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");

    }


    public void AloitaPeli()
    {
        IsPaused = false;
        MediaPlayer.Play("leopardit");
        MediaPlayer.IsRepeating = true;
        pelaaja = LisaaPelaaja();
        //omatorni = LisaaOmaTorni();


        //AxleJoint omaliitos = new AxleJoint(pelaaja, omatorni);
        LisaaNappaimet();
        LuoKentta(kenttaNro);
        Camera.Follow(pelaaja);
        Camera.ZoomFactor = 0.7;
        Camera.StayInLevel = true;
    }

    //TODO
    /*
     -Aseiden lisääminen (panssarikranaatti, hakeutuva ATGM, ydinkärki)
     -Vihujen spämmääminen ja tuhoaminen
     -Pyörivät tornit
     -Kaupungit, jotka pitää tuhota ydinaseilla
     -Pistelaskuri
     -shiftillä toimiva boosti, joka kuluttaa energiaa
     -äänet menuun
    */

    public void Alkuvalikko()
    {
        ClearAll();
        MultiSelectWindow alkuvalikko = new MultiSelectWindow("WILKOMMEN, KOMMANDANT", "Neues Spiel", "Verlassen");
        MediaPlayer.Play("menumusa");
        Level.Background.Image = menutausta;
        MediaPlayer.IsRepeating = true;
        alkuvalikko.Color = Color.AshGray;
        Add(alkuvalikko);
        alkuvalikko.AddItemHandler(0, AloitaPeli);
        //alkuvalikko.AddItemHandler(1, topLista);
        alkuvalikko.AddItemHandler(1, Exit); //vaihdettava 2:ksi kun toplista toimii
        // "Die Beste Panzerkommandanten",
        IsPaused = true;
    }


    public PhysicsObject LisaaPelaaja()
    {
        PhysicsObject pelaaja = new PhysicsObject(100, 200);
        pelaaja.Shape = Shape.Rectangle;
        Vector paikka = new Vector(100, 100);
        pelaaja.Position = paikka;
        pelaaja.Angle = Angle.FromDegrees(-90);
        pelaaja.Image = omaVaunu;
        pelaaja.CollisionIgnoreGroup = 1;
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

    public PhysicsObject LisaaVihollinen()
    {
        PhysicsObject vihollinen = new PhysicsObject(100, 200);
        vihollinen.Shape = Shape.Rectangle;
        Vector paikka = new Vector(100, 100);
        vihollinen.Position = paikka;
        vihollinen.Image = vihollisVaunu;
        Add(vihollinen);
        vihollinen.Tag = "vihollinen";
        return vihollinen;
    }

    void LuoKentta(int kenttaNro)
    {
        //TileMap kentta = TileMap.FromLevelAsset(kenttaNro.ToString());
        //kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
        kenttaNro++;
        Level.Size = new Vector(1920, 10000);
        Level.CreateBorders();
        Level.Background.Image = tausta;
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

        //Keyboard.Listen(Key.Right, ButtonState.Down,
        //  Pyorita, "Turret Right", omatorni, 1);
        //Keyboard.Listen(Key.Left, ButtonState.Down,
        //  Pyorita, "Turret Right", omatorni, -1);

        PhoneBackButton.Listen(ConfirmExit, "Beenden Sie sofort");
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

    //Implementoin jos jaksan
    void Pyorita(PhysicsObject pelaaja, int torniNopeus)
    {
        pelaaja.AngularVelocity = torniNopeus;
        pelaaja.LinearDamping = 0.8;
    }

    void PelaajaKuolema()
    {
        pelaaja.Destroy();
        //topLista.EnterAndShow(pisteLaskuri.Value); // Lisää pistelaskuri
        //topLista.HighScoreWindow.Closed += AloitaPeli;
    }

    //private void topLista()
    //{
    //    topLista.Show();
    //    return;
    //}

}
