using SidebarApp.Constants;
using SidebarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Services {
    public static class AppointmentService {

        public static bool BookAppointment(Appointment appointment) {
            if (AuthService.CurrentPatient == null)
                return false;

            // Set appointment properties
            appointment.Id = GetNextAppointmentId();
            appointment.PatientId = AuthService.CurrentPatient.Id;
            appointment.Status = StatusConstants.Appointment.Pending;

            // Add to patient's appointment history
            AuthService.CurrentPatient.AppointmentHistory.Add(appointment);
            return true;
        }

        public static bool CancelAppointment(Appointment appointment) {
            if (AuthService.CurrentPatient == null)
                return false;

            var existingAppointment = AuthService.CurrentPatient.AppointmentHistory.Find(a => a.Id == appointment.Id);
            if (existingAppointment != null) {
                existingAppointment.Status = StatusConstants.Appointment.Cancelled;
                return true;
            }
            return false;
        }

        public static List<string> GetAvailableAudiologists() {
            // In a real application, this would come from a database
            return new List<string> { "Dr. Smith", "Dr. Jones", "Dr. Taylor", "Dr. Williams" };
        }

        public static List<TimeSlot> GetAvailableTimeSlots(DateTime date, string doctor) {
            // In a real application, this would come from a database
            return new List<TimeSlot>
            {
                new TimeSlot { Time = "09:00 AM", IsAvailable = true },
                new TimeSlot { Time = "10:00 AM", IsAvailable = true },
                new TimeSlot { Time = "11:00 AM", IsAvailable = false },
                new TimeSlot { Time = "12:00 PM", IsAvailable = true },
                new TimeSlot { Time = "02:00 PM", IsAvailable = true },
                new TimeSlot { Time = "03:00 PM", IsAvailable = false },
                new TimeSlot { Time = "04:00 PM", IsAvailable = true }
            };
        }

        private static int GetNextAppointmentId() {
            // In a real application, this would come from a database
            if (AuthService.CurrentPatient.AppointmentHistory.Count == 0)
                return 1;

            int maxId = 0;
            foreach (var appointment in AuthService.CurrentPatient.AppointmentHistory) {
                if (appointment.Id > maxId)
                    maxId = appointment.Id;
            }
            return maxId + 1;
        }
    }
}
