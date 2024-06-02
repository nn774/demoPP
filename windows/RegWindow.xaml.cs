using demoPP.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public TradeContext context;
        public RegWindow()
        {
            InitializeComponent();
            context = new TradeContext();
        }

        private void btnenter_Click(object sender, RoutedEventArgs e)
        {
            if (txtlogin.Text == "" || txtpass.Text == "" || txtsurname.Text == "" || txtname.Text == "" || txtPatronymic.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                List<User> users = new TradeContext().Users.ToList();
                var find = users.FirstOrDefault(u => u.UserLogin == txtlogin.Text);

                if (find == null)
                {
                    User user = new User();
                    user.UserId = users.Count() + 1;
                    user.UserLogin = txtlogin.Text;
                    user.UserPassword = (txtpass.Text);


                    user.UserSurname = txtsurname.Text;
                    user.UserName = txtname.Text;
                    user.UserPatronymic = txtPatronymic.Text;

                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались");
                }
                else if (find != null)
                {
                    MessageBox.Show("Логин занят");
                }

            }
        }

        private void btnleave_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow win = new AuthWindow();
            win.Show();
            this.Close();
        }
    }
}
