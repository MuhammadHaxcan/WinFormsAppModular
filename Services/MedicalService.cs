using SidebarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Services {
    public static class MedicalService {

        public static List<MedicalRecord> GetMedicalRecords() {
            if (AuthService.CurrentPatient == null)
                return new List<MedicalRecord>();

            return AuthService.CurrentPatient.MedicalHistory;
        }

        public static bool AddMedicalRecord(MedicalRecord record) {
            if (AuthService.CurrentPatient == null)
                return false;

            record.Id = GetNextMedicalRecordId();
            record.PatientId = AuthService.CurrentPatient.Id;
            AuthService.CurrentPatient.MedicalHistory.Add(record);
            return true;
        }

        private static int GetNextMedicalRecordId() {
            // In a real application, this would come from a database
            if (AuthService.CurrentPatient.MedicalHistory.Count == 0)
                return 1;

            int maxId = 0;
            foreach (var record in AuthService.CurrentPatient.MedicalHistory) {
                if (record.Id > maxId)
                    maxId = record.Id;
            }
            return maxId + 1;
        }
    }

}
