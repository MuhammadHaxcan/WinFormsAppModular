using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class MedicalRecord {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TestType { get; set; }
        public string Results { get; set; }
        public string Prescription { get; set; }
        public string Doctor { get; set; } 
        public int PatientId { get; set; }
    }
}
