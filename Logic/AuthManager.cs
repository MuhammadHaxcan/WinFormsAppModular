using System.Windows.Forms;

namespace SidebarApp.Logic {
    public static class AuthManager {
        // Global variable to store login state for this session
        public static bool IsLoggedIn = false;

        public static bool AuthenticateUser(string username, string password) {
            // Placeholder for real authentication (e.g., check DB or API)
            return username == "admin" && password == "1234";
        }

        public static void EnableSidebarButtons(Button btnRecords, Button btnSales) {
            btnRecords.Visible = true;
            btnSales.Visible = true;
        }

        public static void ShowMessage(string message, string title, MessageBoxIcon icon) {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }
    }
}
