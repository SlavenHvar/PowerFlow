using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace PowerFlow
{
    /// <summary>
    /// Interaction logic for Transformer.xaml
    /// </summary>
    public partial class Transformer : UserControl
    {
        public Transformer()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            transformGroup = new TransformGroup();
            ConnectedConVN = false;
            ConnectedConNN = false;
            IsSelected = false;
            //Pg = null;
            //Pgmin = null;
            //Pgmax = null;
            //Qgmin = null;
            //Qgmax = null;
        }

        public TranslateTransform transform { get; set; }//variable that helps moving elements
        public bool ConnectedConVN { get; set; }
        public bool ConnectedConNN{ get; set; }
        public bool IsSelected { get; set; }//
        public int Angle { get; set; }
        public TranslateTransform translateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public RotateTransform rotateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public TransformGroup transformGroup { get; set; }
        //public string GeneratorType { get; set; }
        public int NodeKeyVN { get; set; }
        public int NodeKeyNN { get; set; }

        public Complex Zu { get; set; }

        public Complex Yu { get; set; }

        public Complex Yp { get; set; }

        //calcdata
        public double? Rd { get; set; }
        public double? Xd { get; set; }

        public Complex S { get; set; }//12//snaga koja teče vodom
        public Complex SB { get; set; }//12

        public double? BaseVoltage { get; set; }

        public double? VoltageNN { get; set; }
        public double? VoltageVN { get; set; }


        //public Complex Srev { get; set; }//21//snaga iz drugog smjera
        //public Complex SrevB { get; set; }//21

        //public Complex Sloss { get; set; }
        //public Complex SlossB { get; set; }

        ////Calculation data
        //public double? Pg { get; set; }
        //public double? Pgmin { get; set; }
        //public double? Pgmax { get; set; }
        //public double? Qgmin { get; set; }
        //public double? Qgmax { get; set; }

        //public double PgB { get; set; }
        //public double PgminB { get; set; }
        //public double PgmaxB { get; set; }
        //public double QgminB { get; set; }
        //public double QgmaxB { get; set; }
        ////public Complex V { get; set; }


    }
}
