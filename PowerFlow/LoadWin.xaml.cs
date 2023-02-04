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
using System.Numerics;

namespace PowerFlow
{
    /// <summary>
    /// Interaction logic for LoadWin.xaml
    /// </summary>
    public partial class LoadWin : Window
    {
        public LoadWin()
        {
            InitializeComponent();
            //treba loadad već unesene podatke ako su uneseni
            var item = Data.LoadDict.First(x => x.Value.IsSelected).Value;
            if(item.P!=null || item.Q!=null)
            {
                txtP.Text = (item.P*1E-6).ToString();
                txtQ.Text = (item.Q*1E-6).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Data.LoadDict.First(x => x.Value.IsSelected).Value;
            item.P = double.Parse(txtP.Text.Replace('.', ','))*1E6;
            item.Q = double.Parse(txtQ.Text.Replace('.', ','))*1E6;

            item.Sp = new Complex((double)item.P, (double)item.Q);

            Close();
        }
    }
}
