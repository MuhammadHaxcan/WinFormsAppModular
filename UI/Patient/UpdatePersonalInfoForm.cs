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
    public class UpdatePersonalInfoForm : BaseForm {
        private TextBox txtName, txtPhone, txtAddress;
        private DateTimePicker dtpDob;
        private Button btnSave;

        public UpdatePersonalInfoForm() {
            InitializeLayout();
            LoadPersonalInfo();
        }

        private void InitializeLayout() {
            // Set form properties
            this.Text = AppStrings.Titles.UpdatePersonalInfo;

            // Add title label
            var lblTitle = CreateTitleLabel(AppStrings.Titles.UpdatePersonalInfo);
            this.Controls.Add(lblTitle);

            // Create personal info fields
            var lblName = CreateLabel("Name:", UIConstants.Padding.Medium,
                lblTitle.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtName = CreateTextBox(UIConstants.Padding.Medium, lblName.Bottom + UIConstants.Size.StandardControlSpacing);

            var lblPhone = CreateLabel("Phone:", UIConstants.Padding.Medium,
                txtName.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtPhone = CreateTextBox(UIConstants.Padding.Medium, lblPhone.Bottom + UIConstants.Size.StandardControlSpacing);

            var lblAddress = CreateLabel("Address:", UIConstants.Padding.Medium,
                txtPhone.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtAddress = CreateTextBox(UIConstants.Padding.Medium, lblAddress.Bottom + UIConstants.Size.StandardControlSpacing);

            var lblDob = CreateLabel("Date of Birth:", UIConstants.Padding.Medium,
                txtAddress.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            dtpDob = new DateTimePicker {
                Location = new System.Drawing.Point(UIConstants.Padding.Medium, lblDob.Bottom + UIConstants.Size.StandardControlSpacing),
                Width = UIConstants.Size.StandardTextBoxWidth
            };

            btnSave = CreateButton("Save Changes", UIConstants.Padding.Medium,
                dtpDob.Bottom + UIConstants.Size.StandardControlSpacing * 3, BtnSave_Click);

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            this.Controls.Add(lblAddress);
            this.Controls.Add(txtAddress);
            this.Controls.Add(lblDob);
            this.Controls.Add(dtpDob);
            this.Controls.Add(btnSave);
        }

        private void LoadPersonalInfo() {
            // Load current patient's info
            if (AuthService.CurrentPatient != null) {
                txtName.Text = AuthService.CurrentPatient.Name;
                txtPhone.Text = AuthService.CurrentPatient.Phone;
                txtAddress.Text = AuthService.CurrentPatient.Address;
                dtpDob.Value = AuthService.CurrentPatient.DateOfBirth;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtName.Text)) {
                UIService.ShowError("Name cannot be empty.");
                return;
            }

            // Update patient info
            if (AuthService.CurrentPatient != null) {
                AuthService.CurrentPatient.Name = txtName.Text;
                AuthService.CurrentPatient.Phone = txtPhone.Text;
                AuthService.CurrentPatient.Address = txtAddress.Text;
                AuthService.CurrentPatient.DateOfBirth = dtpDob.Value;

                UIService.ShowSuccess(AppStrings.Messages.SaveSuccess);
            }
        }
    }
}
