using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Scanning_Software.Data;
using Test_Scanning_Software.Exam_Sections;
using Test_Scanning_Software.Resources;

namespace Test_Scanning_Software.PhotoScan
{
    public class Compartmentalize
    {
        public static ScanAndGrade  findBubbles(Bitmap input, ExamSpecifics APBiologyExam)
        {
            Bitmap stretched = new Bitmap(input, (int)(input.Width), (int)(input.Height));

            Image<Gray, Byte> grayImage = new Image<Bgr, byte>(stretched).Convert<Gray, Byte>();
            CvInvoke.Threshold(grayImage, grayImage, 120, 1000, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
            
            Gray cannyThreshold = new Gray(1);

            Gray circleAccumulatorThreshold = new Gray(30);

            CircleF[] circles = grayImage.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                1, //Resolution of the accumulator used to detect centers of the circles
                1, //min distance 
                15, //min radius
                60 //max radius
                )[0]; //Get the circles from the first channel

            Graphics f = Graphics.FromImage(input);
            

            Graphics g = Graphics.FromImage(stretched);



            List<Point> circleCenter = new List<Point>();

            double avRadius = 0;


            foreach (CircleF c1 in circles)
            {
                avRadius = avRadius + c1.Radius;
                circleCenter.Add(new Point((int)c1.Center.X, (int)c1.Center.Y));
            }
            avRadius = avRadius / circles.Count();
          
            input.Save("presorted.png");
            Sectionizer testSections = new Sectionizer(circleCenter, avRadius);

            
            ScanAndGrade scantron = new ScanAndGrade(stretched, testSections, APBiologyExam, avRadius);
            Debug.WriteLine("AP Biology Exam Score: " + scantron.FinalUnweightedScore);
            
            
            return scantron;
        }


    }
}
