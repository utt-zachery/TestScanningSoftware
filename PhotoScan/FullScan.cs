using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Scanning_Software.Data;
using Test_Scanning_Software.Exam_Sections;
using Test_Scanning_Software.GUI;
using Test_Scanning_Software.PhotoScan;
using Test_Scanning_Software.Resources;

namespace Test_Scanning_Software.PhotoScan
{
    public class FullScan
    {
        public static BubbleSheet scanSheet(Bitmap input, ExamSpecifics test)
        {
            RotatedRect[] barcodes = BarScan.detectBarcodes(input);
            Bitmap toScan = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(toScan);
            g.DrawImage(input, new Point(0, 0));
            g.Dispose();
            toScan.SetPixel((int)barcodes[0].GetVertices()[1].X, (int)barcodes[0].GetVertices()[1].Y, Color.FromArgb(150, 0, 50));
            toScan.SetPixel((int)barcodes[0].GetVertices()[3].X, (int)barcodes[0].GetVertices()[3].Y, Color.FromArgb(150, 0, 100));

            toScan.SetPixel((int)barcodes[1].GetVertices()[1].X, (int)barcodes[1].GetVertices()[1].Y, Color.FromArgb(150, 50, 50));
            toScan.SetPixel((int)barcodes[1].GetVertices()[3].X, (int)barcodes[1].GetVertices()[3].Y, Color.FromArgb(150, 50,100));
           
            Bitmap toCompartmentalize = new Bitmap(toScan.Width, toScan.Height);
            int minY = 0;
            bool found1 = false;
            bool isDone = false;
            Point p1 = new Point();
            Point p2 = new Point();

            Point p3 = new Point();
            Point p4 = new Point();
            for (int y = 0; y < toScan.Height; y++)
            {
                if (isDone) break;
                for (int x = 0; x < toScan.Width; x++)
                {
                    Color c = toScan.GetPixel(x, y);
                    if (c.R == 150)
                    {
                        if (c.B == 50 && c.G == 0)
                        {
                            p1 = new Point(x, y);
                        }
                        else if (c.B == 100 && c.G == 0)
                        {
                            p2 = new Point(x, y);
                        }
                        else if (c.B == 50 && c.G == 50)
                        {
                            p3 = new Point(x, y);
                        }
                        else if (c.B == 100 && c.G == 50)
                        {
                            p4 = new Point(x, y);
                        }
                        if (c.B == 100)
                        {
                            if (found1)
                            {
                                if (y > minY) minY = y;
                                isDone = true;
                                break;
                            }
                            else
                            {
                                minY = y;
                                found1 = true;
                            }
                        }
                    }
                }
            }
        
            Graphics p = Graphics.FromImage(toCompartmentalize);
            p.DrawImage(toScan, new Point(0, 0));
            p.Dispose();
            Rectangle r1 = new Rectangle(p1.X-50, p1.Y -50, (p2.X - p1.X)+100, (p2.Y - p1.Y)+100);
            Rectangle r2 = new Rectangle(p3.X-50, p3.Y -50, (p4.X - p3.X)+100, (p4.Y - p3.Y)+100);
            Barcodes quickCodes = new Barcodes(BarScan.cropImage(toCompartmentalize, r1), BarScan.cropImage(toCompartmentalize, r2));
            toCompartmentalize = BarScan.cropImage(toCompartmentalize, new Rectangle(0, minY, toCompartmentalize.Width, toCompartmentalize.Height - minY));
            
           
            ScanAndGrade scantron = Compartmentalize.findBubbles(toCompartmentalize, test);

            //CALCULATE SCORE
            double score = 0;
            score = score + (scantron.scannedScores[1].score * test.multipleChoiceWeight);
            score = score + (scantron.scannedScores[2].score * test.pairingWeight);
            score = score + (scantron.scannedScores[3].score * test.trueWeight);
            score = score + (scantron.scannedScores[4].score * test.compWeight);
            score = score + (scantron.scannedScores[5].score * test.subjectWeight);

            Bitmap correctAnswerOverlay = new Bitmap(toCompartmentalize.Width, toCompartmentalize.Height);
            Bitmap inCorrectAnswerOverlay = new Bitmap(toCompartmentalize.Width, toCompartmentalize.Height);
            Bitmap blankAnswerOverlay = new Bitmap(toCompartmentalize.Width, toCompartmentalize.Height);

            Graphics correctAnswerOverlayGraphics = Graphics.FromImage(correctAnswerOverlay);
            scantron.scannedScores[0].drawCorrectAnswer(correctAnswerOverlayGraphics);
            scantron.scannedScores[1].drawCorrectAnswer(correctAnswerOverlayGraphics);
            scantron.scannedScores[2].drawCorrectAnswer(correctAnswerOverlayGraphics);
            scantron.scannedScores[3].drawCorrectAnswer(correctAnswerOverlayGraphics);
            scantron.scannedScores[4].drawCorrectAnswer(correctAnswerOverlayGraphics);
            scantron.scannedScores[5].drawCorrectAnswer(correctAnswerOverlayGraphics);
            correctAnswerOverlayGraphics.Dispose();

            Graphics inCorrectAnswerOverlayGraphics = Graphics.FromImage(inCorrectAnswerOverlay);
            scantron.scannedScores[0].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            scantron.scannedScores[1].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            scantron.scannedScores[2].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            scantron.scannedScores[3].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            scantron.scannedScores[4].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            scantron.scannedScores[5].drawCorrectAnswer(inCorrectAnswerOverlayGraphics);
            inCorrectAnswerOverlayGraphics.Dispose();

            Graphics blankAnswerOverlayGraphics = Graphics.FromImage(blankAnswerOverlay);
            scantron.scannedScores[0].drawCorrectAnswer(blankAnswerOverlayGraphics);
            scantron.scannedScores[1].drawCorrectAnswer(blankAnswerOverlayGraphics);
            scantron.scannedScores[2].drawCorrectAnswer(blankAnswerOverlayGraphics);
            scantron.scannedScores[3].drawCorrectAnswer(blankAnswerOverlayGraphics);
            scantron.scannedScores[4].drawCorrectAnswer(blankAnswerOverlayGraphics);
            scantron.scannedScores[5].drawCorrectAnswer(blankAnswerOverlayGraphics);
            blankAnswerOverlayGraphics.Dispose();

            BubbleSheet toReturn = new BubbleSheet(quickCodes.seatID,quickCodes.socialID, test, score, (scantron.scannedScores[0] as StudentCondition).didCheat, (scantron.scannedScores[0] as StudentCondition).wasAbsent,scantron.scannedScores[1].getResponses(),scantron.scannedScores[2].getResponses(),scantron.scannedScores[3].getResponses(),scantron.scannedScores[4].getResponses(),scantron.scannedScores[5].getResponses(), correctAnswerOverlay, inCorrectAnswerOverlay,toCompartmentalize,blankAnswerOverlay);

            return toReturn;
        }
    }
}
