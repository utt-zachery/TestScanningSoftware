using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Scanning_Software
{
   
    public partial class MainGUI : Form
    {
        private int isSelected = 0;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void select(int toSelect)
        {
            isSelected = toSelect;
            this.students.BackColor = Color.Transparent;
            this.testManager.BackColor = Color.Transparent;
            this.testScanner.BackColor = Color.Transparent;

            if (toSelect == 0) this.students.BackColor = Color.FromArgb(0, 153, 255);
            if (toSelect == 1) this.testManager.BackColor = Color.FromArgb(0, 153, 255);
            if (toSelect == 2) this.testScanner.BackColor = Color.FromArgb(0, 153, 255);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void students_Click_1(object sender, EventArgs e)
        {
            select(0);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            select(0);
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            select(0);
        }

        private void test_Click_1(object sender, EventArgs e)
        {
            select(1);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            select(1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            select(1);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            select(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            select(2);
        }

        private void scanner_Click(object sender, EventArgs e)
        {
            select(2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label80_Click(object sender, EventArgs e)
        {

        }

        private void label78_Click(object sender, EventArgs e)
        {

        }

        private void label76_Click(object sender, EventArgs e)
        {

        }

        private void label77_Click(object sender, EventArgs e)
        {

        }

        private void label79_Click(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

        private void label71_Click(object sender, EventArgs e)
        {

        }

        private void label70_Click(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void label67_Click(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label57_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }
    }
}
