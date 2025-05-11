using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SidebarApp.Services {
    public static class UIService {

        public static void ShowMessage(string message, string title = "Information", MessageBoxIcon icon = MessageBoxIcon.Information) {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        public static void ShowError(string message, string title = "Error") {
            ShowMessage(message, title, MessageBoxIcon.Error);
        }

        public static void ShowSuccess(string message, string title = "Success") {
            ShowMessage(message, title, MessageBoxIcon.Information);
        }

        public static bool Confirm(string message, string title = "Confirm") {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
