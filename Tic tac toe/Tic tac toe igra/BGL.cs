using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pmfst_GameSDK
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        public static List<Sprite> allSprites = new List<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Sprite sprite in allSprites)
            {
                //if (sprite.bmp != null && sprite.show == true)
                if (sprite != null)
                    if (sprite.Show == true)
                    {
                        //lock (sprite.CurrentCostume)
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                    }
            }
            //Ispis(g, bodovi);
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */


        /* ------------ GAME CODE START ------------ */

        /* Game variables */
        //deklarirati varijable koje će se koristiti tijekom cijele igre.
        Polje polje1, polje2, polje3, polje4, polje5, polje6, polje7, polje8, polje9;
        Krizic krizic;
        Kruzic kruzic;
        int brojOznacenih=0;
        string pobjednik="Neriješeno";
        int zivoti = 0;
        /* Initialization */
        private void SetupGame()
        {            
            //1. setup stage
            SetStageTitle("Tic Tac Toe");
            setBackgroundColor(Color.Beige);
            setBackgroundPicture("backgrounds\\space2.png");
            setPictureLayout("stretch");

            //2. add sprites
            StvaranjePolja();
            //ako se dodaju nove slike
            // a) povući sliku u odgovarajući folder u Solution Exploreru
            // b) desni klik na sliku u solution explorer-u
            // c) Properties: Copy to output directory = Copy if newer
            //napomena: ako se koristi lik s više kostima,
            //prepručuje se napraviti png sliku koja već ima prozirnu boju
            //sheep = new Polje("sprites\\sheep01.png", 100, 100);
            //Game.AddSprite(sheep);
            //sheep.SetSize(50);            
            //sheep.RotationStyle = "AllAround";

            //3. scripts that start
            //Game.StartScript(FollowMouse);
            MouseDown += KvadratKlik;
        }        
        
        /* Scripts */


        //private int FollowMouse()
        //{
        //    while (START)
        //    {
        //        sheep.PointToMouse(sensing.Mouse);
        //    }
        //    return 0;
        //}
        void StvaranjePolja()
        {
            polje1 = new Polje("sprites//Kvadrat.jpg", 120, 80);
            Game.AddSprite(polje1);
            polje2 = new Polje("sprites//Kvadrat.jpg", 275, 80);
            Game.AddSprite(polje2);
            polje3 = new Polje("sprites//Kvadrat.jpg", 430, 80);
            Game.AddSprite(polje3);
            polje4 = new Polje("sprites//Kvadrat.jpg", 120, 235);
            Game.AddSprite(polje4);
            polje5 = new Polje("sprites//Kvadrat.jpg", 275, 235);
            Game.AddSprite(polje5);
            polje6 = new Polje("sprites//Kvadrat.jpg", 430, 235);
            Game.AddSprite(polje6);
            polje7 = new Polje("sprites//Kvadrat.jpg", 120, 390);
            Game.AddSprite(polje7);
            polje8 = new Polje("sprites//Kvadrat.jpg", 275, 390);
            Game.AddSprite(polje8);
            polje9 = new Polje("sprites//Kvadrat.jpg", 430, 390);
            Game.AddSprite(polje9);
        }
        void Restart()
        {
            polje1.Oznaka='a';
            polje2.Oznaka = 'a';
            polje3.Oznaka = 'a';
            polje4.Oznaka = 'a';
            polje5.Oznaka = 'a';
            polje6.Oznaka = 'a';
            polje7.Oznaka = 'a';
            polje8.Oznaka = 'a';
            polje9.Oznaka = 'a';
            polje1.Oznaceno = false;
            polje2.Oznaceno = false;
            polje3.Oznaceno = false;
            polje4.Oznaceno = false;
            polje5.Oznaceno = false;
            polje6.Oznaceno = false;
            polje7.Oznaceno = false;
            polje8.Oznaceno = false;
            polje9.Oznaceno = false;
            brojOznacenih = 0;
            allSprites.RemoveAll(sprite => sprite.X > 0);
            StvaranjePolja();
        }
        bool KrajIgre(ref string pobjednik)
        {
            if (polje1.Oznaka == 'x' && polje2.Oznaka == 'x' && polje3.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje4.Oznaka == 'x' && polje5.Oznaka == 'x' && polje6.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje7.Oznaka == 'x' && polje8.Oznaka == 'x' && polje9.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje1.Oznaka == 'x' && polje4.Oznaka == 'x' && polje7.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje2.Oznaka == 'x' && polje5.Oznaka == 'x' && polje8.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje3.Oznaka == 'x' && polje6.Oznaka == 'x' && polje9.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje1.Oznaka == 'x' && polje5.Oznaka == 'x' && polje9.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje3.Oznaka == 'x' && polje5.Oznaka == 'x' && polje7.Oznaka == 'x')
            {
                MessageBox.Show("Pobjednik je igrač!", "Kraj igre");
                pobjednik = "Igrač";
                return true;
            }
            else if (polje1.Oznaka == 'o' && polje2.Oznaka == 'o' && polje3.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje4.Oznaka == 'o' && polje5.Oznaka == 'o' && polje6.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje7.Oznaka == 'o' && polje8.Oznaka == 'o' && polje9.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje1.Oznaka == 'o' && polje4.Oznaka == 'o' && polje7.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje2.Oznaka == 'o' && polje5.Oznaka == 'o' && polje8.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje3.Oznaka == 'o' && polje6.Oznaka == 'o' && polje9.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje1.Oznaka == 'o' && polje5.Oznaka == 'o' && polje9.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else if (polje3.Oznaka == 'o' && polje5.Oznaka == 'o' && polje7.Oznaka == 'o')
            {
                MessageBox.Show("Pobjednik je računalo!", "Kraj igre");
                pobjednik = "Računalo";
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }
        bool ProvjeraOznacenog(int p)
        {
            if (p == 1)
            {
                if (polje1.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 2)
            {
                if (polje2.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 3)
            {
                if (polje3.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 4)
            {
                if (polje4.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 5)
            {
                if (polje5.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 6)
            {
                if (polje6.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 7)
            {
                if (polje7.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 8)
            {
                if (polje8.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (p == 9)
            {
                if (polje9.Oznaceno)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        void OznaciPolje(int p)
        {
            if (p == 1)
            {
                kruzic = new Kruzic("sprites//O.png", polje1.X, polje1.Y);
                polje1.Oznaceno = true;
                polje1.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 2)
            {
                kruzic = new Kruzic("sprites//O.png", polje2.X, polje2.Y);
                polje2.Oznaceno = true;
                polje2.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 3)
            {
                kruzic = new Kruzic("sprites//O.png", polje3.X, polje3.Y);
                polje3.Oznaceno = true;
                polje3.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 4)
            {
                kruzic = new Kruzic("sprites//O.png", polje4.X, polje4.Y);
                polje4.Oznaceno = true;
                polje4.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 5)
            {
                kruzic = new Kruzic("sprites//O.png", polje5.X, polje5.Y);
                polje5.Oznaceno = true;
                polje5.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 6)
            {
                kruzic = new Kruzic("sprites//O.png", polje6.X, polje6.Y);
                polje6.Oznaceno = true;
                polje6.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 7)
            {
                kruzic = new Kruzic("sprites//O.png", polje7.X, polje7.Y);
                polje7.Oznaceno = true;
                polje7.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 8)
            {
                kruzic = new Kruzic("sprites//O.png", polje8.X, polje8.Y);
                polje8.Oznaceno = true;
                polje8.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            else if (p == 9)
            {
                kruzic = new Kruzic("sprites//O.png", polje9.X, polje9.Y);
                polje9.Oznaceno = true;
                polje9.Oznaka = 'o';
                PovecajOznacene(ref brojOznacenih);
            }
            Game.AddSprite(kruzic);
        }
        void PovecajOznacene(ref int brojOznacenih)
        {
            brojOznacenih++;
        }
        void KvadratKlik(object sender, MouseEventArgs e)
        {
            if(polje1.Clicked(sensing.Mouse))
            {
                if (polje1.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje1.X, polje1.Y);
                    Game.AddSprite(krizic);
                    polje1.Oznaceno = true;
                    polje1.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                    if (KrajIgre(ref pobjednik))
                    {
                        Restart();
                        if (pobjednik == "Igrač")
                        {
                            zivoti++;
                        }
                        else if (pobjednik == "Računalo")
                        {
                            zivoti--;
                        }

                        lblzivoti.Text = "Životi: " + zivoti;
                        pobjednik = "Neriješeno";
                    }
                    if ((allSprites.Count-9) > 8 && pobjednik == "Neriješeno")
                    {
                        Restart();
                        MessageBox.Show("Neriješeno!");
                    }
                }
            }
            if (polje2.Clicked(sensing.Mouse))
            {
                if (polje2.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje2.X, polje2.Y);
                    Game.AddSprite(krizic);
                    polje2.Oznaceno = true;
                    polje2.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                }
            }
            if (polje3.Clicked(sensing.Mouse))
            {
                if (polje3.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje3.X, polje3.Y);
                    Game.AddSprite(krizic);
                    polje3.Oznaceno = true;
                    polje3.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                }
            }
            if (polje4.Clicked(sensing.Mouse))
            {
                if (polje4.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje4.X, polje4.Y);
                    Game.AddSprite(krizic);
                    polje4.Oznaceno = true;
                    polje4.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
  
                }
            }
            if (polje5.Clicked(sensing.Mouse))
            {
                if (polje5.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje5.X, polje5.Y);
                    Game.AddSprite(krizic);
                    polje5.Oznaceno = true;
                    polje5.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                
                }
            }
            if (polje6.Clicked(sensing.Mouse))
            {
                if (polje6.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje6.X, polje6.Y);
                    Game.AddSprite(krizic);
                    polje6.Oznaceno = true;
                    polje6.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                    
                }
            }
            if (polje7.Clicked(sensing.Mouse))
            {
                if (polje7.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje7.X, polje7.Y);
                    Game.AddSprite(krizic);
                    polje7.Oznaceno = true;
                    polje7.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                    
                }
            }
            if (polje8.Clicked(sensing.Mouse))
            {
                if (polje8.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje8.X, polje8.Y);
                    Game.AddSprite(krizic);
                    polje8.Oznaceno = true;
                    polje8.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                    
                }
            }
            if (polje9.Clicked(sensing.Mouse))
            {
                if (polje9.Oznaceno == false)
                {
                    krizic = new Krizic("sprites//X.png", polje9.X, polje9.Y);
                    Game.AddSprite(krizic);
                    polje9.Oznaceno = true;
                    polje9.Oznaka = 'x';
                    PovecajOznacene(ref brojOznacenih);
                    if (brojOznacenih < 8)
                    {
                        int p;
                        Random r = new Random();
                        p = r.Next(1, 10);
                        while (!ProvjeraOznacenog(p))
                        {
                            p = r.Next(1, 10);
                        }
                        OznaciPolje(p);
                    }
                        if (KrajIgre(ref pobjednik))
                        {
                            Restart();
                            if (pobjednik == "Igrač")
                            {
                                zivoti++;
                            }
                            else if (pobjednik == "Računalo")
                            {
                                zivoti--;
                            }
                            lblzivoti.Text = "Životi: " + zivoti;
                            pobjednik = "Neriješeno";
                        }
                        if ((allSprites.Count - 9) > 8 && pobjednik == "Neriješeno")
                        {
                            Restart();
                            MessageBox.Show("Neriješeno!");
                        }
                    
                }
            }
        }
        /* ------------ GAME CODE END ------------ */        
        
    }

}
 
