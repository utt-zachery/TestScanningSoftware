using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.Data
{
    public class Check_Point
    {
       public int x { get; set; }
        public int y { get; set; }
        public  int quantity { get; set; }

        public Check_Point(int x, int y, int quantity)
        {
            this.x = x;
            this.y = y;
            this.quantity = quantity;
        }
    }
}
