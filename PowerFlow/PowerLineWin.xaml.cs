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
    /// Interaction logic for PowerLineWin.xaml
    /// </summary>
    public partial class PowerLineWin : Window
    {
        public PowerLineWin()
        {
            InitializeComponent();
            var item = Data.PowerLineDict.First(x => x.Value.IsSelected).Value;
            var list = new List<double?>() { item.lv, item.Rd, item.Xd, item.Gd, item.Bd };
            if (list.All(x => x != null))
            {
                txtlv.Text = item.lv.ToString();
                txtRd.Text = item.Rd.ToString();
                txtXd.Text = item.Xd.ToString();
                txtGd.Text = item.Gd.ToString();
                txtBd.Text = item.Bd.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Data.PowerLineDict.First(x => x.Value.IsSelected).Value;
            item.lv = double.Parse(txtlv.Text.Replace('.', ','));
            item.Rd = double.Parse(txtRd.Text.Replace('.', ','));
            item.Xd = double.Parse(txtXd.Text.Replace('.', ','));
            item.Gd = double.Parse(txtGd.Text.Replace('.', ','));
            item.Bd = double.Parse(txtBd.Text.Replace('.', ','));

            Close();
        }
    }
}
