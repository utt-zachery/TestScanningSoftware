using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Scanning_Software.Data;
using Test_Scanning_Software.Exam_Sections;
using Test_Scanning_Software.GUI;
using Test_Scanning_Software.PhotoScan;
using Test_Scanning_Software.Resources;

namespace Test_Scanning_Software
{
    static class Startup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /**
            List<Student> roster = new List<Student>();
            roster.Add(new Student("1262050", "Zachery Utt"));
            Exam quickExam = new Exam("AP Biology", 0,null,"","",30,10,10,10,5,4,10,7,1.0,1.0,1.0,1.0,1.0);
           **/

            ExamSpecifics APBiologyExam = new ExamSpecifics(true,"AP Biology Exam",0,"AAAAAAAAAAAAA",1,"AAAAAAA",1,"TTTT",1,"111",1,1);
                
        //    Stopwatch quicktime = new Stopwatch();
         //   quicktime.Start();
        //    Bitmap input = new Bitmap(Image.FromFile(@"C:\Users\Uttza\Desktop\toAnalyze.png"));
        //    BubbleSheet scanned = FullScan.scanSheet(input, APBiologyExam);
            
        //    scanned.save();
        //    Debug.WriteLine(quicktime.ElapsedMilliseconds);
   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Graded());

        }
    }
}
