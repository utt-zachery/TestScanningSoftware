using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test_Scanning_Software.Data;
using Test_Scanning_Software.PhotoScan;

namespace Test_Scanning_Software.Exam_Sections
{
    public class Completion : ExamSection
    {

        public List<int[]> colorArray = new List<int[]>(5);
        public List<int[]> scores = new List<int[]>(5);
        public List<int[]> scoreRow = new List<int[]>(5);

        public List<CircleF[]> centerCircles = new List<CircleF[]>(10);



        private ExamSpecifics exam;

        public Completion(List<AnswerLine> xLines, List<AnswerLine> yLines, Bitmap inputImage, ExamSpecifics exam, double avRadius) : base(xLines, yLines, inputImage, exam, avRadius)
        {
            scores.Add(null);
            scores.Add(null);
            scores.Add(null);
            scores.Add(null);
            scores.Add(null);
            

            scoreRow.Add(null);
            scoreRow.Add(null);
            scoreRow.Add(null);
            scoreRow.Add(null);
            scoreRow.Add(null);
            

            colorArray.Add(null);
            colorArray.Add(null);
            colorArray.Add(null);
            colorArray.Add(null);
            colorArray.Add(null);
            

            centerCircles.Add(null);
            centerCircles.Add(null);
            centerCircles.Add(null);
            centerCircles.Add(null);
            centerCircles.Add(null);
           

            this.exam = exam;

        }


        public override void scoreSection(Bitmap original)
        {
            int newRadius = (int)(1.5 * base.avRadius);
            int halfRadius = newRadius / 2;
            List<Thread> startedThreads = new List<Thread>();

            for (int q = 0; q < base.yLines.Count; q++)
            {
                Bitmap fullCopy = new Bitmap(original.Width, original.Height);
                Graphics g = Graphics.FromImage(fullCopy);
                g.DrawImage(original, new Point(0, 0));
                g.Dispose();
                AnswerLine a = base.yLines[q];
                Int32 meGo = q;
                Thread thread1 = new Thread(() => colorBubbles(a, fullCopy, meGo));
                startedThreads.Add(thread1);
                thread1.Start();
            }

            foreach (Thread t in startedThreads)
            {
                if (t.IsAlive)
                    t.Join();
            }
            int row = 0;
            foreach (int[] r in colorArray)
            {
                int[] answerColumn = new int[2];
                int[] answerLocation = new int[2];
                //column 1
                bool didAnswer = false;
                int answer1 = -1; ;
                if (Math.Abs(colorArray[row][0] - colorArray[row][1]) > 70)
                {
                    didAnswer = true;
                }
               
                if (didAnswer)
                {
                    answer1 = returnLargest(colorArray[row][0], colorArray[row][1]);
                }
                else
                {
                    answerColumn[0] = -1;
                    answerLocation[0] = -1;
                }
                int answer2 = -1; ;
                if (Math.Abs(colorArray[row][2] - colorArray[row][3]) > 70)
                {
                    didAnswer = true;
                }
               
                if (didAnswer)
                {
                    answer2 = returnLargest(colorArray[row][2], colorArray[row][3]);
                }
                else
                {
                    answerColumn[1] = -1;
                    answerLocation[1] = -1;
                }
                
                //score section
                if (answer1 != -1)
                {
                    if (base.test.compAnswers.Length > row)
                    {
                        answerLocation[0] = answer1;
                        if (base.test.compAnswers[row] == returnAnswer(answer1))
                        {
                            base.score = base.score + 1;
                            answerColumn[0] = 2;
                        }
                        else
                            answerColumn[0] = 1;
                    }
                }
                else
                {
                    if (base.test.compAnswers.Length > row)
                        answerLocation[0] = 0;
                }
                if (answer2 != -1)
                {
                    if (base.test.compAnswers.Length > (row + 5))
                    {
                        answerLocation[1] = answer2+2;
                        if (base.test.compAnswers[row + 5] == returnAnswer(answer2))
                        {
                            base.score = base.score + 1;
                            answerColumn[1] = 2;
                        }
                        else
                            answerColumn[1] = 1;

                    }
                }
                else
                {
                    if (base.test.compAnswers.Length > row + 5)
                        answerLocation[1] = 0;
                }
                
                this.scores[row] = (answerColumn);
                this.scoreRow[row] = (answerLocation);
                row = row + 1;
            }
        }

        private char returnAnswer(int answerCode)
        {
            if (answerCode == -1)
                return 'N';
            if (answerCode == 0)
                return '0';
            if (answerCode == 1)
                return '1';
            return ' ';
        }
        

