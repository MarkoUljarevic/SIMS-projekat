﻿using Hospital.Controller;
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

namespace Hospital.View.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerHomeWindow.xaml
    /// </summary>
    public partial class ManagerHomeWindow : Window
    {
        private App _app;
 
        public ManagerHomeWindow()
        {
            _app = Application.Current as App;
            InitializeComponent();
        }

        private void EquipmentClick(object sender, RoutedEventArgs e)
        {
            View.ManagerView.EquipmentWindow equipmentWindow = new View.ManagerView.EquipmentWindow(_app._equipmentController);  
            equipmentWindow.Show();
            Close();
        }
        private void RoomClick (object sender, RoutedEventArgs e)
        {

            View.ManagerView.ManagerWindow roomWindow = new View.ManagerView.ManagerWindow(_app._roomController);
            roomWindow.Show();
            Close();
        }

        private void OccupancyClick (object sender, RoutedEventArgs e)
        { 
             View.ManagerView.RoomOccupancy roomOccupancy = new View.ManagerView.RoomOccupancy(_app._appointmentController);
            roomOccupancy.Show();
            Close();
        }

    }
}