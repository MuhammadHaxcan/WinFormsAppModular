using System;
using System.Windows.Forms;

namespace SidebarApp.Forms {
    public class RecordsForm : Form {
        private DataGridView grid;
        private TextBox txtSearch;
        private Button btnAdd;

        public RecordsForm() {
            SetupLayout();
        }

        private void SetupLayout() {
            Label lbl = new Label { Text = "Records", Dock = DockStyle.Top, Height = 50 };
            txtSearch = new TextBox { Dock = DockStyle.Top };
            btnAdd = new Button { Text = "Add Record", Dock = DockStyle.Top };
            grid = new DataGridView {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            grid.Columns.Add("ID", "ID");
            grid.Columns.Add("Name", "Name");
            grid.Columns.Add("Category", "Category");
            grid.Columns.Add("Date", "Date");
            grid.Columns.Add("Status", "Status");

            this.Controls.Add(grid);
            this.Controls.Add(btnAdd);
            this.Controls.Add(txtSearch);
            this.Controls.Add(lbl);

            btnAdd.Click += BtnAdd_Click;

            // Temporary data
            grid.Rows.Add("1001", "Alpha", "A", "2025-01-01", "Active");
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            MessageBox.Show("Add functionality here.");
        }
    }
}
