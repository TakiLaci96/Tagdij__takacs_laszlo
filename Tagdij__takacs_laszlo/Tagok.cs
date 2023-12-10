﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagdij__takacs_laszlo
{
    internal class Tagok
    {
        public int azon;
        public string nev;
        public int szulev;
        public int irszam;
        public string orsz;

        public Tagok(int azon, string nev, int szulev, int irszam, string orszag)
        {
            this.azon = azon;
            this.nev = nev;
            this.szulev = szulev;
            this.irszam = irszam;
            this.orsz = orszag;
        }

        public override string ToString()
        {
            return nev;
        }
    }
}
