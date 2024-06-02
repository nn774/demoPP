using demoPP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace demoPP.windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            TradeContext tradeContext = new TradeContext();
            List<Product> products = tradeContext.Products.ToList();
            foreach (var product in products)
            {
                byte[] photoBytes = File.ReadAllBytes("C:\\Users\\User\\Desktop\\09_1.6-2022_10\\Вариант 10\\Вариант 10\\Сессия 1\\picture.png");

                bool tt = false;
                foreach (var file in Directory.GetFiles("C:\\Users\\User\\Desktop\\09_1.6-2022_10\\Вариант 10\\Вариант 10\\Сессия 1\\Товар_import"))
                {

                    if (File.Exists(file))
                    {
                        if (product.ProductArticleNumber == file.Replace("C:\\Users\\User\\Desktop\\09_1.6-2022_10\\Вариант 10\\Вариант 10\\Сессия 1\\Товар_import\\", "").Replace(".jpg", "").Replace(".jpeg", ""))
                        {
                            byte[] photoByte = File.ReadAllBytes(file);
                            product.ProductPhoto = photoByte;
                            tt = true;
                        }
                    }
                }
                if (!tt)
                {
                    product.ProductPhoto = photoBytes;
                    tt = false;
                }
            }
            tradeContext.SaveChanges();
        }
    }
}
