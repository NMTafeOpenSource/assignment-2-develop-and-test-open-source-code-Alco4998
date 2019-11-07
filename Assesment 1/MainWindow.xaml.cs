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

        List<Vehicle> vehicles;

        public MainWindow()
        {
            InitializeComponent();
            vehicles = new List<Vehicle>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VehicleList.ItemsSource = vehicles;

            Vehicle vehicle = new Vehicle("Volkswagon", "Swifty", 0420, "12adf", 12034, 458646);
            vehicles.Add(vehicle);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            if (MakeYearTxt.Text != null || TankCapTxt.Text != null || DistanceTxt.Text != null)
            {
                int MY;
                int.TryParse(MakeYearTxt.Text, out MY);

                int TankCap;
                int.TryParse(TankCapTxt.Text, out TankCap);

                int Dist;
                int.TryParse(DistanceTxt.Text, out Dist);

                if (Dist != 0 || TankCap != 0 || MY != 0)
                {
                    Vehicle vehicle = new Vehicle(ManufacturerTxt.Text, ModelTxt.Text, MY, RegoTxt.Text, Dist, TankCap);
                    vehicles.Add(vehicle);
                }
            }

            VehicleList.Items.Refresh();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(VehicleList.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Vehicle to remove");
            }

            else
            {
                Vehicle Selected = (Vehicle)VehicleList.SelectedItem;
                vehicles.Remove(Selected);
            }

            VehicleList.Items.Refresh();
        }
    }
}
