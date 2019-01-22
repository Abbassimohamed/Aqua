using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RibbonSimplePad
{
    class Stockart
    {
        public Stockart()
        { }

        private string codeArt;

        public string CodeArt
        {
            get { return codeArt; }
            set { codeArt = value; }
        }
        private string codeSousArt;

        public string CodeSousArt
        {
            get { return codeSousArt; }
            set { codeSousArt = value; }
        }
        private int qtite;

        public int Qtite
        {
            get { return qtite; }
            set { qtite = value; }
        }
    }
}
