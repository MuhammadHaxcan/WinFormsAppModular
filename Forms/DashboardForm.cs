using System;
using System.Windows.Forms;
using SidebarApp.Logic;

namespace SidebarApp.Forms {
    public class DashboardForm : Form {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblWelcomeMessage;

        public event EventHandler LoginSuccess;

        public DashboardForm() {
            InitializeLayout();

            // If the user is already logged in, show welcome message
            if (AuthManager.IsLoggedIn) {
                DisplayWelcomeMessage();
            }
        }

        private void InitializeLayout() {
            this.Text = "Dashboard";

            // Title label
            Label lblTitle = new Label {
                Text = "Dashboard",
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            // Content panel for displaying the login form or dashboard content
            Panel contentPanel = new Panel {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Placeholder for login form (will be hidden after login)
            txtUsername = new TextBox {
                Top = 80,
                Left = 20,
                Width = 200,
                Visible = !AuthManager.IsLoggedIn // Only visible if user is not logged in
            };
            txtPassword = new TextBox {
                Top = 140,
                Left = 20,
                Width = 200,
                UseSystemPasswordChar = true,
                Visible = !AuthManager.IsLoggedIn // Only visible if user is not logged in
            };

            btnLogin = new Button {
                Text = "Login",
                Top = 180,
                Left = 20,
                Width = 100,
                Visible = !AuthManager.IsLoggedIn // Only visible if user is not logged in
            };
            btnLogin.Click += BtnLogin_Click;

            // Label for welcome message (will be shown after successful login)
            lblWelcomeMessage = new Label {
                Text = "",
                AutoSize = true,
                Top = 80,
                Left = 20,
                Visible = AuthManager.IsLoggedIn // Visible if user is logged in
            };

            contentPanel.Controls.Add(txtUsername);
            contentPanel.Controls.Add(txtPassword);
            contentPanel.Controls.Add(btnLogin);
            contentPanel.Controls.Add(lblWelcomeMessage);

            this.Controls.Add(contentPanel);
        }

        private void BtnLogin_Click(object sender, EventArgs e) {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthManager.AuthenticateUser(username, password)) {
                // Set the global login state to true
                AuthManager.IsLoggedIn = true;

                // Show welcome message and hide login form
                DisplayWelcomeMessage();

                // Trigger the LoginSuccess event for MainForm to enable sidebar buttons
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            } else {
                AuthManager.ShowMessage("Invalid credentials.", "Login Failed", MessageBoxIcon.Error);
            }
        }

        private void DisplayWelcomeMessage() {
            lblWelcomeMessage.Text = $"Welcome, admin!";
            lblWelcomeMessage.Visible = true;
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
        }
    }
}
