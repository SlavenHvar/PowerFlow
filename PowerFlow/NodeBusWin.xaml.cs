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
using System.Windows.Shapes;

namespace PowerFlow
{
    /// <summary>
    /// Interaction logic for NodeBusWin.xaml
    /// </summary>
    public partial class NodeBusWin : Window
    {
        public NodeBusWin()
        {
            InitializeComponent();
            var item = Data.NodeBusDict.First(x => x.Value.IsSelected).Value;
            if (item.Voltage != null || item.VoltageAngle != null)
            {
                txtVoltage.Text = (item.VoltageAmplitude*1E-3).ToString();
                txtVoltageAngle.Text = item.VoltageAngle.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Data.NodeBusDict.First(x => x.Value.IsSelected).Value;
            item.VoltageAmplitude = double.Parse(txtVoltage.Text.Replace('.', ','))*1E3;
            item.VoltageAngle = double.Parse(txtVoltageAngle.Text.Replace('.', ','));

            Close();
        }
    }
}
