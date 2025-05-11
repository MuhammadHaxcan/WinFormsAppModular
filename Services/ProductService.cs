using SidebarApp.Constants;
using SidebarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidebarApp.Services {
    public static class ProductService {
        private static List<HearingAid> _availableHearingAids;
        private static List<CartItem> _currentCart = new List<CartItem>();

        public static List<HearingAid> GetAvailableHearingAids() {
            if (_availableHearingAids == null) {
                _availableHearingAids = new List<HearingAid>
                {
                    new HearingAid
                    {
                        Id = 1,
                        Model = "Basic Model A",
                        Price = 500m,
                        Features = "Basic features, water resistant",
                        IsAvailable = true
                    },
                    new HearingAid
                    {
                        Id = 2,
                        Model = "Advanced Model B",
                        Price = 800m,
                        Features = "Advanced features, noise cancellation, Bluetooth",
                        IsAvailable = true
                    },
                    new HearingAid
                    {
                        Id = 3,
                        Model = "Premium Model C",
                        Price = 1200m,
                        Features = "Premium features, AI-assisted, app-controlled",
                        IsAvailable = true
                    }
                };
            }
            return _availableHearingAids;
        }

        public static List<CartItem> GetCart() {
            return _currentCart;
        }

        public static void AddToCart(HearingAid product, int quantity) {
            var existingItem = _currentCart.Find(i => i.Product.Id == product.Id);
            if (existingItem != null) {
                existingItem.Quantity += quantity;
            } else {
                _currentCart.Add(new CartItem {
                    Product = product,
                    Quantity = quantity,
                    Status = StatusConstants.Order.Cart
                });
            }
        }

        public static Order ProcessOrder(string deliveryAddress) {
            if (AuthService.CurrentPatient == null || _currentCart.Count == 0)
                return null;

            var order = new Order {
                Id = GetNextOrderId(),
                Date = DateTime.Now,
                Items = new List<CartItem>(_currentCart),
                DeliveryAddress = deliveryAddress,
                Status = StatusConstants.Order.Pending,
                PatientId = AuthService.CurrentPatient.Id
            };

            // Update cart items status
            foreach (var item in order.Items) {
                item.Status = StatusConstants.Order.Pending;
            }

            // Add to patient's order history
            AuthService.CurrentPatient.OrderHistory.Add(order);

            // Clear the cart
            _currentCart.Clear();

            return order;
        }

        private static int GetNextOrderId() {
            // In a real application, this would come from a database
            if (AuthService.CurrentPatient.OrderHistory.Count == 0)
                return 1;

            int maxId = 0;
            foreach (var order in AuthService.CurrentPatient.OrderHistory) {
                if (order.Id > maxId)
                    maxId = order.Id;
            }
            return maxId + 1;
        }
    }
}
