using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace Test_Scanning_Software.Data
{
    public class Exam
    {
        // Generic Test fields
        public String examName { get; set; }
        public int examID;
        private Scores examScores;
        private String dateCreated;
        private String timeCreated;

        //Exam specifics
        private int multipleChoiceCount = 0;
        private int pairingQuestionsCount = 0;
        private int trueFalseQuestionsCount = 0;
        private int completionQuestionsCount = 0;
        private int subjectiveQuestionsCount = 0;
        private int multipleChoiceAnswerCount = 0;
        private int pairingAnswerCount = 0;
        private int subjectiveAnswerCount = 0;


        //Score weights
        private double multipleChoiceWeight = 0;
        private double pairingQuestionsWeight = 0;
        private double trueFalseQuestionsWeight = 0;
        private double completionQuestionsWeight = 0;
        private double subjectiveQuestionsWeight = 0;

        public Exam(string examName, int examID, Scores examScores, string dateCreated, string timeCreated, int multipleChoiceCount, int pairingQuestionsCount, int trueFalseQuestionsCount, int completionQuestionsCount, int subjectiveQuestionsCount, int multipleChoiceAnswerCount, int pairingAnswerCount, int subjectiveAnswerCount, double multipleChoiceWeight, double pairingQuestionsWeight, double trueFalseQuestionsWeight, double completionQuestionsWeight, double subjectiveQuestionsWeight)
        {
            this.examName = examName;
            this.examID = examID;
            this.examScores = examScores;
            this.dateCreated = dateCreated;
            this.timeCreated = timeCreated;
            this.multipleChoiceCount = multipleChoiceCount;
            this.pairingQuestionsCount = pairingQuestionsCount;
            this.trueFalseQuestionsCount = trueFalseQuestionsCount;
            this.completionQuestionsCount = completionQuestionsCount;
            this.subjectiveQuestionsCount = subjectiveQuestionsCount;
            this.multipleChoiceAnswerCount = multipleChoiceAnswerCount;
            this.pairingAnswerCount = pairingAnswerCount;
            this.subjectiveAnswerCount = subjectiveAnswerCount;
            this.multipleChoiceWeight = multipleChoiceWeight;
            this.pairingQuestionsWeight = pairingQuestionsWeight;
            this.trueFalseQuestionsWeight = trueFalseQuestionsWeight;
            this.completionQuestionsWeight = completionQuestionsWeight;
            this.subjectiveQuestionsWeight = subjectiveQuestionsWeight;
        }

        public static byte[] ImageToByte(Image img)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        
        public Document generateSheet(Student toGenerate, Document document, PdfWriter writer)
        {
            
            BaseFont bfArialUniCode = BaseFont.CreateFont(@"C:\Users\Uttza\OneDrive\Certiprep 2.0\Test Scanning Software\Test Scanning Software\Resources\arabic.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont c128 = BaseFont.CreateFont(@"C:\Users\Uttza\OneDrive\Certiprep 2.0\Test Scanning Software\Test Scanning Software\Resources\code128.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font code128 = new Font(c128, 30);
            Font font = new Font(bfArialUniCode, 14);
            Font font2 = new Font(bfArialUniCode,12);
            Font fontB = new Font(bfArialUniCode, 14, iTextSharp.text.Font.BOLD);
            Font fontBs = new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD);
            PdfPTable table = new PdfPTable(1);
            PdfPTable table2 = new PdfPTable(1);

            PdfPTable table3 = new PdfPTable(1);
            PdfPTable table4 = new PdfPTable(1);
            PdfPTable table5 = new PdfPTable(1);
            PdfPTable table6 = new PdfPTable(1);

            PdfPTable table7 = new PdfPTable(1);
            PdfPTable table8 = new PdfPTable(1);
            PdfPTable table9 = new PdfPTable(1);
            PdfPTable table10 = new PdfPTable(1);
            PdfPTable table11 = new PdfPTable(1);
            PdfPTable table14 = new PdfPTable(1);
            


            //Barcodes
            PdfPTable examCodeLab = new PdfPTable(1);
            PdfPTable studentCodeLab = new PdfPTable(1);
            examCodeLab.TotalWidth = 550;
            studentCodeLab.TotalWidth = 250;

            table14.TotalWidth = 75;
            table.DefaultCell.NoWrap = false;
            table.TotalWidth = 150;
            table2.DefaultCell.NoWrap = false;
            table2.TotalWidth = 150;
            table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table3.DefaultCell.NoWrap = false;
            table3.TotalWidth = 150;
            table3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table4.DefaultCell.NoWrap = false;
            table4.TotalWidth = 150;
            table4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table7.DefaultCell.NoWrap = false;
            table7.TotalWidth = 150;
            table7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table8.DefaultCell.NoWrap = false;
            table8.TotalWidth = 150;
            table8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table9.DefaultCell.NoWrap = false;
            table9.TotalWidth = 150;
            table9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table10.DefaultCell.NoWrap = false;
            table10.TotalWidth = 150;
            table10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            table5.DefaultCell.NoWrap = false;
            table5.TotalWidth = 150;
            table5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table6.TotalWidth = 150;
            table11.TotalWidth = 555;
            Phrase p = new Phrase("اللغة الانجليزية", font);
            p.Leading = 0;
            p.SetLeading(0,0);
            PdfPCell text = new PdfPCell(p);
            text.NoWrap = false;
            text.BorderColor = BaseColor.WHITE;
            text.FixedHeight = 18;
            text.BorderWidth = 0;
            table.AddCell(text);

            
         

            String output = this.examID.ToString().PadLeft(3, '0');
            output = output + "-" + toGenerate.studentID;
            

            Phrase p2 = new Phrase("3", font2);
            p2.Leading = 0;
            p2.SetLeading(0, 0);
            p2.TrimExcess();
            PdfPCell text2 = new PdfPCell(p2);
            text2.NoWrap = false;
            text2.BorderColor = BaseColor.WHITE;
            text2.FixedHeight = 16;
            table.AddCell(text2);
            text2.BorderWidth = 0;

            Phrase p3 = new Phrase("ساعتان", font);
            p3.Leading = 0;
            p3.SetLeading(0, 0);
            p3.TrimExcess();
            PdfPCell text3 = new PdfPCell(p3);
            text3.NoWrap = false;
            text3.BorderColor = BaseColor.WHITE;
            text3.BorderWidth = 0;
            text3.FixedHeight = 18;
            table2.AddCell(text3);

            PdfPCell text4 = new PdfPCell(new Phrase("1", font2));
            text4.NoWrap = false;
            text4.FixedHeight = 16;
            text4.BorderColor = BaseColor.WHITE;
            text4.BorderWidth = 0;
            table2.AddCell(text4);


            PdfPCell text5 = new PdfPCell(new Phrase("المادة", fontB));
            text5.NoWrap = false;
            text5.FixedHeight = 18;
            text5.BorderColor = BaseColor.WHITE;
            text5.BorderWidth = 0;
            table3.AddCell(text5);

            PdfPCell text6 = new PdfPCell(new Phrase("الصف", fontB));
            text6.NoWrap = false;
            text6.FixedHeight = 18;
            text6.BorderColor = BaseColor.WHITE;
            text6.BorderWidth = 0;
            table5.AddCell(text6);

            PdfPCell text7 = new PdfPCell(new Phrase("الزمن", fontB));
            text7.NoWrap = false;
            text7.FixedHeight = 18;
            text7.BorderColor = BaseColor.WHITE;
            text7.BorderWidth = 0;
            table4.AddCell(text7);

            PdfPCell text8 = new PdfPCell(new Phrase("رقم اللجنة", fontB));
            text8.NoWrap = false;
            text8.FixedHeight = 18;
            text8.BorderColor = BaseColor.WHITE;
            text8.BorderWidth = 0;
            table4.AddCell(text8);


            PdfPCell text9 = new PdfPCell(new Phrase("المملكة العربية السعودية", fontB));
            text9.NoWrap = false;
            text9.FixedHeight = 18;
            text9.BorderColor = BaseColor.WHITE;
            text9.BorderWidth = 0;
            table7.AddCell(text9);

            PdfPCell text10 = new PdfPCell(new Phrase("وزارة التعليم", fontB));
            text10.NoWrap = false;
            text10.FixedHeight = 18;
            text10.BorderColor = BaseColor.WHITE;
            text10.BorderWidth = 0;
            table8.AddCell(text10);

            PdfPCell text11 = new PdfPCell(new Phrase("الادارة العامة للتعليم بمحافظة جدة", fontB));
            text11.NoWrap = false;
            text11.FixedHeight = 18;
            text11.BorderColor = BaseColor.WHITE;
            text11.BorderWidth = 0;
            table9.AddCell(text11);

            PdfPCell text12 = new PdfPCell(new Phrase("مدرسة الامام الذهبي المتوسطة", fontB));
            text12.NoWrap = false;
            text12.FixedHeight = 18;
            text12.BorderColor = BaseColor.WHITE;
            text12.BorderWidth = 0;
            table10.AddCell(text12);

        

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Properties.Resources.ministry_of_education_saudi_arabia_seeklogo_com, ImageFormat.Png);
            jpg.BorderColor = BaseColor.WHITE;
            jpg.BackgroundColor = BaseColor.WHITE;
            jpg.BorderWidth = 0;
            PdfPCell imlogo = new PdfPCell(jpg);
            imlogo.BorderWidth = 0;
            imlogo.BorderColor = BaseColor.WHITE;
            table6.AddCell(imlogo);

            
            PdfPCell quickCode1 = new PdfPCell(new Phrase(output, code128));
            quickCode1.BorderWidth = 0;
            quickCode1.BorderColor = BaseColor.WHITE;
            quickCode1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            examCodeLab.AddCell(quickCode1);


            Phrase arabName = new Phrase();
            arabName.Add(new Phrase("اسم الطالب" + " : ", fontB));
            arabName.Add(new Phrase("احمد سعد معوض المرواني الجهني", fontB));
            PdfPCell quickCode2 = new PdfPCell(arabName);
            quickCode2.NoWrap = false;


            quickCode2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            quickCode2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            quickCode2.BorderWidth = 0;
            quickCode2.BorderColor = BaseColor.WHITE;
            studentCodeLab.AddCell(quickCode2);

            PdfContentByte canvas = writer.DirectContent;
            table.WriteSelectedRows(0, -1, -30, document.PageSize.Height-15, canvas);
            table2.WriteSelectedRows(0, -1, -30, document.PageSize.Height - 45, canvas);

            table3.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 15, canvas);
            table4.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 45, canvas);
            table5.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, canvas);
            table6.WriteSelectedRows(0, -1, 230, document.PageSize.Height - 15, canvas);

            table7.WriteSelectedRows(0, -1, 365, document.PageSize.Height - 15, canvas);
            table8.WriteSelectedRows(0, -1, 340, document.PageSize.Height - 30, canvas);
            table9.WriteSelectedRows(0, -1, 380, document.PageSize.Height - 45, canvas);
            table10.WriteSelectedRows(0, -1, 370, document.PageSize.Height - 60, canvas);


            examCodeLab.WriteSelectedRows(0, -1, 15, document.PageSize.Height - 120, canvas);
            studentCodeLab.WriteSelectedRows(0, -1, 340, document.PageSize.Height - 100, canvas);
            


            // Generate Sections
            BaseFont baseEnglishAnserChoices = BaseFont.CreateFont(@"C:\Users\Uttza\OneDrive\Certiprep 2.0\Test Scanning Software\Test Scanning Software\Resources\EnglishBubbles.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font englishAnserChoices = new Font(baseEnglishAnserChoices, 12);
            String answerstring = "";
            for (int a=1; a<this.multipleChoiceAnswerCount+1;a++)
            {
                answerstring = answerstring + "  " + (char)(a + 64);
            }
            PdfPTable border01 = new PdfPTable(1);
            PdfPTable border1 = new PdfPTable(1);
            PdfPTable MCheader = new PdfPTable(1);
            MCheader.TotalWidth = 100;


           
            PdfPCell sc = new PdfPCell(new Phrase("Student Condition", fontBs));
            sc.BorderColor = BaseColor.WHITE;
            sc.BorderWidth = 0;
            sc.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table14.AddCell(sc);

            PdfPTable table12 = new PdfPTable(1);
            table12.TotalWidth = 150;
            Phrase a1 = new Phrase("A ", englishAnserChoices);
            Phrase a2 = new Phrase("Absent", fontBs);
            Phrase allTogether = new Phrase();
            allTogether.Add(a1);
            allTogether.Add(a2);
            PdfPCell absent = new PdfPCell(allTogether);
            absent.BorderColor = BaseColor.WHITE;
            absent.BorderWidth = 0;
            table12.AddCell(absent);

            PdfPTable table13 = new PdfPTable(1);
            table13.TotalWidth = 150;
            Phrase a3 = new Phrase("C ", englishAnserChoices);
            Phrase a4 = new Phrase("Cheat", fontBs);
            Phrase allTogether2 = new Phrase();
            allTogether2.Add(a3);
            allTogether2.Add(a4);
            PdfPCell cheat = new PdfPCell(allTogether2);
            cheat.BorderColor = BaseColor.WHITE;
            cheat.BorderWidth = 0;
            table13.AddCell(cheat);

            PdfPCell MCH = new PdfPCell(new Phrase("Multiple Choice", fontB));
            MCH.NoWrap = false;
            MCH.BorderColor = BaseColor.WHITE;
            MCH.BorderWidth = 0;
            MCH.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            MCheader.AddCell(MCH);

            PdfPTable multipleChoiceTable = new PdfPTable(3);
            multipleChoiceTable.TotalWidth = (document.PageSize.Width - 50);
            border01.TotalWidth = (document.PageSize.Width - 40);
            border1.TotalWidth = (document.PageSize.Width - 40);
            for (int i = 1; i < multipleChoiceCount + 1; i++)
            {
                int realNum = 0;
                if (i % 3 == 1)
                {
                    realNum = (i / 3) + 1;
                }
                else if (i % 3 == 2)
                {
                    realNum = (i / 3) + (multipleChoiceCount/3) +1;
                }
                else if(i % 3 == 0)
                {
                    realNum = (i / 3) + 2*(multipleChoiceCount / 3);
                }
                String spacer = "  ";
                if (realNum > 9)
                {
                    spacer = "";
                }
                Phrase phrase = new Phrase();
                phrase.Add(new Phrase(realNum + spacer, new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD)));
                phrase.Add(new Phrase(answerstring, englishAnserChoices));
                PdfPCell question = new PdfPCell(phrase);
                question.NoWrap = false;
                question.FixedHeight = 18;
                question.BorderColor = BaseColor.WHITE;
                question.BorderWidth = 0;
                multipleChoiceTable.AddCell(question);
            }

            PdfPCell filler = new PdfPCell();
            filler.BorderWidthRight = 0;
            filler.BorderWidthLeft = 0;
            filler.BorderWidthTop = 1;
            filler.BorderWidthBottom = 1;

            PdfPCell filler01 = new PdfPCell();
            filler01.BorderWidthRight = 0;
            filler01.BorderWidthLeft = 0;
            filler01.BorderWidthTop = 1;
            filler01.BorderWidthBottom = 0;
            filler.FixedHeight = multipleChoiceTable.TotalHeight + 40;
            filler01.FixedHeight = 5;
            border1.AddCell(filler);
            border01.AddCell(filler01);
            border1.WriteSelectedRows(0, -1, 30-(30/2), document.PageSize.Height - 180, canvas);
            border01.WriteSelectedRows(0, -1, 30 - (30 / 2), document.PageSize.Height - 100, canvas);
            multipleChoiceTable.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 210, canvas);
            MCheader.WriteSelectedRows(0, -1, 250, document.PageSize.Height - 185, canvas);
            table12.WriteSelectedRows(0, -1, 405, document.PageSize.Height - 130, canvas);
            table13.WriteSelectedRows(0, -1, 405, document.PageSize.Height - 150, canvas);
            table14.WriteSelectedRows(0, -1, 460, document.PageSize.Height - 135, canvas);
            // Pairing Questions
            answerstring = "";
            for (int a = 1; a < this.pairingAnswerCount + 1; a++)
            {
                answerstring = answerstring + "  " + (char)(a + 64);
            }
            answerstring = answerstring + "  ";
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1));
            
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(@"C:\Users\Uttza\OneDrive\Certiprep 2.0\Test Scanning Software\Test Scanning Software\Resources\EnglishBubbles.ttf");
               System.Drawing.SizeF size = graphics.MeasureString(answerstring, new System.Drawing.Font(pfc.Families[0], 16, System.Drawing.FontStyle.Bold));
            
            PdfPTable border2 = new PdfPTable(1);
            PdfPTable Pairingheader = new PdfPTable(1);
            Pairingheader.TotalWidth = 125;


            PdfPCell PH = new PdfPCell(new Phrase("Pairing Questions", fontB));
            PH.NoWrap = false;
            PH.BorderColor = BaseColor.WHITE;
            PH.BorderWidth = 0;
            PH.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            Pairingheader.AddCell(PH);

            PdfPTable pairingTable = new PdfPTable(1);
            pairingTable.TotalWidth = size.Width + 50;
            for (int i = 1; i < this.pairingQuestionsCount + 1; i++)
            {
                String spacer = "  ";
                if (i > 9)
                {
                    spacer = "";
                }
                Phrase phrase = new Phrase();
                phrase.Add(new Phrase(i + spacer, new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD)));
                phrase.Add(new Phrase(answerstring, englishAnserChoices));
                PdfPCell question = new PdfPCell(phrase);
                question.NoWrap = false;
                question.FixedHeight = 18;
                question.BorderColor = BaseColor.WHITE;
                question.BorderWidth = 0;
                pairingTable.AddCell(question);
            }
           
            PdfPCell filler2 = new PdfPCell();
            filler2.BorderWidth = 1;
            filler2.FixedHeight = pairingTable.TotalHeight + 40;
            filler2.BorderWidthRight = 1;
            filler2.BorderWidthLeft = 0;
            filler2.BorderWidthTop = 0;
            filler2.BorderWidthBottom = 1;

            border2.AddCell(filler2);
            border2.TotalWidth = ((11*size.Width)/20)+70;
            border2.WriteSelectedRows(0, -1, 30 - (30 / 2), document.PageSize.Height - 400, canvas);
            pairingTable.WriteSelectedRows(0, -1,40, document.PageSize.Height - 430, canvas);
            Pairingheader.WriteSelectedRows(0, -1, (border2.TotalWidth/2 - 50), document.PageSize.Height - 405, canvas);

            // True & False Questions
            answerstring = "T  F";


            PdfPTable border3 = new PdfPTable(1);
            PdfPTable TrueFalseheader = new PdfPTable(1);
            TrueFalseheader.TotalWidth = 125;


            PdfPCell TFH = new PdfPCell(new Phrase("True / False", fontB));
            TFH.NoWrap = false;
            TFH.BorderColor = BaseColor.WHITE;
            TFH.BorderWidth = 0;
            TFH.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            TrueFalseheader.AddCell(TFH);

           
            PdfPTable TFTable = new PdfPTable(1);
            TFTable.TotalWidth = border1.TotalWidth - border2.TotalWidth+5;

            for (int i = 1; i < this.trueFalseQuestionsCount + 1; i++)
            {
                String spacer = "  ";
                if (i > 9)
                {
                    spacer = " ";
                }
                Phrase TFHrase = new Phrase();
                
                    TFHrase.Add(new Phrase(i+"", new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD)));
                if (i > 9)
                {
                    TFHrase.Add(new Phrase(" ", new Font(bfArialUniCode, 14, iTextSharp.text.Font.NORMAL)));
                    TFHrase.Add(new Phrase(" ", new Font(bfArialUniCode, 3, iTextSharp.text.Font.NORMAL)));
                }

                Phrase pat = new Phrase(spacer + answerstring, englishAnserChoices);

                    TFHrase.Add(pat);
                
               
                PdfPCell question = new PdfPCell(TFHrase);
                question.NoWrap = false;
                question.FixedHeight = 18;
                question.BorderColor = BaseColor.WHITE;
                question.BorderWidth = 0;
                TFTable.AddCell(question);
            }

            PdfPCell filler3 = new PdfPCell();
            filler3.BorderWidth = 1;

                filler3.FixedHeight = filler2.FixedHeight;
            
            filler3.BorderWidthRight = 0;
            filler3.BorderWidthLeft = 0;
            filler3.BorderWidthTop = 0;
            filler3.BorderWidthBottom = 1;

            border3.AddCell(filler3);
            border3.TotalWidth = border1.TotalWidth - border2.TotalWidth;
            border3.WriteSelectedRows(0, -1,15+border2.TotalWidth, document.PageSize.Height - 400, canvas);
            TFTable.WriteSelectedRows(0, -1, 55 + border2.TotalWidth+(25/2), document.PageSize.Height - 430, canvas);
            TrueFalseheader.WriteSelectedRows(0, -1, (22 + border2.TotalWidth + (25 / 2))+(border3.TotalWidth/2 - 75), document.PageSize.Height - 405, canvas);

            //Subjective Questions
            answerstring = "";
            for (int a = 1; a < this.subjectiveAnswerCount + 1; a++)
            {
                answerstring = answerstring + "  " + (char)(a + 47);
            }

            size = graphics.MeasureString(answerstring, new System.Drawing.Font(pfc.Families[0], 16, System.Drawing.FontStyle.Bold));

            PdfPTable border6 = new PdfPTable(1);
            PdfPTable Subjectheader = new PdfPTable(1);
            Subjectheader.TotalWidth = 125;


            PdfPCell SH = new PdfPCell(new Phrase("Subjective", fontB));
            SH.NoWrap = false;
            SH.BorderColor = BaseColor.WHITE;
            SH.BorderWidth = 0;
            SH.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            Subjectheader.AddCell(SH);

            PdfPTable SubjectTable = new PdfPTable(1);
            SubjectTable.TotalWidth = size.Width + 50;
            for (int i = 1; i < this.subjectiveQuestionsCount + 1; i++)
            {
                String spacer = "  ";
                if (i > 9)
                {
                    spacer = "";
                }
                Phrase phrase = new Phrase();
                phrase.Add(new Phrase(i + spacer, new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD)));
                phrase.Add(new Phrase(answerstring, englishAnserChoices));
                PdfPCell question = new PdfPCell(phrase);
                question.NoWrap = false;
                question.FixedHeight = 18;
                question.BorderColor = BaseColor.WHITE;
                question.BorderWidth = 0;
                SubjectTable.AddCell(question);
            }

            PdfPCell filler6 = new PdfPCell();
        
            filler6.BorderWidthRight = 0;
            filler6.BorderWidthTop = 0;
            filler6.BorderWidthBottom = 1;
            filler6.BorderWidthLeft = 0;

            filler6.FixedHeight = SubjectTable.TotalHeight + 40;

            border6.AddCell(filler6);
            border6.TotalWidth = ((11 * size.Width) / 20) + 80;
            border6.WriteSelectedRows(0, -1, ((document.PageSize.Width)- border6.TotalWidth)-25, document.PageSize.Height - 620, canvas);
            SubjectTable.WriteSelectedRows(0, -1, ((document.PageSize.Width) - border6.TotalWidth) - 5, document.PageSize.Height - 650, canvas);
            Subjectheader.WriteSelectedRows(0, -1, (((document.PageSize.Width) - border6.TotalWidth) - 35)+((border6.TotalWidth/2)-60), document.PageSize.Height - 625, canvas);

            // Completion Questions
            answerstring = "0  1";


            PdfPTable border4 = new PdfPTable(1);
            PdfPTable Compheader = new PdfPTable(1);
            Compheader.TotalWidth = 125;


            PdfPCell CH = new PdfPCell(new Phrase("Completion", fontB));
            CH.NoWrap = false;
            CH.BorderColor = BaseColor.WHITE;
            CH.BorderWidth = 0;
            CH.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            Compheader.AddCell(CH);

            PdfPTable compTable = new PdfPTable(2);
            compTable.TotalWidth = (border1.TotalWidth - border6.TotalWidth)-30;
            for (int i = 1; i < this.completionQuestionsCount + 2; i++)
            {
                int realNum = 0;
                if (i % 2 == 1)
                {
                    realNum = (i / 2) + 1;
                }
                else
                {
                    realNum = (i / 2) + (completionQuestionsCount / 2) + (this.completionQuestionsCount % 2);
                } 
                String spacer = "        ";
                if (realNum > 9)
                {
                    spacer = "   ";
                }
                Phrase phrase = new Phrase();
                if (i == this.completionQuestionsCount +1 && i % 2 == 0)
                {
                    phrase.Add(new Phrase());
                    phrase.Add(new Phrase());
                }
                else if (i != this.completionQuestionsCount + 1)
                {
                    phrase.Add(new Phrase(realNum  + spacer, new Font(bfArialUniCode, 12, iTextSharp.text.Font.BOLD)));
                    if (realNum > 9)
                    {
                        phrase.Add(new Phrase(" ", new Font(bfArialUniCode, 14, iTextSharp.text.Font.NORMAL)));
                        phrase.Add(new Phrase(" ", new Font(bfArialUniCode, 14, iTextSharp.text.Font.NORMAL)));
                        phrase.Add(new Phrase(" ", new Font(bfArialUniCode, 8, iTextSharp.text.Font.NORMAL)));
                    }
                    phrase.Add(new Phrase(answerstring, englishAnserChoices));
                }
                
                PdfPCell question = new PdfPCell(phrase);
                question.NoWrap = false;
                question.FixedHeight = 18;
                question.BorderColor = BaseColor.WHITE;
                question.BorderWidth = 0;
                compTable.AddCell(question);
            }

            PdfPCell filler4 = new PdfPCell();
            filler4.BorderWidthRight = 1;
            filler4.BorderWidthTop = 0;
            filler4.BorderWidthBottom = 1;
            filler4.BorderWidthLeft = 0;
            if (compTable.TotalHeight + 40 >= filler6.FixedHeight)
            {
                filler4.FixedHeight = compTable.TotalHeight + 40;
            }
            else
            {
                filler4.FixedHeight = filler6.FixedHeight;
            }

            border4.AddCell(filler4);
            border4.TotalWidth = (border1.TotalWidth - border6.TotalWidth);
            border4.WriteSelectedRows(0, -1, 30 - (30 / 2), document.PageSize.Height - 620, canvas);
            compTable.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 650, canvas);
            Compheader.WriteSelectedRows(0, -1, (border4.TotalWidth / 2 - 60), document.PageSize.Height - 625, canvas);


            return document;
        }

    }
}
