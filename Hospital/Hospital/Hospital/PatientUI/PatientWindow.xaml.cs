﻿using Hospital.Model;
using Prism.Commands;
using Prism.Mvvm;
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
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Drawing;

namespace Hospital.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    /// 

    public partial class PatientWindow : Window
    {

        public AppointmentHandler ah = new AppointmentHandler();

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        
        private DelegateCommand <Appointment> _deleteCommand;
        public DelegateCommand<Appointment> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Appointment>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(Appointment app)
        {
            Appointments.Remove(app);
        }

        public PatientWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Appointments = new ObservableCollection<Appointment>();
            Appointments.Add(new Appointment { Id = 1, Doctor = "Dr Mike Mellow", Date = new DateTime(2022, 12, 5, 7, 0, 0) });
            Appointments.Add(new Appointment { Id = 2, Doctor = "Dr Olivia Wise", Date = new DateTime(2022, 12, 7, 7, 30, 0) });
            Appointments.Add(new Appointment { Id = 3, Doctor = "Dr Mike Mellow", Date = new DateTime(2022, 12, 9, 8, 30, 0) });
            
        }

        private void BookAnAppointmentClick(object sender, RoutedEventArgs e)
        {

            Appointments.Add(new Appointment { Id = 4, Doctor = TextBoxDoctor.Text, Date = calendar.DisplayDate });
            
        }

        private void OpenBookAnAppointmentWindowClick(object sender, RoutedEventArgs e)
        {
            PatientUI.BookAnAppointment bookAnAppointment = new PatientUI.BookAnAppointment(this);
            bookAnAppointment.Show();

        }

        private void EditAnAppointmentClick(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}