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

namespace SidebarApp.UI.Forms {
    public class DashboardForm : BaseForm {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblWelcomeMessage;
        private Button btnLogout;
        private Panel loginPanel;
        private Panel welcomePanel;

        public event EventHandler LoginSuccess;

        public DashboardForm() {
            InitializeLayout();

            // If the user is already logged in, show the welcome message
            if (AuthService.IsLoggedIn) {
                DisplayWelcomeMessage();
            }
        }

        private void InitializeLayout() {
            // Set form properties
            this.Text = AppStrings.Titles.Dashboard;

            // Add title label
            var lblTitle = CreateTitleLabel(AppStrings.Titles.Dashboard);
            this.Controls.Add(lblTitle);

            // Create panels for login and welcome message
            loginPanel = new Panel {
                Dock = DockStyle.Fill,
                Visible = !AuthService.IsLoggedIn
            };

            welcomePanel = new Panel {
                Dock = DockStyle.Fill,
                Visible = AuthService.IsLoggedIn
            };

            // Create login controls
            var lblUsername = CreateLabel("Username:", UIConstants.Padding.Medium, UIConstants.Padding.Medium);
            txtUsername = CreateTextBox(UIConstants.Padding.Medium, lblUsername.Bottom + UIConstants.Size.StandardControlSpacing);

            var lblPassword = CreateLabel("Password:", UIConstants.Padding.Medium,
                txtUsername.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtPassword = CreateTextBox(UIConstants.Padding.Medium, lblPassword.Bottom + UIConstants.Size.StandardControlSpacing, "", true);

            btnLogin = CreateButton("Login", UIConstants.Padding.Medium,
                txtPassword.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnLogin_Click);

            loginPanel.Controls.Add(lblUsername);
            loginPanel.Controls.Add(txtUsername);
            loginPanel.Controls.Add(lblPassword);
            loginPanel.Controls.Add(txtPassword);
            loginPanel.Controls.Add(btnLogin);

            // Create welcome message controls
            lblWelcomeMessage = CreateLabel("", UIConstants.Padding.Medium, UIConstants.Padding.Medium);
            lblWelcomeMessage.Font = new System.Drawing.Font(this.Font.FontFamily, 12, System.Drawing.FontStyle.Bold);

            btnLogout = CreateButton("Logout", UIConstants.Padding.Medium,
                lblWelcomeMessage.Bottom + UIConstants.Size.StandardControlSpacing * 3, BtnLogout_Click);

            welcomePanel.Controls.Add(lblWelcomeMessage);
            welcomePanel.Controls.Add(btnLogout);

            // Add panels to form
            this.Controls.Add(loginPanel);
            this.Controls.Add(welcomePanel);
        }

        private void BtnLogin_Click(object sender, EventArgs e) {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthService.AuthenticateUser(username, password)) {
                // Show welcome message and hide login form
                DisplayWelcomeMessage();

                // Show success message
                UIService.ShowSuccess(AppStrings.Messages.LoginSuccess);

                // Trigger the LoginSuccess event for MainForm to enable sidebar buttons
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            } else {
                UIService.ShowError(AppStrings.Messages.LoginFailed);
            }
        }

        private void DisplayWelcomeMessage() {
            lblWelcomeMessage.Text = string.Format(AppStrings.Labels.Welcome, AuthService.CurrentPatient.Name);
            loginPanel.Visible = false;
            welcomePanel.Visible = true;
        }

        private void BtnLogout_Click(object sender, EventArgs e) {
            // Reset the login state
            AuthService.Logout();

            // Clear input fields
            txtUsername.Text = "";
            txtPassword.Text = "";

            // Reset the UI for login
            loginPanel.Visible = true;
            welcomePanel.Visible = false;

            // Show success message
            UIService.ShowSuccess(AppStrings.Messages.LogoutSuccess);
        }
    }   
}
