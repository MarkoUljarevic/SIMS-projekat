﻿using Hospital.Controller;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EquipmentWindow.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        private App _app;
        private readonly object _content;
        private readonly EquipmentController _equipmentController;
        private readonly RoomController _roomController;

        public EquipmentWindow(EquipmentController equipmentController)
        {
            _app = Application.Current as App;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            _content = Content;
            _equipmentController = equipmentController;
            dataGridEquipment.ItemsSource = _equipmentController.Read();
            ObservableCollection<Equipment> equipments = _app._equipmentController.Read();
            ObservableCollection<String> roomNames = new ObservableCollection<string>();
            foreach (Equipment equipment in equipments)
            {
                roomNames.Add(equipment.Room);
            }
            eqComboBox.ItemsSource = roomNames;
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var filteredList = _equipmentController.Read().Where(x => x.Name.ToLower().Contains(tbx.Text.ToLower()));
                dataGridEquipment.ItemsSource = null;
                dataGridEquipment.ItemsSource = filteredList;
            }
            else
            {
                dataGridEquipment.ItemsSource = _equipmentController.Read();
            } 
                
        }
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            var tbx = sender as ComboBox;
            if (tbx.SelectedItem != null)
            {
                var filteredList = _equipmentController.Read().Where(x => x.Room.Contains((string)tbx.SelectedItem));
                dataGridEquipment.ItemsSource = null;
                dataGridEquipment.ItemsSource = filteredList;
            }
            else
            {
                dataGridEquipment.ItemsSource = _equipmentController.Read();
            }

        }

        private void RelocationClick(object sender, RoutedEventArgs e)
        {
            Equipment equipment = dataGridEquipment.SelectedItem as Equipment;
            var editEquipment = new Relocation(equipment, this, _equipmentController, _roomController);
            Content = editEquipment;
        }
        public void BackToEquipmentWindow()
        {
            Content = _content;
            refresh();
        }

        public void refresh()
        {
            dataGridEquipment.ItemsSource = _equipmentController.Read();
        }
        private void EquipmentClick(object sender, RoutedEventArgs e)
        {
            View.ManagerView.EquipmentWindow equipmentWindow = new View.ManagerView.EquipmentWindow(_app._equipmentController);
            equipmentWindow.Show();
            Close();
        }
        private void RoomClick(object sender, RoutedEventArgs e)
        {

            View.ManagerView.ManagerWindow roomWindow = new View.ManagerView.ManagerWindow(_app._roomController);
            roomWindow.Show();
            Close();
        }

        private void OccupancyClick(object sender, RoutedEventArgs e)
        {
            View.ManagerView.RoomOccupancy roomOccupancy = new View.ManagerView.RoomOccupancy(_app._appointmentController);
            roomOccupancy.Show();
            Close();
        }
        private void HomeClick(object sender, RoutedEventArgs e)
        {
            View.ManagerView.ManagerHomeWindow managerHomeWindow = new ManagerHomeWindow();
            managerHomeWindow.Show();
            Close();
        }
        private void MedicineClick(object sender, RoutedEventArgs e)
        {
            MedicineWindow medicine = new MedicineWindow(_app._medicineController);
            medicine.Show();
            Close();
        }
        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
            Close();
        }
    }
}
