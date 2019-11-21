﻿using MySql.Data.MySqlClient;
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

                await Task.Delay(1);

                if (!Select.Equals(0))
                {
                    sql += " WHERE id = " + Select;
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

                    //VehicleList.Items.Refresh();

                }

            }
            catch (Exception ex)
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

                        sql = string.Format("INSERT INTO `{6}` (`make`,`model`,`make-Year`,`registration`,`fuel`,`tank-Size`) VALUES ('{0}','{1}',{2},'{3}','{4}',{5})",
                            ManufacturerTxt.Text, ModelTxt.Text, MY, RegoTxt.Text, FuelTxt.Text, TankCapTxt.Text, table);
                        Console.WriteLine(sql);
                        using (connection = new MySqlConnection(CONNSTR))
                        {
                            connection.Open();
                            using (MySqlCommand sqlCommand = new MySqlCommand(sql, connection))
                            {
                                sqlCommand.ExecuteNonQuery();
                            }
                            connection.Close();
                            Filltable(0, 0);
                        }
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
                        sqlCommand.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                Filltable(0, 0);
            }
        }

        private void VehicleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (!VehicleList.SelectedItem.Equals(null))
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
    }
}
