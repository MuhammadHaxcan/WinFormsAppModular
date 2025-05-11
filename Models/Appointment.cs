using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class Appointment {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Doctor { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }

        public Appointment Clone() {
            return new Appointment {
                Id = this.Id,
                Date = this.Date,
                Doctor = this.Doctor,
                Purpose = this.Purpose,
                Status = this.Status,
                PatientId = this.PatientId
            };
        }
    }
}
