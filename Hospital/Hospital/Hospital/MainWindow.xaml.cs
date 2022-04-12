﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void DoctorClick(object sender, RoutedEventArgs e) {
            Doctor.DoctorWindow doctorWindow = new Doctor.DoctorWindow();
            doctorWindow.Show();
        }
        private void ManagerClick(object sender, RoutedEventArgs e) {
            Manager.ManagerWindow managerWindow = new Manager.ManagerWindow();
            managerWindow.Show();
        }
        private void SecretaryClick(object sender, RoutedEventArgs e) {
            Secretary.SecretaryWindow secretaryWindow = new Secretary.SecretaryWindow();
            secretaryWindow.Show();
        }
        private void PatientClick(object sender, RoutedEventArgs e) {
            PatientUI.PatientWindow patientWindow = new PatientUI.PatientWindow();
            patientWindow.Show();
        }
    }
}