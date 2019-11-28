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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool ParseInputs(string Btn, int table, string input1,string input2 ,string input3 ,string input4,string input5,string input6)
        {
            bool Final = false;

            if (Btn == "Submit")
            {
                switch (table)
                {
                    case 1://fuel
                        Final = input2 != null && input1 != null;
                        break;
                    case 2://journey
                        Final = input2 != null && input1 != null;
                        break;
                    case 3://services
                        Final = input2 != null && input1 != null;
                        break;
                    default://vehicles
                        Final = input3 != null && input6 != null && !(input3.Length > 4 || input6.Length > 10 || input2.Length > 128 || input4.Length > 16 || input5.Length > 8 || input1.Length > 64);
                        if (input3.Length > 4 || input6.Length > 10 || input2.Length > 128 || input4.Length > 16 || input5.Length > 8 || input1.Length > 64)
                        { MessageBox.Show("One or more of you Text boxes has too many Charactors"); }
                        break;
                }

                return Final;
            }
            if (Btn == "Delete" || Btn == "Mouse")
            {
                return input1 != null;
            }

            return Final;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
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

            if (table == "vehicles")
            {
                if (ParseInputs("Submit",tableNumb,ManufacturerTxt.Text,ModelTxt.Text,MakeYearTxt.Text,RegoTxt.Text,FuelTxt.Text,TankCapTxt.Text))
                {
                    int MY;
                    int.TryParse(MakeYearTxt.Text, out MY);

                    int TankCap;
                    int.TryParse(TankCapTxt.Text, out TankCap);

                    if (TankCap != 0 && MY != 0)
                    {
                        string checkdupes = string.Format("SELECT * FROM {0} WHERE registration = '{1}' AND vehicle_id = '{2}'", table, RegoTxt.Text, Selected);
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
                }
                Reset();
            }

            if (table == "journey")
            {
                if (ManufacturerTxt.Text != null && StartCalandercal.SelectedDate != null)
                {
                    DateTime dateTime = Convert.ToDateTime(StartCalandercal.Text);
                    string date = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string checkdupes = string.Format("SELECT journey_at FROM {0} WHERE journey_at = '{1}' AND vehicle_id = '{2}'", table, date, Selected);
                    using (connection = new MySqlConnection(CONNSTR))
                    {
                        bool Duplicates = false;
                        Console.WriteLine(Duplicates);
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
                            int Distance;
                            int.TryParse(ManufacturerTxt.Text, out Distance);
                            sql = string.Format("INSERT INTO `{3}` (`vehicle_id`,`distance`,`journey_at`) VALUES ({0},{1},'{2}')",
                                Selected, Distance, date, table);

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
                Reset();
            }

            if (table == "services")
            {
                if (StartCalandercal != null && ManufacturerTxt.Text != null)
                {
                    DateTime dateTime = Convert.ToDateTime(StartCalandercal.Text);
                    string date = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    int.TryParse(ManufacturerTxt.Text, out int odo);
                    string checkdupes = string.Format("SELECT * FROM {0} WHERE serviced_at = '{1}' AND vehicle_id = '{2}'", table, date, Selected);

                    using (connection = new MySqlConnection(CONNSTR))
                    {
                        bool Duplicates = false;
                        Console.WriteLine(Duplicates);
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
                            int Distance;
                            int.TryParse(ManufacturerTxt.Text, out Distance);
                            sql = string.Format("INSERT INTO `{3}` (`vehicle_id`,`odometer`,`serviced_at`) VALUES ({0},{1},'{2}')",
                                Selected, odo, date, table);

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
                Reset();
            }

            if (table == "fuel")
            {
                DateTime dateTime = Convert.ToDateTime(StartCalandercal.Text);
                string date = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                int.TryParse(ManufacturerTxt.Text, out int amount);
                double cost = amount * 2.45;

                string checkdupes = string.Format("SELECT * FROM {0} WHERE serviced_at = '{1}' AND vehicle_id = '{2}'", table, date, Selected);
                using (connection = new MySqlConnection(CONNSTR))
                {
                    bool Duplicates = false;
                    Console.WriteLine(Duplicates);
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
                        int Distance;
                        int.TryParse(ManufacturerTxt.Text, out Distance);
                        sql = string.Format("INSERT INTO `{4}` (`vehicle_id`,`amount`,`cost`,`serviced_at`) VALUES ({0},{1},{2},'{3}')",
                            Selected, amount, cost, date, table);

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

            Reset();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            string numb = VehicleList.SelectedItem.ToString() != null ? VehicleList.SelectedItem.ToString() : "";
            if (ParseInputs("Delete", tableNumb, numb, ModelTxt.Text, MakeYearTxt.Text, RegoTxt.Text, FuelTxt.Text, TankCapTxt.Text))
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
            if (ParseInputs("Mouse", tableNumb, VehicleList.SelectedItem.ToString(), ModelTxt.Text, MakeYearTxt.Text, RegoTxt.Text, FuelTxt.Text, TankCapTxt.Text))
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
            Reset();
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

        private void ChangeAddContext()
        {
            switch (tableNumb)
            {
                case 1: //Fuel
                    lblManufacturer.Content = "Amount";
                    modelLbl.Content = "Fuel Date";
                    MYLbl.Content = "Cost";
                    MYLbl.Visibility = Visibility.Visible;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Visible;

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Hidden;
                    MakeYearTxt.IsEnabled = false;
                    StartCalandercal.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Visible;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Visible;
                    TankCapTxt.IsEnabled = false;
                    tankLbl.Content = "Total Filled:";

                    ManufacturerTxt.Text = "";
                    ModelTxt.Text = "";
                    MakeYearTxt.Text = "";
                    StartCalandercal.Text = "";
                    MakeYearTxt.Text = "";
                    RegoTxt.Text = "";
                    FuelTxt.Text = "";
                    TankCapTxt.Text = totalFilled(tableNumb, Selected, CONNSTR);
                    break;

                case 2: //Journey
                    lblManufacturer.Content = "Distance";
                    modelLbl.Content = "Start Journey Day";
                    MYLbl.Visibility = Visibility.Hidden;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Visible;
                    tankLbl.Content = "Total Distance:";

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Hidden;
                    StartCalandercal.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Hidden;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Visible;
                    TankCapTxt.IsEnabled = false;

                    ManufacturerTxt.Text = "";
                    ModelTxt.Text = "";
                    MakeYearTxt.Text = "";
                    StartCalandercal.Text = "";
                    MakeYearTxt.Text = "";
                    RegoTxt.Text = "";
                    FuelTxt.Text = "";
                    TankCapTxt.Text = totalDistance(tableNumb, Selected, CONNSTR);

                    break;

                case 3: //Services
                    lblManufacturer.Content = "Odometer";
                    modelLbl.Content = "Day of Service";
                    MYLbl.Visibility = Visibility.Hidden;
                    RNLbl.Visibility = Visibility.Hidden;
                    fuelLbl.Visibility = Visibility.Hidden;
                    tankLbl.Visibility = Visibility.Hidden;
                    tankLbl.Content = "Last ODO# @ Service:";

                    ManufacturerTxt.Visibility = Visibility.Visible;
                    ModelTxt.Visibility = Visibility.Hidden;
                    StartCalandercal.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Hidden;
                    RegoTxt.Visibility = Visibility.Hidden;
                    FuelTxt.Visibility = Visibility.Hidden;
                    TankCapTxt.Visibility = Visibility.Visible;
                    TankCapTxt.IsEnabled = false;

                    ManufacturerTxt.Text = "";
                    ModelTxt.Text = "";
                    MakeYearTxt.Text = "";
                    StartCalandercal.Text = "";
                    MakeYearTxt.Text = "";
                    RegoTxt.Text = "";
                    FuelTxt.Text = "";
                    TankCapTxt.Text = LastService(tableNumb, Selected, CONNSTR);
                    break;

                default: //Vehicles

                    lblManufacturer.Visibility = Visibility.Visible; ManufacturerTxt.Visibility = Visibility.Visible;
                    modelLbl.Visibility = Visibility.Visible; ModelTxt.Visibility = Visibility.Visible;
                    MakeYearTxt.IsEnabled = true;
                    StartCalandercal.Visibility = Visibility.Hidden;
                    MYLbl.Visibility = Visibility.Visible;
                    MakeYearTxt.Visibility = Visibility.Visible;
                    RegoTxt.Visibility = Visibility.Visible;
                    RNLbl.Visibility = Visibility.Visible; FuelTxt.Visibility = Visibility.Visible;
                    fuelLbl.Visibility = Visibility.Visible; TankCapTxt.Visibility = Visibility.Visible;
                    tankLbl.Visibility = Visibility.Visible;
                    TankCapTxt.IsEnabled = true;

                    lblManufacturer.Content = "Manufacturer";
                    modelLbl.Content = "Model";
                    MYLbl.Content = "Make Year";
                    RNLbl.Content = "Registration Number";
                    fuelLbl.Content = "Fuel Type";
                    tankLbl.Content = "Tank Capacity";

                    ManufacturerTxt.Text = "";
                    ModelTxt.Text = "";
                    MakeYearTxt.Text = "";
                    StartCalandercal.Text = "";
                    MakeYearTxt.Text = "";
                    RegoTxt.Text = "";
                    FuelTxt.Text = "";
                    TankCapTxt.Text = "";
                    break;
            }
        }

        private void Reset()
        {
            tableNumb = 0;
            Selected = 0;
            Filltable(Selected, 0);
            VehicleRbtn.IsChecked = true;
            ChangeAddContext();

        }

        private void ManufacturerTxt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (tableNumb == 1)
            {
                int.TryParse(ManufacturerTxt.Text, out int amount);
                double cost = amount * 2.45;
                MakeYearTxt.Text = cost.ToString();
            }
        }

        public string totalDistance(int tableTag, int ItemSelected, string strconn)
        {
            if (tableTag == 2)
            {
                string Total;
                string table = "journey";
                string checkdupes = string.Format("SELECT SUM(distance) FROM {0} WHERE vehicle_id = {1}", table, ItemSelected);
                using (MySqlConnection connection = new MySqlConnection(strconn))
                {
                    connection.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(checkdupes, connection))
                    {
                        Total = sqlCommand.ExecuteScalar().ToString();
                    }
                    connection.Close();
                    return Total;
                }
            }
            return "Not Available";
        }

        public string LastService(int tableTag, int ItemSelected,string strconn)
        {
            if (tableTag == 3)
            {
                string Total;
                string table = "services";
                string checkdupes = string.Format("SELECT odometer FROM {0} WHERE vehicle_id = {1} ORDER BY serviced_at DESC LIMIT 1", table, ItemSelected);
                using (connection = new MySqlConnection(strconn))
                {
                    connection.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(checkdupes, connection))
                    {
                        Total = sqlCommand.ExecuteScalar().ToString();
                    }
                    connection.Close();
                    return Total;
                }
            }
            return "Not Available";
        }

        public string totalFilled(int tableTag, int ItemSelected, string strconn)
        {
            if (tableTag == 1)
            {
                string Total;
                string table = "fuel";
                string checkdupes = string.Format("SELECT SUM(amount) FROM {0} WHERE vehicle_id = {1}", table, ItemSelected);
                using (connection = new MySqlConnection(strconn))
                {
                    connection.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(checkdupes, connection))
                    {
                        Total = sqlCommand.ExecuteScalar().ToString();
                    }
                    connection.Close();
                    return Total;
                }
            }
            return "Not Available";
        }
    }
}