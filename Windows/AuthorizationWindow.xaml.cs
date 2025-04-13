using System.Windows;
using FoodProducts.Models;

namespace FoodProducts.Windows;

/// <summary>
/// Логика взаимодействия для AuthorizationWindow.xaml
/// </summary>
public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
        InitializeComponent();
    }

    private void Login(User? user = null)
    {
        Data.User = user;
        Data.LoggedIn = true;
        new MainWindow().Show();
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (Data.User is null) return;
        tbEmail.Text = Data.User.Email;
    }

    // Войти
    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        using var ctx = new FoodProductsContext();

        var user = ctx.Users.Where(u => u.Email == tbEmail.Text && u.Password == pbPassword.Password);
        if (user.Count() == 1) // Успешный вход
        {
            Login(user.First());
        }
        else // Ошибка входа
        {
            MessageBox.Show("Логин и/или пароль неверны.", "Данные неверны", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Войти как гость
    private void btnLoginAsGuest_Click(object sender, RoutedEventArgs e)
    {
        Login();
    }
}
