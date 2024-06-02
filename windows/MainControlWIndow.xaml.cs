using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using demoPP.Model;

namespace demoPP.windows
{
    public partial class MainControlWindow : Window
    {
        private TradeContext tradeContext = new TradeContext();
        private List<Product> allProducts;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages;
        private List<Product> originalProducts;

        public MainControlWindow()
        {
            InitializeComponent();
            allProducts = tradeContext.Products.ToList();
            originalProducts = allProducts.ToList();
            LoadFilters();
            LoadProducts();
            CheckAdminPermissions();
        }

        private void LoadProducts()
        {
            allProducts = tradeContext.Products.ToList();
            totalPages = (int)Math.Ceiling((double)allProducts.Count / itemsPerPage);
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            var productsToDisplay = allProducts
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            ProductsListView.ItemsSource = productsToDisplay;
            PageInfoTextBlock.Text = $"Page {currentPage} of {totalPages}";
        }

        private void LoadFilters()
        {
            var categories = originalProducts.Select(p => p.ProductCategory).Distinct().ToList();
            categories.Insert(0, "Категория");
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedIndex = 0;

            var manufacturers = originalProducts.Select(p => p.ProductManufacturer).Distinct().ToList();
            manufacturers.Insert(0, "Производитель");
            ManufacturerComboBox.ItemsSource = manufacturers;
            ManufacturerComboBox.SelectedIndex = 0;
        }

        private void CheckAdminPermissions()
        {
            if (Authorization.Role != 1)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string searchText = SearchTextBox.Text.ToLower();
            string selectedCategory = CategoryComboBox.SelectedItem as string;
            string selectedManufacturer = ManufacturerComboBox.SelectedItem as string;

            var filteredProducts = originalProducts.Where(p =>
                (string.IsNullOrEmpty(searchText) ||
                 p.ProductName.ToLower().Contains(searchText) ||
                 p.ProductDescription.ToLower().Contains(searchText) ||
                 p.ProductCategory.ToLower().Contains(searchText) ||
                 p.ProductManufacturer.ToLower().Contains(searchText)) &&
                (selectedCategory == "Категория" || p.ProductCategory == selectedCategory) &&
                (selectedManufacturer == "Производитель" || p.ProductManufacturer == selectedManufacturer)).ToList();

            allProducts = filteredProducts;
            totalPages = (int)Math.Ceiling((double)allProducts.Count / itemsPerPage);
            currentPage = 1;
            DisplayCurrentPage();
        }

        private void OnAddProductClick(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new AddEditWindow();

            if (addEditWindow.ShowDialog() == true)
            {
                var newProduct = addEditWindow.EditedProduct;

                if (newProduct != null)
                {
                    tradeContext.Products.Add(newProduct);
                    tradeContext.SaveChanges();
                    LoadProducts();
                }
            }
        }

        private void OnEditProductClick(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                var addEditWindow = new AddEditWindow(selectedProduct);

                if (addEditWindow.ShowDialog() == true)
                {
                    Product product = tradeContext.Products.FirstOrDefault(p => p.ProductArticleNumber == selectedProduct.ProductArticleNumber);

                    if (product != null)
                    {
                        product.ProductArticleNumber = addEditWindow.EditedProduct.ProductArticleNumber;
                        product.ProductName = addEditWindow.EditedProduct.ProductName;
                        product.ProductDescription = addEditWindow.EditedProduct.ProductDescription;
                        product.ProductCategory = addEditWindow.EditedProduct.ProductCategory;
                        product.ProductManufacturer = addEditWindow.EditedProduct.ProductManufacturer;
                        product.ProductCost = addEditWindow.EditedProduct.ProductCost;
                        product.ProductDiscountAmount = addEditWindow.EditedProduct.ProductDiscountAmount;
                        product.ProductQuantityInStock = addEditWindow.EditedProduct.ProductQuantityInStock;
                        product.ProductStatus = addEditWindow.EditedProduct.ProductStatus;

                        tradeContext.SaveChanges();
                        LoadProducts();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void OnDeleteProductClick(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                tradeContext.Products.Remove(selectedProduct);
                tradeContext.SaveChanges();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }

        private void OnPreviousPageClick(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }
    }
}
