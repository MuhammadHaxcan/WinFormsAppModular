using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class Order {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public string DeliveryAddress { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }

        public decimal TotalAmount {
            get {
                decimal total = 0;
                foreach (var item in Items) {
                    total += item.TotalPrice;
                }
                return total;
            }
        }

        public string DisplayTotalAmount => $"${TotalAmount}";
    }
}
