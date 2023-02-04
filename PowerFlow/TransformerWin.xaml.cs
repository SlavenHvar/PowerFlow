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
    /// Interaction logic for TransformerWin.xaml
    /// </summary>
    public partial class TransformerWin : Window
    {
        public TransformerWin()
        {
            InitializeComponent();
            //var item = Data.TransformerDict.First(x => x.Value.IsSelected).Value;
            //var list = new List<double?>() { item.Rd, item.Xd, item.VoltageNN.Real, item.VoltageVN.Real };//only real voltage input
            //if (list.All(x => x != null))
            //{
            //    txtRd.Text = item.Rd.ToString();
            //    txtXd.Text = item.Xd.ToString();
            //    txtUNN.Text = item.VoltageNN.ToString();
            //    txtUVN.Text = item.VoltageVN.ToString();
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Data.TransformerDict.First(x => x.Value.IsSelected).Value;
          
            item.Rd = double.Parse(txtRd.Text.Replace('.', ','));
            item.Xd = double.Parse(txtXd.Text.Replace('.', ','));
            item.VoltageNN = double.Parse(txtUNN.Text.Replace('.', ','));
            item.VoltageVN = double.Parse(txtUVN.Text.Replace('.', ','));
            Close();
        }
    }
}
