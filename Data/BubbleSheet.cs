using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Test_Scanning_Software.Exam_Sections;

namespace Test_Scanning_Software.Data
{
    [Serializable]
    public class BubbleSheet
    {
        public String seatID;
        public String socialID;
        public ExamSpecifics exam;

        public double score;
        public bool didCheat;
        public bool wasAbsent;

        public List<String> multipleChoiceAnswers;
        public List<String> pairingAnswers;
        public List<String> trueFalseAnswers;

        public List<String> completionAnswers;
        public List<String> subjectiveAnswers;

        public Bitmap correctAnswerOverylay;
        public Bitmap incorrectAnswerOverlay;
        public Bitmap baseSheet;
        public Bitmap unansweredAnswerOverlay;

        public BubbleSheet()
        {
        }

        public BubbleSheet(string seatID, string socialID, ExamSpecifics exam, double score, bool didCheat, bool wasAbsent, List<String> multipleChoiceAnswers, List<String> pairingAnswers, List<String> trueFalseAnswers, List<String> completionAnswers, List<String> subjectiveAnswers, Bitmap correctAnswerOverylay, Bitmap incorrectAnswerOverlay, Bitmap baseSheet, Bitmap unansweredAnswerOverlay)
        {
            this.seatID = seatID;
            this.socialID = socialID;
            this.exam = exam;
            this.score = score;
            this.didCheat = didCheat;
            this.wasAbsent = wasAbsent;
            this.multipleChoiceAnswers = multipleChoiceAnswers;
            this.pairingAnswers = pairingAnswers;
            this.trueFalseAnswers = trueFalseAnswers;
            this.completionAnswers = completionAnswers;
            this.subjectiveAnswers = subjectiveAnswers;
            this.correctAnswerOverylay = correctAnswerOverylay;
            this.incorrectAnswerOverlay = incorrectAnswerOverlay;
            this.baseSheet = baseSheet;
            this.unansweredAnswerOverlay = unansweredAnswerOverlay;
        }

        public void save()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(BubbleSheet));

            var xml = "";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    xml = sww.ToString();
                }
            }
            System.IO.File.WriteAllText("Stored Data\\Answer Sheets\\" + this.socialID + "-" + exam.testID, xml);

        }
    }
}
