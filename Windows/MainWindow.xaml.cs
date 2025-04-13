using System.Windows;
using FoodProducts.Models;
using FoodProducts.Windows.AddEditForms;
using Microsoft.EntityFrameworkCore;

namespace FoodProducts.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обновляет интерфейс в зависимости от того, как вошел пользователь.
    /// </summary>
    private void UpdateUi()
    {
        var user = Data.User;

        Title = "Производство продуктов питания - Вы вошли как ";

        // Если пользователь вошел как гость
        if (user is null)
        {
            Title += "Гость";
            return;
        }

        // Если пользователь вошел как администратор/менеджер, раскрываем кнопки редактирования записей
        Title += $"{user.LastName} {user.FirstName[0]}. ";
        miEntities.Visibility = Visibility.Visible;
        gbProducts.Visibility = Visibility.Visible;
        gbProductOutputs.Visibility = Visibility.Visible;
        gbCompanies.Visibility = Visibility.Visible;

        switch (user.RoleId)
        {
            case 1:
                Title += "(Администратор)";
                break;

            case 2:
                Title += "(Менеджер)";

                // Убираем кнопки удаления записей
                miDeleteEntity.Visibility = Visibility.Collapsed;
                btnDeleteProduct.Visibility = Visibility.Collapsed;
                btnDeleteProductOutput.Visibility = Visibility.Collapsed;
                btnDeleteCompany.Visibility = Visibility.Collapsed;
                break;
        }
    }

    /// <summary>
    /// Загружает продукты в ListView.
    /// </summary>
    private void LoadProductsInListView()
    {
        using var ctx = new FoodProductsContext();
        ctx.ProductGroups.Load();
        ctx.ProductPackagingTypes.Load();
        lvProducts.ItemsSource = ctx.Products.ToList();
    }

    /// <summary>
    /// Загружает объем производства продуктов фирмами в ListView.
    /// </summary>
    private void LoadProductOutputsInListView()
    {
        using var ctx = new FoodProductsContext();
        ctx.Products.Load();
        ctx.Companies.Load();
        lvProductOutputs.ItemsSource = ctx.ProductOutputs.ToList();
    }

    /// <summary>
    /// Загружает фирмы-производители в ListView.
    /// </summary>
    private void LoadCompaniesInListView()
    {
        using var ctx = new FoodProductsContext();
        lvCompanies.ItemsSource = ctx.Companies.ToList();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Если пользователь не авторизован, закрываем окно
        if (!Data.LoggedIn)
        {
            Close();
            return;
        }

        UpdateUi();
        LoadProductsInListView();
        LoadProductOutputsInListView();
        LoadCompaniesInListView();
    }

    // Выход в окно авторизации
    private void miExit_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult confirmExit = MessageBox.Show("Вы уверены, что хотите выйти?",
            "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmExit == MessageBoxResult.No) return;

        Data.LoggedIn = false;
        new AuthorizationWindow().Show();
        Close();
    }

    // Справка
    private void miInfo_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Задание по производственной практике\n" +
            "Модуль: ПМ.02 Осуществление интеграции программных модулей\n" +
            "Разработать БД «Производство продуктов питания». Необходимо хранить информацию " +
            "о фирмах-производителях продуктов (код фирмы, название фирмы, адрес, фамилия директора), " +
            "продуктах (код, название, группа продуктов, вид упаковки), " +
            "а также об объеме производства продуктов фирмами.\n" +
            "Разработчик: Антонишин Кирилл Сергеевич ИСП-41\n",
            "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // Добавить продукт
    private void btnAddProduct_Click(object sender, RoutedEventArgs e)
    {
        Data.NewProduct = null;
        var addProductWindow = new AddEditProductWindow
        {
            Owner = this
        };
        addProductWindow.ShowDialog();
        LoadProductsInListView();
    }

    // Редактировать продукт
    private void btnEditProduct_Click(object sender, RoutedEventArgs e)
    {
        if (lvProducts.SelectedItem is not Product selectedProduct)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите редактировать.",
                "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Data.NewProduct = selectedProduct;
        var editProductWindow = new AddEditProductWindow
        {
            Owner = this
        };
        editProductWindow.ShowDialog();
        LoadProductsInListView();
    }

    // Удалить продукт
    private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        if (lvProducts.SelectedItem is not Product selectedProduct)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите удалить.",
                "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBoxResult confirmDelete = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
            "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmDelete == MessageBoxResult.No) return;

        try
        {
            using var ctx = new FoodProductsContext();
            ctx.Products.Remove(selectedProduct);
            ctx.SaveChanges();
            LoadProductsInListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Добавить объем производства
    private void btnAddProductOutput_Click(object sender, RoutedEventArgs e)
    {
        Data.NewProductOutput = null;
        var addProductOutputWindow = new AddEditProductOutputWindow
        {
            Owner = this
        };
        addProductOutputWindow.ShowDialog();
        LoadProductOutputsInListView();
    }

    // Редактировать объем производства
    private void btnEditProductOutput_Click(object sender, RoutedEventArgs e)
    {
        if (lvProductOutputs.SelectedItem is not ProductOutput selectedProductOutput)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите редактировать.",
                "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Data.NewProductOutput = selectedProductOutput;
        var editProductOutputWindow = new AddEditProductOutputWindow
        {
            Owner = this
        };
        editProductOutputWindow.ShowDialog();
        LoadProductOutputsInListView();
    }

    // Удалить объем производства
    private void btnDeleteProductOutput_Click(object sender, RoutedEventArgs e)
    {
        if (lvProductOutputs.SelectedItem is not ProductOutput selectedProductOutput)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите удалить.",
                "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBoxResult confirmDelete = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
            "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmDelete == MessageBoxResult.No) return;

        try
        {
            using var ctx = new FoodProductsContext();
            ctx.ProductOutputs.Remove(selectedProductOutput);
            ctx.SaveChanges();
            LoadProductOutputsInListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Добавить компанию
    private void btnAddCompany_Click(object sender, RoutedEventArgs e)
    {
        Data.NewCompany = null;
        var addCompanyWindow = new AddEditCompanyWindow
        {
            Owner = this
        };
        addCompanyWindow.ShowDialog();
        LoadCompaniesInListView();
    }

    // Редактировать компанию
    private void btnEditCompany_Click(object sender, RoutedEventArgs e)
    {
        if (lvCompanies.SelectedItem is not Company selectedCompany)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите редактировать.",
                "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Data.NewCompany = selectedCompany;
        var editCompanyWindow = new AddEditCompanyWindow
        {
            Owner = this
        };
        editCompanyWindow.ShowDialog();
        LoadCompaniesInListView();
    }

    // Удалить компанию
    private void btnDeleteCompany_Click(object sender, RoutedEventArgs e)
    {
        if (lvCompanies.SelectedItem is not Company selectedCompany)
        {
            MessageBox.Show("Выберите запись, которую Вы хотите удалить.",
                "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBoxResult confirmDelete = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
            "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmDelete == MessageBoxResult.No) return;

        try
        {
            using var ctx = new FoodProductsContext();
            ctx.Companies.Remove(selectedCompany);
            ctx.SaveChanges();
            LoadCompaniesInListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
