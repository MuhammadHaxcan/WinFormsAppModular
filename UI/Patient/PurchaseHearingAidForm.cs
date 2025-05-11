using SidebarApp.Constants;
using SidebarApp.Services;
using SidebarApp.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SidebarApp.UI.Patient {
    public class PurchaseHearingAidForm : BaseForm {
        private DataGridView dgvHearingAids;
        private DataGridView dgvCart;
        private NumericUpDown nudQuantity;
        private TextBox txtDeliveryAddress;
        private Label lblOrderStatus;
        private Button btnAddToCart, btnCheckout, btnRemoveFromCart;

        public PurchaseHearingAidForm() {
            InitializeLayout();
            LoadHearingAids();
            LoadCart();
        }

        private void InitializeLayout() {
            // Set form properties
            this.Text = AppStrings.Titles.PurchaseHearingAid;

            // Add title label
            var lblTitle = CreateTitleLabel(AppStrings.Titles.PurchaseHearingAid);
            this.Controls.Add(lblTitle);

            // Create hearing aids data grid
            var lblAvailableProducts = CreateLabel("Available Hearing Aids:", UIConstants.Padding.Medium,
                lblTitle.Bottom + UIConstants.Size.StandardControlSpacing);
            this.Controls.Add(lblAvailableProducts);

            dgvHearingAids = CreateDataGrid(150);
            dgvHearingAids.Top = lblAvailableProducts.Bottom + UIConstants.Size.StandardControlSpacing;
            dgvHearingAids.Columns.Add("Model", "Model");
            dgvHearingAids.Columns.Add("Price", "Price");
            dgvHearingAids.Columns.Add("Features", "Features");
            this.Controls.Add(dgvHearingAids);

            // Create quantity selector
            var lblQuantity = CreateLabel("Quantity:", UIConstants.Padding.Medium,
                dgvHearingAids.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            nudQuantity = new NumericUpDown {
                Location = new System.Drawing.Point(UIConstants.Padding.Medium, lblQuantity.Bottom + UIConstants.Size.StandardControlSpacing),
                Width = UIConstants.Size.StandardTextBoxWidth,
                Minimum = 1,
                Maximum = 10,
                Value = 1
            };
            this.Controls.Add(lblQuantity);
            this.Controls.Add(nudQuantity);

            // Create delivery address field
            var lblDeliveryAddress = CreateLabel("Delivery Address:", UIConstants.Padding.Medium,
                nudQuantity.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            txtDeliveryAddress = CreateTextBox(UIConstants.Padding.Medium, lblDeliveryAddress.Bottom + UIConstants.Size.StandardControlSpacing);

            // Set default address if logged in
            if (AuthService.CurrentPatient != null) {
                txtDeliveryAddress.Text = AuthService.CurrentPatient.Address;
            }

            this.Controls.Add(lblDeliveryAddress);
            this.Controls.Add(txtDeliveryAddress);

            // Create order status label
            lblOrderStatus = CreateLabel(string.Format(AppStrings.Labels.OrderStatus, "None"), UIConstants.Padding.Medium,
                txtDeliveryAddress.Bottom + UIConstants.Size.StandardControlSpacing * 2);
            this.Controls.Add(lblOrderStatus);

            // Create buttons
            btnAddToCart = CreateButton("Add to Cart", UIConstants.Padding.Medium,
                lblOrderStatus.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnAddToCart_Click);

            btnRemoveFromCart = CreateButton("Remove", UIConstants.Padding.Medium + UIConstants.Size.StandardButtonWidth + UIConstants.Size.StandardControlSpacing,
                lblOrderStatus.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnRemoveFromCart_Click);

            btnCheckout = CreateButton("Checkout", UIConstants.Padding.Medium + (UIConstants.Size.StandardButtonWidth + UIConstants.Size.StandardControlSpacing) * 2,
                lblOrderStatus.Bottom + UIConstants.Size.StandardControlSpacing * 2, BtnCheckout_Click);

            this.Controls.Add(btnAddToCart);
            this.Controls.Add(btnRemoveFromCart);
            this.Controls.Add(btnCheckout);

            // Create shopping cart data grid
            var lblCart = CreateLabel("Shopping Cart:", UIConstants.Padding.Medium,
                btnAddToCart.Bottom + UIConstants.Size.StandardControlSpacing * 3);
            this.Controls.Add(lblCart);

            dgvCart = CreateDataGrid(150);
            dgvCart.Top = lblCart.Bottom + UIConstants.Size.StandardControlSpacing;
            dgvCart.Columns.Add("Model", "Model");
            dgvCart.Columns.Add("Quantity", "Quantity");
            dgvCart.Columns.Add("TotalPrice", "Total Price");
            dgvCart.Columns.Add("Status", "Status");
            this.Controls.Add(dgvCart);
        }

        private void LoadHearingAids() {
            // Clear existing rows
            dgvHearingAids.Rows.Clear();

            // Add available hearing aids to the grid
            var hearingAids = ProductService.GetAvailableHearingAids();
            foreach (var hearingAid in hearingAids) {
                if (hearingAid.IsAvailable) {
                    dgvHearingAids.Rows.Add(
                        hearingAid.Model,
                        hearingAid.DisplayPrice,
                        hearingAid.Features
                    );
                }
            }

            // Select the first row by default
            if (dgvHearingAids.Rows.Count > 0) {
                dgvHearingAids.Rows[0].Selected = true;
            }
        }

        private void LoadCart() {
            // Clear existing rows
            dgvCart.Rows.Clear();

            // Add cart items to the grid
            var cartItems = ProductService.GetCart();
            foreach (var item in cartItems) {
                dgvCart.Rows.Add(
                    item.Product.Model,
                    item.Quantity,
                    item.DisplayTotalPrice,
                    item.Status
                );
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e) {
            // Check if a hearing aid is selected
            if (dgvHearingAids.SelectedRows.Count == 0) {
                UIService.ShowError("Please select a hearing aid to add to cart.");
                return;
            }

            // Get selected hearing aid
            int selectedIndex = dgvHearingAids.SelectedRows[0].Index;
            if (selectedIndex >= 0) {
                var hearingAids = ProductService.GetAvailableHearingAids();
                if (selectedIndex < hearingAids.Count) {
                    var hearingAid = hearingAids[selectedIndex];
                    int quantity = (int)nudQuantity.Value;

                    // Add to cart
                    ProductService.AddToCart(hearingAid, quantity);
                    UIService.ShowSuccess(AppStrings.Messages.ItemAddedToCart);
                    LoadCart();
                }
            }
        }

        private void BtnRemoveFromCart_Click(object sender, EventArgs e) {
            // This functionality would remove items from the cart
            // For simplicity, we'll just show a message that this would be implemented
            UIService.ShowMessage("Remove from cart functionality would be implemented here.");
            // In a real application, you would:
            // 1. Get the selected cart item
            // 2. Remove it from the cart
            // 3. Refresh the cart display
        }

        private void BtnCheckout_Click(object sender, EventArgs e) {
            // Check if the cart is empty
            var cartItems = ProductService.GetCart();
            if (cartItems.Count == 0) {
                UIService.ShowError("Your cart is empty. Please add items before checkout.");
                return;
            }

            // Validate delivery address
            if (string.IsNullOrWhiteSpace(txtDeliveryAddress.Text)) {
                UIService.ShowError("Please enter a delivery address.");
                return;
            }

            // Process order
            var order = ProductService.ProcessOrder(txtDeliveryAddress.Text);
            if (order != null) {
                UIService.ShowSuccess(AppStrings.Messages.CheckoutSuccess);
                lblOrderStatus.Text = string.Format(AppStrings.Labels.OrderStatus, order.Status);
                LoadCart(); // Refresh cart (should be empty now)
            } else {
                UIService.ShowError("Failed to process order. Please try again.");
            }
        }
    }
}
