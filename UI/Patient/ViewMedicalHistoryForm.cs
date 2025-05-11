using SidebarApp.Constants;
using SidebarApp.Services;
using SidebarApp.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SidebarApp.UI.Patient {
    public class ViewMedicalHistoryForm : BaseForm {
        private DataGridView dgvMedicalHistory;

        public ViewMedicalHistoryForm() {
            InitializeLayout();
            LoadMedicalHistory();
        }

        private void InitializeLayout() {
            this.Text = AppStrings.Titles.MedicalHistory;

            var lblTitle = CreateTitleLabel(AppStrings.Titles.MedicalHistory);
            this.Controls.Add(lblTitle);

            dgvMedicalHistory = CreateDataGrid(400);
            dgvMedicalHistory.Columns.Add("Date", "Date");
            dgvMedicalHistory.Columns.Add("TestType", "Test Type");
            dgvMedicalHistory.Columns.Add("Results", "Results");
            dgvMedicalHistory.Columns.Add("Prescription", "Prescription");
            dgvMedicalHistory.Columns.Add("Doctor", "Doctor");  // New column for doctor
            this.Controls.Add(dgvMedicalHistory);
        }

        private void LoadMedicalHistory() {
            dgvMedicalHistory.Rows.Clear();

            var medicalRecords = MedicalService.GetMedicalRecords();
            foreach (var record in medicalRecords) {
                dgvMedicalHistory.Rows.Add(
                    record.Date.ToShortDateString(),
                    record.TestType,
                    record.Results,
                    record.Prescription,
                    record.Doctor  // Displaying the doctor's name
                );
            }
        }
    }
}
