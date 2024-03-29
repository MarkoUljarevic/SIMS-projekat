﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using Hospital.Controller;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Hospital.View.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>


    public partial class AddPatient : Page
    {
        private App _app;
        private readonly SecretaryWindow _secretaryWindow;
        private List<int> allergies;
        private ObservableCollection<Allergen> _allergens;
        public AddPatient(SecretaryWindow secretaryWindow)
        {
            _app = Application.Current as App;
            InitializeComponent();
            var allergens = _app._allergenController.Read();
            _allergens = new ObservableCollection<Allergen>(allergens);
            AllergenListBox.ItemsSource = allergens;
            _secretaryWindow = secretaryWindow;

        }

        private void AddPatientOnClick(object sender, RoutedEventArgs e)
        {
            Country tempCountry = new Country(countryText.Text);
            _app._countryController.Create(tempCountry);
            City tempCity = new City(cityText.Text, zipText.Text, tempCountry);
            _app._cityController.Create(tempCity);
            Address tempAddress = new Address(streetText.Text, numberText.Text, tempCity);
            _app._addressController.Create(tempAddress);
            User user = new User(nameText.Text, lastNameText.Text, idNumberText.Text, usernameText.Text,
                passwordText.Text, tempAddress, phoneText.Text, emailText.Text, "patient",
                (DateTime)datePicker.SelectedDate);
            var patientAllergies = new List<int>();
            foreach (Allergen allergen in AllergenListBox.SelectedItems)
            {
                patientAllergies.Add(allergen.Id);
            }
            _app._userController.Create(user);
            MedicalRecord record = new MedicalRecord(chronicalDiseaseText.Text, patientAllergies);
            _app._medicalRecordController.Create(record);
            Patient patient = new Patient(user, genderText.Text, bloodTypeText.Text, healthInsuranceIdText.Text, record);
            _app._patientController.Create(patient);
            _secretaryWindow.BackToSecretaryWindow();
        }

        private void CancelPatientOnClick(Object sender, RoutedEventArgs e)
        {
            _secretaryWindow.BackToSecretaryWindow();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _secretaryWindow.BackToSecretaryWindow();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            _secretaryWindow.Close();
        }

        private void AddAllergen_Click(object sender, RoutedEventArgs e)
        {
            if (AllergiesText.Text.Trim().Equals(""))
            {
                MessageBox.Show("Allergy can't have no name");
                return;
            }
            Allergen allergen = new Allergen(AllergiesText.Text);
            _app._allergenController.Create(allergen);
            AllergiesText.Text = "";
            AllergenListBox.Items.Refresh();
            AllergenListBox.ItemsSource = _allergens;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            _secretaryWindow.Close();
        }
    }
}
