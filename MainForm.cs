using System;
using System.Windows.Forms;
using SidebarApp.Forms;
using SidebarApp.Logic;

namespace SidebarApp {
    public class MainForm : Form {
        private Button btnDashboard;
        private Button btnRecords;
        private Button btnSales;
        private Panel sidebarPanel;
        private Panel contentPanel;
        private Form currentForm;

        public MainForm() {
            InitializeLayout();
            SetupSidebar();

            // Show the dashboard by default
            ShowForm(new DashboardForm());
        }

        private void InitializeLayout() {
            this.Text = "Sidebar Application";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            sidebarPanel = new Panel { Dock = DockStyle.Left, Width = 200 };
            contentPanel = new Panel { Dock = DockStyle.Fill };

            this.Controls.Add(contentPanel);
            this.Controls.Add(sidebarPanel);
        }

        private void SetupSidebar() {
            btnDashboard = CreateSidebarButton("Dashboard", 0);
            btnRecords = CreateSidebarButton("Records", 1);
            btnSales = CreateSidebarButton("Sales", 2);

            btnRecords.Visible = false; // Initially hidden
            btnSales.Visible = false;   // Initially hidden

            // Add event handler for dashboard form login success
            btnDashboard.Click += (s, e) => ShowForm(new DashboardForm());
            btnRecords.Click += (s, e) => ShowForm(new RecordsForm());
            btnSales.Click += (s, e) => ShowForm(new SalesForm());

            sidebarPanel.Controls.Add(btnSales);
            sidebarPanel.Controls.Add(btnRecords);
            sidebarPanel.Controls.Add(btnDashboard);
        }

        private Button CreateSidebarButton(string text, int position) {
            var btn = new Button {
                Text = text,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            return btn;
        }

        private void ShowForm(Form form) {
            if (currentForm != null) {
                currentForm.Close();
                currentForm.Dispose();
            }

            currentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(form);
            form.Show();

            if (form is DashboardForm dashboard) {
                dashboard.LoginSuccess += Dashboard_LoginSuccess;
            }
        }

        private void Dashboard_LoginSuccess(object sender, EventArgs e) {
            AuthManager.EnableSidebarButtons(btnRecords, btnSales);
        }
    }
}
