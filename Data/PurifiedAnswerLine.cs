using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class PurifiedAnswerLine
    {
        public List<Check_Point> quickList = new List<Check_Point>();
        public PurifiedAnswerLine(AnswerLine a, int avRadius)
        {
            int lastY = a.qPoints[0].Y;
            int startY = 0;
            a.qPoints = a.qPoints.OrderBy(o => o.Y).ToList();
            for (int i=1; i<a.qPoints.Count;i++)
            {
                if (a.qPoints[i].Y - lastY > avRadius/1.5)
                {
                        quickList.Add(new Check_Point(a.qPoints[i-1].X,a.qPoints[i-1].Y,(i)- startY));
                    
                    startY = i;
                    lastY = a.qPoints[i].Y;
                }
               
            }
        }
    }
}
