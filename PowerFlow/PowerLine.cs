using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Numerics;
namespace PowerFlow
{
    public class PowerLine : UserControl
    {
        public PowerLine()
        {
            PowerLineList = new List<Line>();
            IsSelected = false;
        }

        public int NodeStartKey { get; set; }

        public int NodeEndKey { get; set; }

        public List<Line> PowerLineList { get; set; }

        public bool IsSelected { get; set; }

        public Complex Zu { get; set; }

        public Complex Yu { get; set; }

        public Complex Yp { get; set; }

        //calcdata
        public double? lv { get; set; }
        public double? Rd { get; set; }
        public double? Xd { get; set; }
        public double? Gd { get; set; }
        public double? Bd { get; set; }

        public Complex S { get; set; }//12//snaga koja teče vodom
        public Complex SB { get; set; }//snaga svedena na baznu snagu

        public Complex Srev { get; set; }//21//snaga iz drugog smjera
        public Complex SrevB { get; set; }//21

        public Complex Sloss { get; set; }
        public Complex SlossB { get; set; }

        public double? BaseVoltage { get; set; }

    } 
}
