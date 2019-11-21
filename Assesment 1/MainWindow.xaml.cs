using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Assesment_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Vehicle> vehicles;
        private int Selected;
        private int tableNumb;
        private bool related;

        private const string CONNSTR =
                "server=localhost;" +
                "user=AJC_Car_asses1;" +
                "database=ajc_car_asses1;" +
                "port=3306;" +
                "password=A*crewdev;";

        private MySqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            vehicles = new List<Vehicle>();
            Filltable(Selected, tableNumb);

        }



        private async void Filltable(int Select, int tableNumb)
        {


            try
            {
                string sql = "SELECT * FROM ";
                switch (tableNumb)
                {
                    case 1:
                        sql += " fuel ";
                        related = true;
                        break;
                    case 2:
                        sql += " journey ";
                        related = true;
                        break;
                    case 3:
                        sql += " services ";
                        related = true;
                        break;
                    default:
                        sql += " vehicles ";
                        related = false;
                        break;
                }

                await Task.Delay(1);

                if (!Select.Equals(0))
                {
                    string filteroption = related ? "vehicle_id" : "id";
                    sql += " WHERE " + filteroption + " = " + Select;
                }

                using (connection = new MySqlConnection(CONNSTR))
                {
                    connection.Open();

                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                        da.Fill(dt);
                        VehicleList.DataContext = dt;

                    }
                    connection.Close();

                    ChangeAddContext();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (MakeYearTxt.Text != null ||  TankCapTxt.Text != null)
            {
                if (MakeYearTxt.Text.Length > 4 || TankCapTxt.Text.Length > 10 || ModelTxt.Text.Length > 128 || RegoTxt.Text.Length > 16 || FuelTxt.Text.Length > 8 || ManufacturerTxt.Text.Length > 64)
                { MessageBox.Show("One or more of you Text boxes has too many Charactors"); }

                int MY;
                int.TryParse(MakeYearTxt.Text, out MY);

                int TankCap;
                int.TryParse(TankCapTxt.Text, out TankCap);

                if (TankCap != 0 || MY != 0)
                {
                    string sql;
                    string table = "";
                    switch (tableNumb)
                    {
                        case 1:
                            table += "fuel";
                            break;
                        case 2:
                            table += "journey";
                            break;
                        case 3:
                            table += "services";
                            break;
                        default:
                            table += "vehicles";
                            break;
                    }

                    if (table == "vehicles") {

                        string checkdupes = string.Format("SELECT * FROM {0} WHERE registration = '{1}'", table, RegoTxt.Text);
                        using (connection = new MySqlConnection(CONNSTR))
                        {
                            bool Duplicates = false;
                            connection.Open();
                            using (MySqlCommand sqlCommand = new MySqlCommand(checkdupes, connection))
                            {
                                DataTable dt = new DataTable();
                                MySqlDataAdapter da = new MySqlDataAdapter(sqlCommand);
                                da.Fill(dt);
                                Duplicates = dt.Rows.Count > 0 ? true : false;
                            }

                            if (!Duplicates)
                            {

                                sql = string.Format("INSERT INTO `{6}` (`make`,`model`,`make-Year`,`registration`,`fuel`,`tank-Size`) VALUES ('{0}','{1}',{2},'{3}','{4}',{5})",
                                    ManufacturerTxt.Text, ModelTxt.Text, MY, RegoTxt.Text, FuelTxt.Text, TankCapTxt.Text, table);

                                Console.WriteLine(sql);

                                using (MySqlCommand sqlCommand = new MySqlCommand(sql, connection))
                                {
                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                            connection.Close();
                            Filltable(0, 0);
                        }
                    }

                    if (table == "journey")
                    {

                    }
                }

            }

            VehicleList.Items.Refresh();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!VehicleList.SelectedItem.Equals(null))
            {
                DataRowView dr = VehicleList.SelectedItem as DataRowView;

                int dr1;
                int.TryParse(dr[0].ToString(), out dr1);
                Selected = dr1;

                string sql = "Delete FROM ";
                switch (tableNumb)
                {
                    case 1:
                        sql += " fuel ";
                        break;
                    case 2:
                        sql += " journey ";
                        break;
                    case 3:
                        sql += " services ";
                        break;
                    default:
                        sql += " vehicles ";
                        break;
                }
                sql += "WHERE id = " + dr1;
                using (connection = new MySqlConnection(CONNSTR))
                {
                    connection.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(sql, connection))
                    {
                        try { sqlCommand.ExecuteNonQuery(); }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    }
                    connection.Close();
                }

                Filltable(0, 0);
            }
        }

        private void VehicleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                if (VehicleList.SelectedItem != null)
                {
                    DataRowView dr = VehicleList.SelectedItem as DataRowView;

                    int dr1;
                    int.TryParse(dr[0].ToString(), out dr1);

                    Selected = dr1;

                    Debug.Print(dr[0].ToString());
                }

                Filltable(Selected, tableNumb);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

            tableNumb = 0;
            Selected = 0;
            Filltable(Selected, 0);
            VehicleRbtn.IsChecked = true;

        }

        private void ServiceRbtn_Checked(object sender, RoutedEventArgs e)
        {
            tableNumb = 3;
        }

        private void JourneyRbtn_Checked(object sender, RoutedEventArgs e)
        {
            tableNumb = 2;
        }

        private void FuelRbtn_Checked(object sender, RoutedEventArgs e)
        {
            tableNumb = 1;
        }

        private void VehicleRbtn_Checked(object sender, RoutedEventArgs e)
        {
            tableNumb = 0;
        }

        private void VehicleList_MouseEnter(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(true);
        }
        
        private void ChangeAddContext()
        {
            switch(tableNumb)
            {
                case 1:
                    lblManufacturer.Content = "Amount";
                    modelLbl.Content = "Cost";
                    MYLbl.Visibility = Visibility.Hidden;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Hidden;

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Visible;
                    Calandarcal.Visibility = Visibility.Hidden;
                    MakeYearTxt.Visibility = Visibility.Hidden;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Hidden;
                    break;

                case 2:
                    lblManufacturer.Content = "Distance";
                    modelLbl.Content = "Journey day";
                    MYLbl.Visibility = Visibility.Hidden;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Hidden;

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Hidden;
                    Calandarcal.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Hidden;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Hidden;
                    break;

                case 3:
                    lblManufacturer.Content = "Odometer";
                    modelLbl.Content = "Day of Service";
                    MYLbl.Visibility = Visibility.Hidden;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Hidden;

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Hidden;
                    Calandarcal.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Hidden;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Hidden;
                    break;

                default:

                    lblManufacturer.Visibility = Visibility.Visible;  ManufacturerTxt.Visibility = Visibility.Visible;
                    modelLbl.Visibility = Visibility.Visible;         ModelTxt.Visibility = Visibility.Visible;
                                                                      Calandarcal.Visibility = Visibility.Hidden;
                    MYLbl.Visibility = Visibility.Visible;            MakeYearTxt.Visibility = Visibility.Visible;
                    RNLbl.Visibility = Visibility.Visible;            RegoTxt.Visibility = Visibility.Visible;
                    fuelLbl.Visibility = Visibility.Visible;          FuelTxt.Visibility = Visibility.Visible;
                    tankLbl.Visibility = Visibility.Visible;          TankCapTxt.Visibility = Visibility.Visible;

                    lblManufacturer.Content = "Manufacturer";
                    modelLbl.Content = "Model";
                    MYLbl.Content = "Make Year";
                    RNLbl.Content = "Registration Number";
                    fuelLbl.Content = "Fuel Type";
                    tankLbl.Content = "Tank Capacity";
                    break;
            }
        }
    }
}
