using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assesment_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Vehicle> vehicles = new List<Vehicle>();

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int MY = int.Parse(MakeYearTxt.Text);
            int TankCap = int.Parse(TankCapTxt.Text);
            int Dist = int.Parse(DistanceTxt.Text);

            Vehicle vehicle = new Vehicle(ManufacturerTxt.Text, ModelTxt.Text, MY, RegoTxt.Text, Dist, TankCap);
            vehicles.Add(vehicle);
        }
    }
}
