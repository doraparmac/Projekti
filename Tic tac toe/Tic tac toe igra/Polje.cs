using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pmfst_GameSDK
{
    /// <summary>
    /// Primjer izvedene klase. Sprite je postavljena kao abstract.
    /// </summary>
    public class Polje : Sprite
    {
        bool oznaceno;

        public bool Oznaceno
        {
            get { return oznaceno; }
            set { oznaceno = value; }
        }
        char oznaka;

        public char Oznaka
        {
            get { return oznaka; }
            set { oznaka = value; }
        }
        public Polje(string picture, int xcoor, int ycoor)
            : base(picture, xcoor, ycoor)
        {
            Oznaceno = false;
        }
        public bool Clicked(Point p)
        {
            if (p.X > this.X && p.X < this.X + this.Width)
            {
                if (p.Y > this.Y && p.Y < this.Y + this.Heigth)
                    return true;
            }

            return false;
        }
    }
    public class Krizic : Polje

    {
        public Krizic(string putanja, int x, int y)
            : base(putanja, x, y)
        {
        }
    }
    public class Kruzic : Polje
    {
        public Kruzic(string putanja, int x, int y)
            : base(putanja, x, y)
        {
        }
    }
}
