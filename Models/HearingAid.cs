using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class HearingAid {
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Features { get; set; }
        public bool IsAvailable { get; set; }

        public string DisplayPrice => $"${Price}";
    }
}
