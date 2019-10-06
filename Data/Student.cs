using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class Student
    {
        public String studentID { get; set; }
        public String studentName { get; set; }

        public Student(String studentID, string studentName)
        {
            this.studentID = studentID;
            this.studentName = studentName;
        }
        
    }
}
