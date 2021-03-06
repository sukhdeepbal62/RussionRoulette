using System;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace RussionRoulette
{
    public partial class RouletteForm : Form
    {
        //Sounds
        private readonly SoundPlayer _dryGunShot;
        private readonly SoundPlayer _load;
        private readonly SoundPlayer _shoot;
        private readonly SoundPlayer _spin;

        private readonly ClassGame MyClassRoulette = new ClassGame();

        //     Random rand = new Random();
        public RouletteForm()
        {
            _dryGunShot = new SoundPlayer(Resource1.drygunshot);
            _load = new SoundPlayer(Resource1.load);
            _shoot = new SoundPlayer(Resource1.gunshot);
            _spin = new SoundPlayer(Resource1.spin);
            InitializeComponent();
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            lblBulletLocation.Text = MyClassRoulette.SecretChamberID.ToString();
            lblCurrentID.Text = MyClassRoulette.CurrentChamberID.ToString();
            lblGame.Text = MyClassRoulette.TotalGamePlayed.ToString();
            lblWin.Text = MyClassRoulette.Win.ToString();
            lblLose.Text = MyClassRoulette.Lose.ToString();
            lblNoOfAway.Text = "You have " + MyClassRoulette.AwayCount + " away shots left.";
        }

        private void RouletteForm_Load(object sender, EventArgs e)
        {
            btnLoad.Enabled = true; // enabling load function
            btnSpin.Enabled = false;
            btnShoot.Enabled = false;
            btnNoOfAway.Enabled = false;
            btnNew.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MyClassRoulette.NewGame();
            RefreshScreen();
            btnLoad.Enabled = true;
            btnSpin.Enabled = false; 
            btnShoot.Enabled = false;
            btnNoOfAway.Enabled = false;
            btnNew.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            RefreshScreen();
            _load.Play(); // for playing the sound for load buttonm
            btnLoad.Enabled = false;
            btnSpin.Enabled = true; // Enabling spin function
            btnShoot.Enabled = false;
            btnNoOfAway.Enabled = false;
            var mygame = Assembly.GetExecutingAssembly();
            var myst = mygame.GetManifestResourceStream(
                "RussianRoulette.Resource1.load1"); // Adding image in load function
            imgBox.Image = Resource1.load1;
            RefreshScreen();
        }

        private void btnSpin_Click(object sender, EventArgs e)
        {
            MyClassRoulette.NewGame();
            _spin.Play();
            imgBox.Visible = true;
            btnLoad.Enabled = false; // Disable the spin button
            btnSpin.Enabled = false; // Disable the spin button
            btnShoot.Enabled = true; // Enabling shoot button
            btnNoOfAway.Enabled = true;
            var mygame = Assembly.GetExecutingAssembly();
            var myst = mygame.GetManifestResourceStream("RussianRoulette.Resource1.spin1");
            imgBox.Image = Resource1.spin1;
            //     MessageBox.Show("Chamber" + MyClassRoulette.CurrentChamberID);

            RefreshScreen();
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            if (MyClassRoulette.bulletShot())
            {
                _shoot.Play(); // This sound will play on the click of shoot button
                var mygame = Assembly.GetExecutingAssembly();
                var myst =
                    mygame.GetManifestResourceStream(
                        "RussionRoulette.Resources1.shoot1"); // This image will show on the click of shoot button
                imgBox.Image = Resource1.shoot1;
                MessageBox.Show("You just blew your brains.!\nYou Lose!\nTry Again.");
                MyClassRoulette.YouLose();
                MyClassRoulette.NewGame();
                btnLoad.Enabled = false;
                btnSpin.Enabled = false;
                btnShoot.Enabled = false;
                btnNoOfAway.Enabled = false;
                btnNew.Enabled = true;
            }
            else
            {
                {
                    MyClassRoulette.Next();
                    _dryGunShot.Play();
                    var mygame = Assembly.GetExecutingAssembly();
                    var myst =
                        mygame.GetManifestResourceStream(
                            "RussionRoulette.Resources1.NotShoot"); // This image will show on the click of shoot button
                    imgBox.Image = Resource1.NotShoot;
                }

                if (MyClassRoulette.CurrentChamberID == MyClassRoulette.NoOfChamber)
                {
                    _dryGunShot.Play(); // This sound will play on the click of shoot button
                    var mygame = Assembly.GetExecutingAssembly();
                    var myst = mygame.GetManifestResourceStream(
                        "RussionRoulette.Resources1.NotShoot"); // This image will show on the click of shoot button
                    imgBox.Image = Resource1.NotShoot;
                    MyClassRoulette.YouLose();
                    MyClassRoulette.NewGame();
                    MessageBox.Show(
                        $"You Have Shot All {MyClassRoulette.NoOfChamber - 1} Chambers & Found Bullet In The {MyClassRoulette.SecretChamberID}.\nYou Won!!!");
                    MyClassRoulette.YouWon();
                    MyClassRoulette.NewGame();
                    btnLoad.Enabled = false;
                    btnSpin.Enabled = false;
                    btnShoot.Enabled = false;
                    btnNoOfAway.Enabled = false;
                    btnNew.Enabled = true;
                }
            }

            RefreshScreen();
        }

        private void btnNoOfAway_Click(object sender, EventArgs e)
        {
            if (MyClassRoulette.bulletShot())
            {
                _shoot.Play(); // This sound will play on the click of awayshoot button
                var mygame = Assembly.GetExecutingAssembly();
                var myst = mygame.GetManifestResourceStream(
                    "RussionRoulette.Resources1.ShootAway"); // This image will show on the click of awayshoot button
                imgBox.Image = Resource1.ShotAway;
                MessageBox.Show("You Just Shot The Bullet Away!\nYou Won!");
                MyClassRoulette.YouWon();
                MyClassRoulette.NewGame();
                btnLoad.Enabled = false;
                btnSpin.Enabled = false;
                btnShoot.Enabled = false;
                btnNoOfAway.Enabled = false;
                btnNew.Enabled = true;
            }
            else
            {
                _dryGunShot.Play();
                MyClassRoulette.AwayCount--;
                MyClassRoulette.Next();
                if (MyClassRoulette.AwayCount == 0)
                {
                    _dryGunShot.Play(); // This sound will play on the click of awayshoot button
                    var mygame = Assembly.GetExecutingAssembly();
                    var myst = mygame.GetManifestResourceStream(
                        "RussionRoulette.Resources1.DryAwayShot"); // This image will show on the click of awayshoot button
                    imgBox.Image = Resource1.DryAwayShot;
                    MessageBox.Show("Used all away shots & You didn't find the bullet.\nYou Lost!");
                    btnLoad.Enabled = false;
                    btnSpin.Enabled = false;
                    btnShoot.Enabled = false;
                    btnNoOfAway.Enabled = false;
                    MyClassRoulette.YouLose();
                    MyClassRoulette.NewGame();
                    btnNew.Enabled = true;
                }
            }

            RefreshScreen();
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();

            Application.Exit();
        }

      
    }
}