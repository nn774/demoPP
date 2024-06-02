using System;
using System.Windows;
using demoPP.Model;

namespace demoPP.windows
{
    public partial class AddEditWindow : Window
    {
        public Product EditedProduct { get; private set; }

        public AddEditWindow(Product product = null)
        {
            InitializeComponent();

            if (product != null)
            {
                txtArticleNumber.Text = product.ProductArticleNumber;
                txtProductName.Text = product.ProductName;
                txtDescription.Text = product.ProductDescription;
                txtCategory.Text = product.ProductCategory;
                txtManufacturer.Text = product.ProductManufacturer;
                txtCost.Text = product.ProductCost.ToString();
                txtDiscount.Text = product.ProductDiscountAmount.ToString();
                txtQuantity.Text = product.ProductQuantityInStock.ToString();
                txtStatus.Text = product.ProductStatus.ToString();
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticleNumber.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                string.IsNullOrWhiteSpace(txtManufacturer.Text) ||
                string.IsNullOrWhiteSpace(txtCost.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            if (!decimal.TryParse(txtCost.Text, out decimal cost) ||
                !sbyte.TryParse(txtDiscount.Text, out sbyte discount) ||
                !int.TryParse(txtQuantity.Text, out int quantity) ||
                !sbyte.TryParse(txtStatus.Text, out sbyte status))
            {
                MessageBox.Show("Invalid numeric input.");
                return;
            }

            EditedProduct = new Product
            {
                ProductArticleNumber = txtArticleNumber.Text,
                ProductName = txtProductName.Text,
                ProductDescription = txtDescription.Text,
                ProductCategory = txtCategory.Text,
                ProductManufacturer = txtManufacturer.Text,
                ProductCost = cost,
                ProductDiscountAmount = discount,
                ProductQuantityInStock = quantity,
                ProductStatus = status
            };

            DialogResult = true;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
