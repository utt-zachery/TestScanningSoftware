using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test_Scanning_Software.Exam_Sections;

namespace Test_Scanning_Software.Data
{
    public class ScanAndGrade
    {
        public List<ExamSection> scannedScores;
        public int FinalUnweightedScore;

        public ScanAndGrade(Bitmap stretched, Sectionizer testSections, ExamSpecifics exam, double avRadius)
        {

            Bitmap fullCopy1 = new Bitmap(stretched.Width, stretched.Height);
            Graphics g = Graphics.FromImage(fullCopy1);
            g.DrawImage(stretched, new Point(0, 0));


            Bitmap fullCopy0 = new Bitmap(stretched.Width, stretched.Height);
            g = Graphics.FromImage(fullCopy0);
            g.DrawImage(stretched, new Point(0, 0));


            Bitmap fullCopy2 = new Bitmap(stretched.Width, stretched.Height);
            g = Graphics.FromImage(fullCopy2);
            g.DrawImage(stretched, new Point(0, 0));


            Bitmap fullCopy3 = new Bitmap(stretched.Width, stretched.Height);
            g = Graphics.FromImage(fullCopy3);
            g.DrawImage(stretched, new Point(0, 0));

            Bitmap fullCopy4 = new Bitmap(stretched.Width, stretched.Height);
            g = Graphics.FromImage(fullCopy4);
            g.DrawImage(stretched, new Point(0, 0));

            Bitmap fullCopy5 = new Bitmap(stretched.Width, stretched.Height);
            g = Graphics.FromImage(fullCopy5);
            g.DrawImage(stretched, new Point(0, 0));

            g.Dispose();

            scannedScores = new List<ExamSection>(5);
            scannedScores.Add(null);
            scannedScores.Add(null);
            scannedScores.Add(null);
            scannedScores.Add(null);
            scannedScores.Add(null);
            scannedScores.Add(null);

            Thread thread0 = new Thread(() => gradeSection(0, fullCopy0, testSections, exam, avRadius));
            Thread thread1 = new Thread(() => gradeSection(1, fullCopy1, testSections, exam, avRadius));
            Thread thread2 = new Thread(() => gradeSection(2, fullCopy2, testSections, exam, avRadius));
            Thread thread3 = new Thread(() => gradeSection(3, fullCopy3, testSections, exam, avRadius));
            Thread thread4 = new Thread(() => gradeSection(4, fullCopy4, testSections, exam, avRadius));
            Thread thread5 = new Thread(() => gradeSection(5, fullCopy5, testSections, exam, avRadius));
            thread0.Start();
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread0.Join();
            thread4.Join();
            thread5.Join();
            for (int i=0; i<scannedScores.Count;i++)
            {
                if (scannedScores[i] != null)
                {
                    Debug.WriteLine(i + " : SCORE = " + scannedScores[i].score);
                    FinalUnweightedScore = FinalUnweightedScore + scannedScores[i].score;
                }
            }
        }

        private void gradeSection(int sectionCode, Bitmap stretched, Sectionizer testSections, ExamSpecifics exam, double avRadius)
        {
            if (sectionCode == 0)
            {
                LineScannerSC prelines = new LineScannerSC(testSections.studentCondition, avRadius);
                StudentCondition section1 = new StudentCondition(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section1.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode] = (section1);
            }
            if (sectionCode == 1)
            {
                LineScannerMC prelines = new LineScannerMC(testSections.multipleChoice, avRadius);
                Multiple_Choice section1 = new Multiple_Choice(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section1.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode]=(section1);
            }
            else if (sectionCode == 2)
            {
                LineScannerPairing prelines = new LineScannerPairing(testSections.pairing, avRadius);
                Pairing section2 = new Pairing(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section2.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode]=(section2);
            }
            else if (sectionCode == 3)
            {
                LineScannerTF prelines = new LineScannerTF(testSections.trueFalse, avRadius);
                True_False section3 = new True_False(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section3.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode] = (section3);
            }
            else if (sectionCode == 4)
            {
                LineScannerCompletion prelines = new LineScannerCompletion(testSections.completion, avRadius);
                Completion section4 = new Completion(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section4.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode] = (section4);
            }
            else if (sectionCode == 5)
            {
                LineScannerSubjective prelines = new LineScannerSubjective(testSections.subjective, avRadius);
                Subjective section4 = new Subjective(prelines.lineX, prelines.lineY, stretched, exam, avRadius);
                section4.scoreSection(stretched); // should be in a worker thread
                scannedScores[sectionCode] = (section4);
            }
        }
    }
}
