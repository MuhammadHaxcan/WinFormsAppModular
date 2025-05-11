using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Models {
    public class TimeSlot {
        public string Time { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString() {
            return Time;
        }
    }
}
