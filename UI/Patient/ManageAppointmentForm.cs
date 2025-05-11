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
using SidebarApp.Models;

namespace SidebarApp.UI.Patient {
    public class ManageAppointmentForm : BaseForm {
        private DataGridView dgvAppointments;
        private ComboBox cboAudiologist;
        private DateTimePicker dtpDate;
        private ComboBox cboTimeSlot;
        private TextBox txtPurpose;
        private Button btnBook, btnCancel;

        public ManageAppointmentForm() {
            InitializeLayout();
            LoadAppointments();
        }

        private void InitializeLayout() {
            // Set form properties
            this.Text = AppStrings.Titles.ManageAppointment;

            // Add title label
            var lblTitle = CreateTitleLabel(AppStrings.Titles.ManageAppointment);
            this.Controls.Add(lblTitle);

            // Create appointment data grid
            dgvAppointments = CreateDataGrid(200);
            dgvAppointments.Columns.Add("Date", "Date");
            dgvAppointments.Columns.Add("Doctor", "Audiologist");
            dgvAppointments.Columns.Add("Purpose", "Purpose");
            dgvAppointments.Columns.Add("Status", "Status");
            this.Controls.Add(dgvAppointments);

            // Create appointment booking controls
            var lblAudiologist = CreateLabel("Audiologist:", UIConstants.Padding.Medium,
                dgvAppointments.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            cboAudiologist = new ComboBox {
                Location = new System.Drawing.Point(UIConstants.Padding.Medium, lblAudiologist.Bottom + UIConstants.Size.StandardControlSpacing),
                Width = UIConstants.Size.StandardTextBoxWidth,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboAudiologist.Items.AddRange(AppointmentService.GetAvailableAudiologists().ToArray());
            cboAudiologist.SelectedIndexChanged += CboAudiologist_SelectedIndexChanged;

            var lblDate = CreateLabel("Date:", UIConstants.Padding.Medium,
                cboAudiologist.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            dtpDate = new DateTimePicker {
                Location = new System.Drawing.Point(UIConstants.Padding.Medium, lblDate.Bottom + UIConstants.Size.StandardControlSpacing),
                Width = UIConstants.Size.StandardTextBoxWidth,
                MinDate = DateTime.Today
            };
            dtpDate.ValueChanged += DtpDate_ValueChanged;

            var lblTimeSlot = CreateLabel("Time Slot:", UIConstants.Padding.Medium,
                dtpDate.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            cboTimeSlot = new ComboBox {
                Location = new System.Drawing.Point(UIConstants.Padding.Medium, lblTimeSlot.Bottom + UIConstants.Size.StandardControlSpacing),
                Width = UIConstants.Size.StandardTextBoxWidth,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var lblPurpose = CreateLabel("Purpose:", UIConstants.Padding.Medium,
                cboTimeSlot.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtPurpose = CreateTextBox(UIConstants.Padding.Medium, lblPurpose.Bottom + UIConstants.Size.StandardControlSpacing);

            btnBook = CreateButton("Book Appointment", UIConstants.Padding.Medium,
                txtPurpose.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnBook_Click);
            btnCancel = CreateButton("Cancel Selected", UIConstants.Padding.Medium + UIConstants.Size.StandardButtonWidth + UIConstants.Size.StandardControlSpacing,
                txtPurpose.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnCancel_Click);

            this.Controls.Add(lblAudiologist);
            this.Controls.Add(cboAudiologist);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtpDate);
            this.Controls.Add(lblTimeSlot);
            this.Controls.Add(cboTimeSlot);
            this.Controls.Add(lblPurpose);
            this.Controls.Add(txtPurpose);
            this.Controls.Add(btnBook);
            this.Controls.Add(btnCancel);
        }

        private void LoadAppointments() {
            // Clear existing rows
            dgvAppointments.Rows.Clear();

            // Add current patient's appointments to the grid
            if (AuthService.CurrentPatient != null) {
                foreach (var appointment in AuthService.CurrentPatient.AppointmentHistory) {
                    dgvAppointments.Rows.Add(
                        appointment.Date.ToShortDateString(),
                        appointment.Doctor,
                        appointment.Purpose,
                        appointment.Status
                    );
                }
            }

            // Load available time slots if audiologist and date are selected
            if (cboAudiologist.SelectedItem != null) {
                LoadTimeSlots();
            }
        }

        private void CboAudiologist_SelectedIndexChanged(object sender, EventArgs e) {
            LoadTimeSlots();
        }

        private void DtpDate_ValueChanged(object sender, EventArgs e) {
            LoadTimeSlots();
        }

        private void LoadTimeSlots() {
            if (cboAudiologist.SelectedItem != null) {
                cboTimeSlot.Items.Clear();
                var timeSlots = AppointmentService.GetAvailableTimeSlots(dtpDate.Value, cboAudiologist.SelectedItem.ToString());

                foreach (var slot in timeSlots) {
                    if (slot.IsAvailable) {
                        cboTimeSlot.Items.Add(slot);
                    }
                }

                if (cboTimeSlot.Items.Count > 0) {
                    cboTimeSlot.SelectedIndex = 0;
                }
            }
        }

        private void BtnBook_Click(object sender, EventArgs e) {
            // Validate input
            if (cboAudiologist.SelectedItem == null) {
                UIService.ShowError("Please select an audiologist.");
                return;
            }

            if (cboTimeSlot.SelectedItem == null) {
                UIService.ShowError("Please select a time slot.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPurpose.Text)) {
                UIService.ShowError("Please enter a purpose for the appointment.");
                return;
            }

            // Create new appointment
            var appointment = new Appointment {
                Date = dtpDate.Value.Date.Add(TimeSpan.Parse(cboTimeSlot.SelectedItem.ToString().Split(' ')[0])),
                Doctor = cboAudiologist.SelectedItem.ToString(),
                Purpose = txtPurpose.Text
            };

            // Book appointment
            if (AppointmentService.BookAppointment(appointment)) {
                UIService.ShowSuccess(AppStrings.Messages.AppointmentBooked);
                LoadAppointments();

                // Clear inputs
                txtPurpose.Text = "";
            } else {
                UIService.ShowError("Failed to book appointment. Please try again.");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            // Check if an appointment is selected
            if (dgvAppointments.SelectedRows.Count == 0) {
                UIService.ShowError("Please select an appointment to cancel.");
                return;
            }

            // Get selected appointment
            int selectedIndex = dgvAppointments.SelectedRows[0].Index;
            if (selectedIndex >= 0 && selectedIndex < AuthService.CurrentPatient.AppointmentHistory.Count) {
                var appointment = AuthService.CurrentPatient.AppointmentHistory[selectedIndex];

                // Confirm cancellation
                if (UIService.Confirm("Are you sure you want to cancel this appointment?")) {
                    // Cancel appointment
                    if (AppointmentService.CancelAppointment(appointment)) {
                        UIService.ShowSuccess(AppStrings.Messages.AppointmentCancelled);
                        LoadAppointments();
                    } else {
                        UIService.ShowError("Failed to cancel appointment. Please try again.");
                    }
                }
            }
        }
    }
}
