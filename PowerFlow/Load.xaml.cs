using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;

namespace PowerFlow
{
    /// <summary>
    /// Interaction logic for Load.xaml
    /// </summary>
    public partial class Load : UserControl
    {
        public Load()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            transformGroup = new TransformGroup();
            Connected = false;
            IsSelected = false;
            //P = null;//nullable mora bit jer ako pozovem prozor moram napraviti check da li je prvi unos null ili je već uneseno i prikazati unesene podatke
            //Q = null;
        }
        TransformGroup myTransformGroup = new TransformGroup();
        public TranslateTransform transform { get; set; }//variable that helps moving elements
        public bool Connected { get; set; }
        public bool IsSelected { get; set; }
        public int Angle { get; set; }
        public TranslateTransform translateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public RotateTransform rotateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public TransformGroup transformGroup { get; set; }

        public int nodeKey { get; set; }
        //varibles
        public double? P { get; set; }//active power
        public double? Q { get; set; }//reactive power

        public double PpB { get; set; }//P sveden na baznu snagu
        public double QpB { get; set; }

        public Complex Sp { get; set; }
        public Complex SpB { get; set; }

        public Complex BaseVoltage { get; set; }


    }
}
