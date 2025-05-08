using System;
using System.Windows.Forms;

namespace SidebarApp.Forms {
    public class SalesForm : Form {
        private DataGridView grid;

        public SalesForm() {
            SetupLayout();
        }

        private void SetupLayout() {
            Label lbl = new Label { Text = "Sales", Dock = DockStyle.Top, Height = 50 };
            grid = new DataGridView {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            grid.Columns.Add("Invoice", "Invoice #");
            grid.Columns.Add("Date", "Date");
            grid.Columns.Add("Customer", "Customer");
            grid.Columns.Add("Items", "Items");
            grid.Columns.Add("Amount", "Amount");
            grid.Columns.Add("Status", "Status");

            this.Controls.Add(grid);
            this.Controls.Add(lbl);

            // Placeholder data
            grid.Rows.Add("INV-001", "2025-05-01", "John", "3", "$1000", "Paid");
        }
    }
}
