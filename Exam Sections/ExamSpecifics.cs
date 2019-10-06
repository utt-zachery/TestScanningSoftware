using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Test_Scanning_Software.Exam_Sections
{
    public class ExamSpecifics
    {
        public bool isEnglish;
        public string answerSheetName;
        public int testID;
        

        public ExamSpecifics()
        {
            // Constructor to load exam for .XML file
        }

        public ExamSpecifics(bool isEnglish, string answerSheetName, int testID, string multipleChoiceAnswers, double multipleChoiceWeight, string pairingAnswers, double pairingWeight, string trueAnswers, double trueWeight, string compAnswers, double compWeight, double subjectWeight)
        {
            this.isEnglish = isEnglish;
            this.answerSheetName = answerSheetName;
            this.testID = testID;
            this.multipleChoiceAnswers = multipleChoiceAnswers;
            this.multipleChoiceWeight = multipleChoiceWeight;
            this.pairingAnswers = pairingAnswers;
            this.pairingWeight = pairingWeight;
            this.trueAnswers = trueAnswers;
            this.trueWeight = trueWeight;
            this.compAnswers = compAnswers;
            this.compWeight = compWeight;
            this.subjectWeight = subjectWeight;
        }

        //Multiple Choice
        public String multipleChoiceAnswers;
        public double multipleChoiceWeight;

        //Pairing Questions
        public String pairingAnswers;
        public double pairingWeight;

        // True / False
        public String trueAnswers;
        public double trueWeight;

        //Completion Questions
        public String compAnswers;
        public double compWeight;

        //Subjective
        public double subjectWeight;

      
        public void save()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ExamSpecifics));
        
            var xml = "";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    xml = sww.ToString(); 
                }
            }
            System.IO.File.WriteAllText("Stored Data\\Answer Sheets\\" + this.testID, xml);

        }
    }
}