        private void colorBubbles(AnswerLine a, Bitmap source, int row)
        {
            int i = 0;
            float quickRadius = (float)(1.25 * base.avRadius);
            int halfRadius = (int)(quickRadius / 2);
            CircleF[] bubbles = new CircleF[12];
            int[] pixels = new int[12];

            foreach (AnswerLine b in base.xLines)
            {
                CircleF bubble = new CircleF(new Point(b.grabAverage(), a.grabAverage()), quickRadius);

                bubbles[i] = bubble;

                //Calculate the amount of colored pixels to detect colored in circles
                int totalPixel = 0;
                for (int x = b.grabAverage() - halfRadius; x <= b.grabAverage() + halfRadius; x++)
                {
                    for (int y = a.grabAverage() - halfRadius; y <= a.grabAverage() + halfRadius; y++)
                    {
                        if (Math.Pow((double)(x - b.grabAverage()), 2) + Math.Pow((double)(y - a.grabAverage()), 2) <= Math.Pow((1.25) * base.avRadius, 2))
                        {
                            totalPixel = totalPixel + (255 - source.GetPixel(x, y).R) + (255 - source.GetPixel(x, y).B) + (255 - source.GetPixel(x, y).G);
                        }
                    }
                }
                pixels[i] = (int)(totalPixel / bubble.Area);
                i = i + 1;
            }
            while (this.colorArray[row] != pixels)
            {
                this.colorArray[row] = pixels;
            }
            while (this.centerCircles[row] != bubbles)
            {
                this.centerCircles[row] = bubbles;
            }

        }

        private int returnLargest(int c1, int c2)
        {
            if (c1>c2)
                 return 0;
            return 1;
        }

        public override void drawCorrectAnswer(Graphics drawWith)
        {
            for (int row = 0; row < this.scores.Count; row++)
            {
                if (scores[row][0] == 2)
                    drawWith.FillEllipse(new SolidBrush(Color.Green), new Rectangle((int)this.centerCircles[row][1].Center.X - (int)(avRadius), (int)this.centerCircles[row][1].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                if (scores[row][1] == 2)
                    drawWith.FillEllipse(new SolidBrush(Color.Green), new Rectangle((int)this.centerCircles[row][3].Center.X - (int)(avRadius), (int)this.centerCircles[row][3].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                 }
        }

        public override void drawInCorrectAnswer(Graphics drawWith)
        {

            for (int row = 0; row < this.scores.Count; row++)
            {
                if (scores[row][0] == 1)
                    drawWith.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)this.centerCircles[row][1].Center.X - (int)(avRadius), (int)this.centerCircles[row][1].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                if (scores[row][1] == 1)
                    drawWith.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)this.centerCircles[row][3].Center.X - (int)(avRadius), (int)this.centerCircles[row][3].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
               
            }
        }

        public override void drawBlank(Graphics drawWith)
        {
            for (int row = 0; row < this.scores.Count; row++)
            {
                if (scores[row][0] == 0)
                {
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][0].Center.X - (int)(avRadius), (int)this.centerCircles[row][0].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][1].Center.X - (int)(avRadius), (int)this.centerCircles[row][1].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][2].Center.X - (int)(avRadius), (int)this.centerCircles[row][2].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][3].Center.X - (int)(avRadius), (int)this.centerCircles[row][3].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                }
                if (scores[row][1] == 0)
                {
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][4].Center.X - (int)(avRadius), (int)this.centerCircles[row][4].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][5].Center.X - (int)(avRadius), (int)this.centerCircles[row][5].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][6].Center.X - (int)(avRadius), (int)this.centerCircles[row][6].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));
                    drawWith.FillEllipse(new SolidBrush(Color.Violet), new Rectangle((int)this.centerCircles[row][7].Center.X - (int)(avRadius), (int)this.centerCircles[row][7].Center.Y - (int)(avRadius), (int)(2 * avRadius), (int)(2 * avRadius)));

                }
               
            }
        }

        public override List<string> getResponses()
        {
            List<String> toReturn = new List<string>();
           for (int i=0; i<this.scores.Count; i++)
            {
                if (this.scores[i][0] == 1 || this.scores[i][0] == 2)
                    toReturn.Add(this.scoreRow[i][0].ToString());
                else if (this.scores[i][0] == 0)
                    toReturn.Add("Blank");
            }
            for (int i = 0; i < this.scores.Count; i++)
            {
                if (this.scores[i][1] == 1 || this.scores[i][1] == 2)
                    toReturn.Add(this.scoreRow[i][0].ToString());
                else if (this.scores[i][1] == 0)
                    toReturn.Add("Blank");
            }
            return toReturn;
        }
    }
}
