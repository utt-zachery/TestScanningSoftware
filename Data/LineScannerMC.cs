using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class LineScannerMC
    {
        public List<AnswerLine> lineY;
        public List<AnswerLine> lineX;
        private List<Point> outliarsX;
        private List<Point> outliarsY;

        private List<Point> mcPoints;
        private double avRadius;

        public LineScannerMC(List<Point> mcPoints, double avRadius)
        {
            this.outliarsX = new List<Point>();
            this.outliarsY = new List<Point>();

            this.mcPoints = mcPoints;
            this.avRadius = avRadius;
            Thread thread1 = new Thread(() => scanLine(true));
            thread1.Start();
            Thread thread2 = new Thread(() => scanLine(false));
            thread2.Start();
            thread1.Join();
            thread2.Join();
            foreach (Point p in outliarsX)
            {
                foreach (AnswerLine a in lineY)
                {
                    a.qPoints.Remove(p);
                }
            }

            foreach (Point p in outliarsY)
            {
                foreach (AnswerLine a in lineX)
                {
                    a.qPoints.Remove(p);
                }
            }
            outliarsX.Clear();
            outliarsY.Clear();
        }

        private void scanLine(bool isX)
        {
            List<Point> pCopy = new List<Point>(this.mcPoints);
            if (isX)
                pCopy = pCopy.OrderBy(o => o.X).ToList();
            else
                pCopy = pCopy.OrderBy(o => o.Y).ToList();
            List<AnswerLine> mclines;
            if (isX)
                mclines = CenterFinder.aggregateLinesX(pCopy, avRadius);
            else
                mclines = CenterFinder.aggregateLinesY(pCopy, avRadius);

            mclines = mclines.OrderBy(o => o.qPoints.Count).ToList();
            
            if (mclines.Count > 12 && isX)
            {
                for (int i = mclines.Count - 13; i > -1; i--)
                {
                    foreach (Point p in mclines[0].qPoints)
                    {
                            this.outliarsX.Add(p);
                       
                    }
                    mclines.RemoveAt(0);
                }
            }

            if (mclines.Count > 10 && !isX)
            {
                for (int i = mclines.Count - 11; i > -1; i--)
                {
                    foreach (Point p in mclines[0].qPoints)
                    {
                            this.outliarsY.Add(p);
                    }
                    mclines.RemoveAt(0);
                }
            }
            if (isX)
                this.lineX = mclines;
            else this.lineY = mclines;
        }
    }
}
