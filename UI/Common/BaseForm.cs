using SidebarApp.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SidebarApp.UI.Common {
    public class BaseForm : Form {
        public BaseForm() {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(UIConstants.Padding.Medium);
        }

        protected Label CreateLabel(string text, int x, int y, int width = -1) {
            width = width > 0 ? width : UIConstants.Size.StandardTextBoxWidth;

            return new Label {
                Text = text,
                Location = new Point(x, y),
                Width = width,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = true
            };
        }

        protected TextBox CreateTextBox(int x, int y, string text = "", bool isPassword = false) {
            var textBox = new TextBox {
                Location = new Point(x, y),
                Width = UIConstants.Size.StandardTextBoxWidth,
                Text = text
            };

            if (isPassword) {
                textBox.UseSystemPasswordChar = true;
            }

            return textBox;
        }
        protected Button CreateButton(string text, int x, int y, EventHandler clickHandler = null) {
            var button = new Button {
                Text = text,
                Location = new Point(x, y),
                Width = UIConstants.Size.StandardButtonWidth,
                Height = UIConstants.Size.StandardButtonHeight
            };

            if (clickHandler != null) {
                button.Click += clickHandler;
            }

            return button;
        }

        protected Label CreateTitleLabel(string text) {
            return new Label {
                Text = text,
                Dock = DockStyle.Top,
                Height = UIConstants.Layout.TopBarHeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold)
            };
        }

        protected DataGridView CreateDataGrid(int height, bool allowUserToAddRows = false) {
            return new DataGridView {
                Dock = DockStyle.Top,
                Height = height,
                AllowUserToAddRows = allowUserToAddRows,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
        }
    }
}
