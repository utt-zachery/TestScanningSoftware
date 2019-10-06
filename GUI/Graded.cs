using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Scanning_Software.Data;
using Test_Scanning_Software.Exam_Sections;
using Test_Scanning_Software.PhotoScan;

namespace Test_Scanning_Software.GUI
{
    public partial class Graded : Form
    {
        public Graded()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button1.Enabled = false;
                this.textBox1.Text = this.openFileDialog1.FileName;
                ExamSpecifics APBiologyExam = new ExamSpecifics(true, "AP Biology Exam", 0, "AAAAAAAAAAAAAAAAAAAA", 1, "AAAAAAAAAA", 1, "TTTTTTTTTT", 1, "1111111111", 1, 1);
                
                Bitmap input = new Bitmap(Image.FromFile(this.openFileDialog1.FileName));
                BubbleSheet scanned = FullScan.scanSheet(input, APBiologyExam);
                this.label1.Text = "Total Score: " + scanned.score;

                Bitmap overimage = new Bitmap(scanned.correctAnswerOverylay.Width, scanned.correctAnswerOverylay.Height);
                Graphics g = Graphics.FromImage(overimage);
                g.DrawImage(scanned.baseSheet, new Point(0, 0));
                g.DrawImage(scanned.correctAnswerOverylay, new Point(0,0));
                this.pictureBox1.Image = overimage;
                this.button1.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
