using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Constants {
    public static class AppStrings {
        public static class Titles {
            public const string AppTitle = "Audiology Patient Management System";
            public const string Dashboard = "Dashboard";
            public const string ManageAppointment = "Manage Appointments";
            public const string PurchaseHearingAid = "Purchase Hearing Aid";
            public const string MedicalHistory = "Medical History";
            public const string UpdatePersonalInfo = "Update Personal Information";
        }

        public static class Messages {
            public const string LoginFailed = "Invalid credentials. Please try again.";
            public const string LoginSuccess = "Login successful!";
            public const string LogoutSuccess = "Logout successful!";
            public const string SaveSuccess = "Changes saved successfully.";
            public const string AppointmentBooked = "Appointment booked successfully.";
            public const string AppointmentCancelled = "Appointment canceled.";
            public const string ItemAddedToCart = "Item added to cart!";
            public const string CheckoutSuccess = "Checkout successful.";
            public const string SelectItemToCheckout = "Please select an item to checkout.";
        }

        public static class Labels {
            public const string Welcome = "Welcome, {0}!";
            public const string OrderStatus = "Order Status: {0}";
        }
    }
}
