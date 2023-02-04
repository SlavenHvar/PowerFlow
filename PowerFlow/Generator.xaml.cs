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
    /// Interaction logic for Generator.xaml
    /// </summary>
    public partial class Generator : UserControl
    {
        public Generator()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            transformGroup = new TransformGroup();
            Connected = false;
            IsSelected = false;
            //Pg = null;
            //Pgmin = null;
            //Pgmax = null;
            //Qgmin = null;
            //Qgmax = null;
        }

        public TranslateTransform transform { get; set; }//variable that helps moving elements
        public bool Connected { get; set; }
        public bool IsSelected { get; set; }//
        public int Angle { get; set; }
        public TranslateTransform translateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public RotateTransform rotateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public TransformGroup transformGroup { get; set; }
        public string GeneratorType { get; set; } 
        public int NodeKey { get; set; }

        //Calculation data
        public double? Pg { get; set; }
        public double? Pgmin { get; set; }
        public double? Pgmax { get; set; }
        public double? Qgmin { get; set; }
        public double? Qgmax { get; set; }

        public double PgB { get; set; }
        public double PgminB { get; set; }
        public double PgmaxB { get; set; }
        public double QgminB { get; set; }
        public double QgmaxB { get; set; }
        //public Complex V { get; set; }

        public Complex QgB { get; set; }//računa se u iteraciji

        public Complex Sg { get; set; }
        public Complex SgB { get; set; }

        public Complex SgOutput { get; set; }//snaga koju regel daje u mrežu
        public Complex SgOutputB { get; set; }//snaga koju regel daje u mrežu

        public bool BoundriesOK { get; set; }//da li je isporučena snaga veća od one koju regulacijska el može predat mreži

        public Complex BaseVoltage { get; set; }
       





    }
}
