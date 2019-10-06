using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    class CenterFinder
    {

        public static int leftCornerX(int i, List<Point> inputData, int found, int avRadius)
        {
            //sorted by x of course
            if (found > 5)
                return inputData[i].X;

            if (inputData[i + 1 + found].X - inputData[i + found].X < 0.05 * avRadius)
                return leftCornerX(i, inputData, found + 1, avRadius);

            else
                return leftCornerX(i + 1, inputData, 0, avRadius);
        }

        public static Point grabLeftCorner(int x, List<Point> inputData, int avRadius)
        {
            int changeX = x;
            int iterator = 0;
            Point maxPoint = new Point(0, Int32.MaxValue);
            for (int i = 0; i < inputData.Count; i++)
            {
                if (inputData[i].X > x)
                {
                    if (inputData[i].X - changeX < 0.05 * avRadius)
                    {
                        changeX = inputData[i].X;
                        if (maxPoint.Y > inputData[i].Y)
                        {
                            maxPoint = inputData[i];
                            iterator = i;
                        }
                    }
                    if (inputData[i].X - changeX >= 0.05 * avRadius)
                    {
                        return maxPoint;
                    }
                }
            }
            return maxPoint;
        }
        public static List<AnswerLine> aggregateLinesX(List<Point> inputArray, double avRadius)
        {
            List<AnswerLine> toReturn = new List<AnswerLine>();
            toReturn.Add(new AnswerLine(inputArray[0].X, inputArray[0].Y, true));
            int lastX = inputArray[0].X;
            for (int i = 1; i < inputArray.Count; i++)
            {
                if (inputArray[i].X - lastX < avRadius )
                {
                    toReturn[toReturn.Count - 1].addPoint(inputArray[i]);
                    lastX = inputArray[i].X;

                }
                else
                {
                    toReturn.Add(new AnswerLine(inputArray[i].X, inputArray[i].Y, true));
                    lastX = inputArray[i].X;
                }

            }
            return toReturn;
        }

        public static List<AnswerLine> aggregateLinesY(List<Point> inputArray, double avRadius)
        {
            List<AnswerLine> toReturn = new List<AnswerLine>();
            toReturn.Add(new AnswerLine(inputArray[0].X, inputArray[0].Y, false));
            int lastY = inputArray[0].Y;

            double quickLimit = avRadius * 0.5;
            for (int i = 1; i < inputArray.Count; i++)
            {
                if (inputArray[i].Y - lastY < quickLimit)
                {
                    toReturn[toReturn.Count - 1].addPoint(inputArray[i]);
                    lastY = inputArray[i].Y;

                }
                else
                {
                    toReturn.Add(new AnswerLine(inputArray[i].X, inputArray[i].Y, false));
                    lastY = inputArray[i].Y;

                }

            }
            return toReturn;
        }
    }
}
