using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Model;
using Hospital.Repository;
using Xceed.Wpf.Toolkit;

namespace Hospital.Service
{
    public class AppointmentService
    {
        private int _id;
        private readonly AppointmentRepository _repository;

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            _repository = appointmentRepository;
            List<Appointment> appointments = Read();
            if (appointments.Count == 0)
            {
                _id = 0;
            }
            else
            {
                _id = appointments.Last().Id;
            }
        }

        public Appointment ReadById(int id)
        {
            return _repository.ReadById(id);
        }

        public void Create(Appointment newAppointment)
        {
            var appointments = Read();
            foreach (var appointment in appointments)
            {
                var startTime = appointment.Date;
                var tempTime = startTime;
                var endTime = tempTime.AddMinutes(appointment.Duration.Hours * 60 + appointment.Duration.Minutes);

                var appointmentStarTime = newAppointment.Date;
                var appointmentTempTime = appointmentStarTime;
                var appointmentEndTime = appointmentTempTime.AddMinutes(newAppointment.Duration.Hours * 60 +
                                                                        newAppointment.Duration.Minutes);

                if (DateTime.Compare(newAppointment.Date, startTime) > 0 && DateTime.Compare(newAppointment.Date, endTime) < 0)
                {
                    MessageBox.Show("There is already an appointment at the selected time!");
                    return;
                }

                if (DateTime.Compare(appointmentEndTime, appointment.Date) > 0 && DateTime.Compare(appointmentEndTime, endTime) < 0)
                {
                    MessageBox.Show("There is already an appointment at the selected time!");
                    return;
                }
            }
            newAppointment.Id = GenerateId();
            _repository.Create(newAppointment);
        }

        public void Edit(Appointment editAppointment)
        {
            _repository.Edit(editAppointment);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Appointment> Read()
        {
            return _repository.Read();
        }
        public List<Appointment> ReadByDoctorId(int doctorId)
        {
            return _repository.ReadByDoctorId(doctorId);
        }

        public List<Appointment> ReadByDateAndNotDoctor(int doctorId, DateTime date)
        {
            return _repository.ReadByDateAndNotDoctor(doctorId, date);
        }

        private int GenerateId()
        {
            return ++_id;
        }

        public List<Appointment> ReadPastAppointments(int patientId)
        {
            List<Appointment> pastAppointments = new List<Appointment>();

            List<Appointment> allAppointments = _repository.ReadByPatientId(patientId);
            foreach (Appointment a in allAppointments)
            {
                if (a.Date < DateTime.Now)
                {
                    pastAppointments.Add(a);
                }
            }
            return pastAppointments;
        }

        public List<Appointment> ReadFutureAppointments(int patientId)
        {
            List<Appointment> futureAppointments = new List<Appointment>();

            List<Appointment> allAppointments = _repository.ReadByPatientId(patientId);
            foreach (Appointment a in allAppointments)
            {
                if (a.Date > DateTime.Now)
                {
                    futureAppointments.Add(a);
                }
            }
            return futureAppointments;
        }

        public List<Appointment> FindAvailabeAppointments(Doctor selectedDoctor, DateTime _date, List<Appointment> DoctorsAppointments,
            List<TimeSpan> hospitalWorkingHours, List<TimeSpan> hospitalWorkingHoursListForCalculation, DateTime date)
        {

            List<Appointment> AvailableAppointments = new List<Appointment>();
            List<TimeSpan> cloneList = new List<TimeSpan>(hospitalWorkingHoursListForCalculation);

            foreach (Appointment a in DoctorsAppointments)
            {
                selectedDoctor = a.Doctor;
                DateTime appStartTime = a.Date;
                DateTime appEndTime = a.Date + a.Duration;

                foreach (TimeSpan appTime in hospitalWorkingHours)
                {
                    date += appTime;
                    cloneList = CompareTimes(appTime, cloneList, date, appStartTime, appEndTime);
                    date = _date;
                }
            }

            return MakeNewAppointmantList(cloneList, selectedDoctor, _date, AvailableAppointments);
        }

        private List<TimeSpan> CompareTimes(TimeSpan appTime, List<TimeSpan> cloneList, DateTime date, DateTime appStartTime, DateTime appEndTime)
        {
            if (DateTime.Compare(date, appStartTime) > 0)
            {
                if (DateTime.Compare(date, appEndTime) < 0)
                {
                    if (cloneList.Contains(appTime))
                    {
                        cloneList.Remove(appTime);
                    }

                }
                else if (DateTime.Compare(date, appEndTime) == 0)
                {
                    cloneList.Remove(appTime);
                }
            }
            else if (DateTime.Compare(date, appStartTime) == 0)
            {
                if (DateTime.Compare(date, appEndTime) < 0)
                {
                    if (cloneList.Contains(appTime))
                    {
                        cloneList.Remove(appTime);
                    }
                }
            }
            return cloneList;
        }

        private List<Appointment> MakeNewAppointmantList(List<TimeSpan> cloneList, Doctor selectedDoctor, DateTime _date, List<Appointment> AvailableAppointments) {

            String doctorName = selectedDoctor.Name + " " + selectedDoctor.LastName;

            foreach (TimeSpan time in cloneList)
            {
                Appointment app = new Appointment();
                Doctor newDoctor = new Doctor
                {
                    Name = doctorName,
                    Id = selectedDoctor.Id
                };
                app.Date = _date + time;
                app.Doctor = newDoctor;
                app.DoctorId = selectedDoctor.Id;

                AvailableAppointments.Add(app);
            }

            return AvailableAppointments;

        }


    }
}