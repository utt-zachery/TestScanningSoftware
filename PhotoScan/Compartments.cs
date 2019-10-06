using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.PhotoScan
{
    public class Compartments
    {
        public Bitmap coloredImage{ get; }
        public RotatedRect barCode1 { get; }
        public RotatedRect barCode2 { get;  }

        public Compartments(Bitmap coloredImage, RotatedRect barCode1, RotatedRect barCode2)
        {
            this.coloredImage = coloredImage;
            this.barCode1 = barCode1;
            this.barCode2 = barCode2;
        }
    }
}
