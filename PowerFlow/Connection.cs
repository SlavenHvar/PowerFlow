using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PowerFlow
{
    public class Connection : UserControl
    {
        public Connection()
        {
            ConnectionList = new List<Line>();
        }

        //add elemeent info here

        public int Node { get; set; }

        public List<Line> ConnectionList { get; set; }
    }
}
