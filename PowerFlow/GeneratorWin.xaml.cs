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
    /// Interaction logic for GeneratorWin.xaml
    /// </summary>
    public partial class GeneratorWin : Window
    {
        public GeneratorWin()
        {
            InitializeComponent();
            GenTypesCombo.ItemsSource = Data.GeneratorTypes;
            var item = Data.GeneratorDict.First(x => x.Value.IsSelected).Value;
            var list = new List<double?>() { item.Pg, item.Pgmin, item.Pgmax, item.Qgmin, item.Qgmax };
            if (list.All(x=>x!=null))
            {
                txtPg.Text = (item.Pg*1E-6).ToString();
                txtPgmin.Text = (item.Pgmin * 1E-6).ToString();
                txtPgmax.Text = (item.Pgmax * 1E-6).ToString();
                txtQgmin.Text = (item.Qgmin * 1E-6).ToString();
                txtQgmax.Text = (item.Qgmax * 1E-6).ToString();
                GenTypesCombo.Text = item.GeneratorType;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Data.GeneratorDict.First(x => x.Value.IsSelected).Value;

            item.Pg = double.Parse(txtPg.Text.Replace('.', ','))*1E6;
            item.Pgmin = double.Parse(txtPgmin.Text.Replace('.', ',')) * 1E6;
            item.Pgmax = double.Parse(txtPgmax.Text.Replace('.', ',')) * 1E6;
            item.Qgmin = double.Parse(txtQgmin.Text.Replace('.', ',')) * 1E6;
            item.Qgmax = double.Parse(txtQgmax.Text.Replace('.', ',')) * 1E6;
            item.GeneratorType = GenTypesCombo.Text;

            Close();
        }
    }
}
