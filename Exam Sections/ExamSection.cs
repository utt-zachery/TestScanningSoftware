using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Scanning_Software.Data;

namespace Test_Scanning_Software.Exam_Sections
{
    public abstract class ExamSection
    {
        
        public double avRadius;
        
        public int score;
        public List<AnswerLine> xLines;
        public List<AnswerLine> yLines;
        public Bitmap inputImage;
        public ExamSpecifics test;

        public ExamSection(List<AnswerLine> xLines, List<AnswerLine> yLines, Bitmap inputImage, ExamSpecifics test, double avRadius)
        {
            this.xLines = xLines.OrderBy(o => o.grabAverage()).ToList();
            this.yLines = yLines.OrderBy(o => o.grabAverage()).ToList();
            this.inputImage = inputImage;
            this.test = test;
            this.avRadius = avRadius;
        }

        public abstract void scoreSection(Bitmap original);
        public abstract List<String> getResponses();

        public abstract void drawCorrectAnswer(Graphics drawWith);
        public abstract void drawInCorrectAnswer(Graphics drawWith);
        public abstract void drawBlank(Graphics drawWith);
    }
}
