using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class Patient {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Appointment> AppointmentHistory { get; set; } = new List<Appointment>();
        public List<MedicalRecord> MedicalHistory { get; set; } = new List<MedicalRecord>();
        public List<Order> OrderHistory { get; set; } = new List<Order>();
    }
}
