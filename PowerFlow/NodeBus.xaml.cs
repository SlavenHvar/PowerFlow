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
    /// Interaction logic for NodeBus.xaml
    /// </summary>
    public partial class NodeBus : UserControl
    {
        public NodeBus()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            transformGroup = new TransformGroup();
            PLineNodeKeyList = new List<int>();
            NodeGenKeyList = new List<int>();
            NodeLoadKeyList = new List<int>();
            ConnectedPowerLineKeys = new List<int>();
            ConnectedTransformerKeysDict = new Dictionary<int, string>();
            IsSelected = false;//mislin da ne treba inicializirat da je default false
            //Voltage = null;
            //VoltageAngle = null;//možda je već automastki setano
        }
        //vjerojatno nepotrebno
        public List<int> PLineNodeKeyList { get; set; }//list that saves values of dict keys of connected nodes to this node
        
        //
        public List<int> NodeGenKeyList { get; set; }//list that saves values of dict keys of connected generators to this node
        public List<int> NodeLoadKeyList { get; set; }//list that saves values of dict keys of connected loads to this node
        //


        public List<int> ConnectedPowerLineKeys{ get; set; }
        public Dictionary<int, string> ConnectedTransformerKeysDict { get; set; }
        //render transform
        public TranslateTransform translateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public RotateTransform rotateTransform { get; set; }//UserControl_MouseMove_NodeBus
        public TransformGroup transformGroup { get; set; }
        public bool IsSelected { get; set; }//
        public int Angle { get; set; }
        //Calc Data
        public double? VoltageAngle { get; set; }
        public double? VoltageAmplitude { get; set; }

        public Complex Voltage { get; set; }
        public Complex VoltageB { get; set; }//napon sveden na baznu vrijednost

        public double? BaseVoltage { get; set; }//napon sveden na baznu vrijednost

        public Complex Current { get; set; }
        public Complex CurrentB { get; set; }

        public Complex SB { get; set; }//snaga čvora
        public Complex SpB { get; set; }//snaga čvora
        public Complex SgB { get; set; }//snaga čvora

        public Complex Snode { get; set; }//snaga čvora
        public Complex SnodeB { get; set; }//snaga čvora

       

    }
}
