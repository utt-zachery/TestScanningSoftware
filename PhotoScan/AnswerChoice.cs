using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.PhotoScan
{
    public class AnswerChoice
    {
        public Color displayColor;
        public Point center;
        public double avRadius;

        public AnswerChoice(Color displayColor, Point center, double avRadius)
        {
            this.displayColor = displayColor;
            this.center = center;
            this.avRadius = avRadius;
        }
    }
}
