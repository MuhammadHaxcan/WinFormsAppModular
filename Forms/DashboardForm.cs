using System;
using System.Windows.Forms;
using SidebarApp.Logic;

namespace SidebarApp.Forms {
    public class DashboardForm : Form {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        public event EventHandler LoginSuccess;

        public DashboardForm() {
            InitializeLayout();
        }

        private void InitializeLayout() {
            this.Text = "Dashboard";

            Label lblTitle = new Label {
                Text = "Dashboard",
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            Panel contentPanel = new Panel {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Label lblWelcome = new Label {
                Text = "Welcome! Please log in to continue.",
                AutoSize = true,
                Top = 20,
                Left = 20
            };

            Label lblUser = new Label {
                Text = "Username:",
                Top = 60,
                Left = 20,
                AutoSize = true
            };

            txtUsername = new TextBox {
                Top = 80,
                Left = 20,
                Width = 200
            };

            Label lblPass = new Label {
                Text = "Password:",
                Top = 120,
                Left = 20,
                AutoSize = true
            };

            txtPassword = new TextBox {
                Top = 140,
                Left = 20,
                Width = 200,
                UseSystemPasswordChar = true
            };

            btnLogin = new Button {
                Text = "Login",
                Top = 180,
                Left = 20,
                Width = 100
            };
            btnLogin.Click += BtnLogin_Click;

            // Add controls to panel
            contentPanel.Controls.Add(lblWelcome);
            contentPanel.Controls.Add(lblUser);
            contentPanel.Controls.Add(txtUsername);
            contentPanel.Controls.Add(lblPass);
            contentPanel.Controls.Add(txtPassword);
            contentPanel.Controls.Add(btnLogin);

            this.Controls.Add(contentPanel);
        }

        private void BtnLogin_Click(object sender, EventArgs e) {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthManager.AuthenticateUser(username, password)) {
                AuthManager.ShowMessage("Login successful!", "Success", MessageBoxIcon.Information);
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            } else {
                AuthManager.ShowMessage("Invalid credentials.", "Login Failed", MessageBoxIcon.Error);
            }
        }
    }
}
