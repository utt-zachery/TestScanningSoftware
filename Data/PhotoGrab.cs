using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class PhotoGrab
    {

        public static void detectLines(Bitmap input)
        {
            Bitmap stretched = new Bitmap(input, (int)(input.Width), (int)(input.Height));
            Image<Gray, Byte> grayImage = new Image<Bgr, byte>(stretched).Convert<Gray, Byte>();
            CvInvoke.Threshold(grayImage, grayImage, 120, 1000, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

            grayImage.ToBitmap().Save("32.png");
        }

    }
}
