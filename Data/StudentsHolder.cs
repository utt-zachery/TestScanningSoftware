using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    class StudentsHolder
    {
        private List<Student> roster;

        public StudentsHolder(List<Student> roster)
        {
            this.roster = roster;
        }

 
    }
}
