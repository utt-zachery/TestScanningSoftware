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
    public class Sectionizer
    {
        public List<Point> studentCondition;
        public List<Point> multipleChoice;

        public List<Point> pairing;
        public List<Point> trueFalse;

        public List<Point> completion;
        public List<Point> subjective;

        public List<Point> sourcePoints;
        
        public List<int> breakers;
        public List<int> lastPoint;

        public Sectionizer(List<Point> sourcePoints, double avRadius)
        {
            this.sourcePoints = sourcePoints;
            this.sourcePoints = this.sourcePoints.OrderBy(o => o.Y).ToList();
            studentCondition = new List<Point>();
            multipleChoice = new List<Point>();
            pairing = new List<Point>();
            trueFalse = new List<Point>();
            completion = new List<Point>();
            subjective = new List<Point>();

            breakers = new List<int>(3);
            breakers.Add(0);
            breakers.Add(0);
            breakers.Add(0);
            lastPoint = new List<int>(3);
            lastPoint.Add(0);
            lastPoint.Add(0);
            lastPoint.Add(0);
            //Start Sectioning off
            Thread thread1 = new Thread(() => startTop(avRadius));
            Thread thread2 = new Thread(() => startBottom(avRadius));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            //Successfully sectioned into int ervals :)
            
            for (int i = 0; i < this.sourcePoints.Count; i++)
            {
                if (i <= lastPoint[0])
                    studentCondition.Add(this.sourcePoints[i]);
                else if (i <= lastPoint[1] && i > lastPoint[0])
                    multipleChoice.Add(this.sourcePoints[i]);
                else if (i <= lastPoint[2] && i > lastPoint[1])
                    pairing.Add(this.sourcePoints[i]);
                else
                    completion.Add(this.sourcePoints[i]);
            }

            this.pairing = this.pairing.OrderBy(o => o.X).ToList();
            this.completion = this.completion.OrderBy(o => o.X).ToList();


            thread1 = new Thread(() => doSectionX(this.pairing, avRadius, 0));
            thread2 = new Thread(() => doSectionX(this.completion, avRadius, 1));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            List<Point> pairingCopy = new List<Point>(this.pairing);
            List<Point> completionCopy = new List<Point>(this.completion);
            this.pairing.Clear();
            this.completion.Clear();
            for (int i = 0; i < pairingCopy.Count; i++)
            {
                if (i <= lastPoint[0])
                    this.pairing.Add(pairingCopy[i]);
                else
                    this.trueFalse.Add(pairingCopy[i]);
            }

            for (int i = 0; i < completionCopy.Count; i++)
            {
                if (i <= lastPoint[1])
                    this.completion.Add(completionCopy[i]);
                else
                    this.subjective.Add(completionCopy[i]);
            }

            //recycle memory
            completionCopy.Clear();
            pairingCopy.Clear();
            this.sourcePoints.Clear();
            this.lastPoint.Clear();
        }
        
        private void startTop(double avRadius)
        {
            grabStudentConditionBorder(avRadius);
            grabPairingBorder(lastPoint[0], avRadius);
        }

        private void startBottom(double avRadius)
        {
            doSectionBottom(2, sourcePoints.Count - 1, avRadius);
        }

        private void doSectionX(List<Point> dataBank, double avRadius, int index)
        {
            int breakerpoint = 0;
            int lastX = dataBank[0].X;
            int breaker = 0;
            int counter = 0;
            bool didBreak = false;

            for (int i = dataBank.Count-1; i >-1; i--)
            {
                if (lastX-dataBank[i].X   > 8 * avRadius)
                {
                    if (didBreak)
                    {
                        counter = 0;
                    }
                    didBreak = true;
                    breaker = (dataBank[i].X + dataBank[i - 1].X) / 2;
                    breakerpoint = i - 1;
                }
                if (counter > 10)
                {
                    break;
                }
                if (didBreak)
                {
                    counter = counter + 1;
                }
                lastX = dataBank[i].X;
            }
            breakers[index] =breaker;
            lastPoint[index]=breakerpoint;
        }

        private void grabStudentConditionBorder(double avRadius)
        {
            int lasty = this.sourcePoints[0].Y;
            int yValueBreaker = 0;
            int iValueofBreaker = 0;
            int counter = 0;
            bool didBreak = false;

            for (int i = 1; i < this.sourcePoints.Count; i++)
            {
                if (this.sourcePoints[i].Y - lasty > 6 * avRadius)
                {
                    if (didBreak)
                    {
                        counter = 0;
                    }
                    didBreak = true;
                    yValueBreaker = (this.sourcePoints[i].Y + this.sourcePoints[i - 1].Y) / 2;
                    iValueofBreaker = i - 1;
                }
                if (counter > 10)
                {
                    break;
                }
                if (didBreak)
                {
                    counter = counter + 1;
                }
                lasty = this.sourcePoints[i].Y;
            }
            breakers[0] = yValueBreaker;
            lastPoint[0] = iValueofBreaker;
        }

        private void grabPairingBorder(int lastBreakerPointIValue, double avRadius)
        {
            int lasty = this.sourcePoints[lastBreakerPointIValue+1].Y;
            int yValueBreaker = 0;
            int iValueofBreaker = 0;
            int counter = 0;
            bool didBreak = false;

            for (int i = lastBreakerPointIValue+2; i < this.sourcePoints.Count; i++)
            {
                if (this.sourcePoints[i].Y - lasty > 4 * avRadius)
                {
                    if (didBreak)
                    {
                        counter = 0;
                    }
                    didBreak = true;
                    yValueBreaker = (this.sourcePoints[i].Y + this.sourcePoints[i - 1].Y) / 2;
                    iValueofBreaker = i-1;
                }
                if (counter > 10)
                {
                    break;
                }
                if (didBreak)
                {
                    counter = counter + 1;
                }
                lasty = this.sourcePoints[i].Y;
            }
            breakers[1]= yValueBreaker;
            lastPoint[1]= iValueofBreaker;
        }

        private void doSectionBottom(int sectionNumber, int firstPointIndex, double avRadius)
        {
            int breakerpoint = 0;
            int lasty = this.sourcePoints[firstPointIndex].Y;
            int breaker = 0;
            int counter = 0;
            bool didBreak = false;

            for (int i = firstPointIndex -1; i >-1; i--)
            {
                if (lasty - this.sourcePoints[i].Y  > 3 * avRadius)
                {
                    if (didBreak)
                    {
                        counter = 0;
                    }
                    didBreak = true;
                    breaker = (this.sourcePoints[i].Y + this.sourcePoints[i - 1].Y) / 2;
                    breakerpoint = i + 1;
                }
                if (counter > 10)
                {
                    break;
                }
                if (didBreak)
                {
                    counter = counter + 1;
                }
                lasty = this.sourcePoints[i].Y;
            }
            lastPoint[2] =breakerpoint;
            breakers[2]= breaker;
        }
    }
}
