using demoPP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        public List<User> users;
        public AuthWindow()
        {
            InitializeComponent();
            users = new TradeContext().Users.ToList();
        }

        private void btnenter_Click(object sender, RoutedEventArgs e)
        {
            Login(txtlogin.Text, txtpass.Text, users);
        }

        private void btnreg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow win = new RegWindow();
            win.Show();
            this.Close();
        }
        public void Login(string login, string pass, List<User> users)
        {
            User user = users.FirstOrDefault(u => u.UserLogin == login && u.UserPassword == pass);
            if (user == null){
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            int role = user.UserRole;
            MessageBox.Show("Добро пожаловать " + user.UserName);
            Authorization.User = user;
            Authorization.Role = role;
            MainControlWindow mainControlWIndow = new MainControlWindow();
            mainControlWIndow.Show();
            Close();
        }
    }
}
