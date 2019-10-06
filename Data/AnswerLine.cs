using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class AnswerLine
    {
        public List<Point> qPoints;
        public List<int> sumX = new List<int>();
        public List<int> sumY = new List<int>();
        private bool isX;

        public AnswerLine(int x, int y, bool isX)
        {
            this.isX = isX;
            this.qPoints = new List<Point>();
            qPoints.Add(new Point(x,y));
             if (isX) this.sumX.Add(x);
             else this.sumY.Add(y);
        }

        public int grabAverage()
        {

            if (isX)
            {
                sumX.Sort();
                return sumX[sumX.Count / 2];
            }
            else
            {
                sumY.Sort();
                return sumY[sumY.Count / 2];
            }
        }
        public void addPoint(Point p)
        {
            qPoints.Add(p);
            if (isX) this.sumX.Add(p.X);
            else this.sumY.Add(p.Y);
        }
    }
}
