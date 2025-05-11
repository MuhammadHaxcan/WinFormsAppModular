using SidebarApp.Constants;
using SidebarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Services {
    public static class AuthService {
        // Current logged in patient
        public static Patient CurrentPatient { get; private set; }

        // Authentication state
        public static bool IsLoggedIn => CurrentPatient != null;
        public static bool AuthenticateUser(string username, string password) {
            // In a real application, this would check against a database
            if (username == AuthConstants.DefaultUsername && password == AuthConstants.DefaultPassword) {
                // Create a demo patient with sample data
                CurrentPatient = CreateDemoPatient();
                return true;
            }
            return false;
        }

        public static void Logout() {
            CurrentPatient = null;
        }

        private static Patient CreateDemoPatient() {
            var patient = new Patient {
                Id = 1,
                Username = AuthConstants.DefaultUsername,
                Name = "John Doe",
                Phone = "123-456-7890",
                Address = "123 Main St, City, Country",
                DateOfBirth = DateTime.Now.AddYears(-30),
                AppointmentHistory = new List<Appointment>
                {
                    new Appointment
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(3),
                        Doctor = "Dr. Smith",
                        Purpose = "Hearing Checkup",
                        Status = StatusConstants.Appointment.Pending,
                        PatientId = 1
                    }
                },
                MedicalHistory = new List<MedicalRecord>
                {
                    new MedicalRecord
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(-15),
                        TestType = "Hearing Test",
                        Results = "Normal",
                        Prescription = "Take 1 tablet of Hearing Aid daily for 7 days.",
                        PatientId = 1,
                        Doctor = "Shakoor Saab"
                    },
                    new MedicalRecord
                    {
                        Id = 2,
                        Date = DateTime.Now.AddDays(-30),
                        TestType = "Ear Exam",
                        Results = "No issues",
                        Prescription = "None",
                        PatientId = 1,
                        Doctor = "Khalid Bhai"
                    }
                },
                OrderHistory = new List<Order>()
            };

            return patient;
        }
    }
}
