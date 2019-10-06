using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Scanning_Software.Data
{
    public partial class SheetGeneratorWaitingDialog : Form
    {
        private Exam testSource;
        private List<Student> toGenerate;
        public SheetGeneratorWaitingDialog(List<Student> toGenerate, Exam testSource)
        {
            InitializeComponent();
            this.label1.Text = this.label1.Text + testSource.examName;
            this.Show();
            this.testSource = testSource;
            this.toGenerate = toGenerate;
            this.progressBar1.Maximum = toGenerate.Count;
            this.progressBar1.Value = 0;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Document document = new Document(PageSize.A4);
            FileStream output = new FileStream(testSource.examName + " Answer Sheets.pdf", FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(document, output);
            document.Open();
            int i = 0;
            foreach (Student toAdd in toGenerate)
            {
                document.NewPage();
                testSource.generateSheet(toGenerate[i], document, writer);
                i++;
                backgroundWorker1.ReportProgress(i);
            }
            
               
            document.Close();
            backgroundWorker1.ReportProgress(i+1);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
         
            if (e.ProgressPercentage == progressBar1.Maximum+1)
            {
                this.Dispose();
            }
            progressBar1.Increment(1);
        }



    }
}
