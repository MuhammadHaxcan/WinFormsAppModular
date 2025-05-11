using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Constants {
    public static class StatusConstants {
        public static class Order {
            public const string Pending = "Pending";
            public const string Delivered = "Delivered";
            public const string Cancelled = "Cancelled";
            public const string Cart = "Cart";
        }

        public static class Appointment {
            public const string Scheduled = "Scheduled";
            public const string Completed = "Completed";
            public const string Cancelled = "Cancelled";
            public const string Pending = "Pending";
        }
    }
}
