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
    /// Interaction logic for SettingsWin.xaml
    /// </summary>
    public partial class SettingsWin : Window
    {
        public SettingsWin()
        {
            InitializeComponent();
            var list = new List<double?>() { Data.SB, Data.UB, Data.EpsRe, Data.EpsIm };
            if (list.All(x => x != null))
            {
                txtSB.Text = (Data.SB * 1E-6).ToString();
                txtUB.Text = (Data.UB * 1E-3).ToString();
                txtEpsRe.Text = Data.EpsRe.ToString();
                txtEpsIm.Text = Data.EpsIm.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Data.SB = double.Parse(txtSB.Text.Replace('.', ','))*1E6;//MVA
            Data.UB = double.Parse(txtUB.Text.Replace('.', ','))*1E3;//kV
            Data.EpsRe = double.Parse(txtEpsRe.Text.Replace('.', ','));
            Data.EpsIm = double.Parse(txtEpsIm.Text.Replace('.', ','));


            Close();
        }
    }
}
