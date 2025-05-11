using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class CartItem {
        public HearingAid Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public decimal TotalPrice => Product.Price * Quantity;
        public string DisplayTotalPrice => $"${TotalPrice}";
    }
}
