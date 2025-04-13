using System.Text;
using System.Windows;
using FoodProducts.Models;

namespace FoodProducts.Windows.AddEditForms;

/// <summary>
/// Логика взаимодействия для AddEditProductWindow.xaml
/// </summary>
public partial class AddEditProductWindow : Window
{
    private readonly FoodProductsContext _ctx = new();
    private Product _product = new();

    public AddEditProductWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Проверяет, достаточно ли введенных данных для сохранения записи.
    /// Если нет, выводит окно с информацией о недостающих данных.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>, если данных достаточно; иначе, <see langword="false"/>.
    /// </returns>
    private bool IsEnoughDataProvided()
    {
        var errors = new StringBuilder();
        if (tbName.Text.Length == 0)
            errors.AppendLine("Введите наименование");
        if (cbGroup.SelectedItem is null)
            errors.AppendLine("Выберите группу");
        if (cbPackagingType.SelectedItem is null)
            errors.AppendLine("Выберите вид упаковки");

        if (errors.Length <= 0) return true;

        MessageBox.Show($"""
            Недостаточно данных:
            {errors}
            """, "Ошибка добавления записи", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        cbGroup.ItemsSource = _ctx.ProductGroups.ToList();
        cbPackagingType.ItemsSource = _ctx.ProductPackagingTypes.ToList();

        if (Data.NewProduct is null)
        {
            Title = "Добавление продукта";
            _product = new Product();
        }
        else
        {
            Title = "Редактирование продукта";
            var productToEdit = _ctx.Products.Find(Data.NewProduct.Id);
            if (productToEdit is null)
            {
                MessageBox.Show("Ошибка загрузки записи", "Запись не найдена", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            _product = productToEdit;
        }

        DataContext = _product;
    }

    // Сохранить
    private void btnAddEditProduct_Click(object sender, RoutedEventArgs e)
    {
        if (!IsEnoughDataProvided()) return;

        try
        {
            if (Data.NewProduct is null)
            {
                _ctx.Products.Add(_product);
            }
            _ctx.SaveChanges();
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Ошибка добавления записи", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
