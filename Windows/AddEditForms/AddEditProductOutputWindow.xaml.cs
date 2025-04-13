using System.Text;
using System.Windows;
using FoodProducts.Models;

namespace FoodProducts.Windows.AddEditForms;

/// <summary>
/// Логика взаимодействия для AddEditProductOutputWindow.xaml
/// </summary>
public partial class AddEditProductOutputWindow : Window
{
    private readonly FoodProductsContext _ctx = new();
    private ProductOutput _productOutput = new();

    public AddEditProductOutputWindow()
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
        if (cbCompany.SelectedItem is null)
            errors.AppendLine("Выберите фирму");
        if (cbProduct.SelectedItem is null)
            errors.AppendLine("Выберите продукт");
        if (int.TryParse(tbProductAmount.Text, out int a) && a < 0)
            errors.AppendLine("Введите корректное количество продуктов, произведенных фирмой");

        if (errors.Length <= 0) return true;

        MessageBox.Show($"""
            Недостаточно данных:
            {errors}
            """, "Ошибка добавления записи", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        cbCompany.ItemsSource = _ctx.Companies.ToList();
        cbProduct.ItemsSource = _ctx.Products.ToList();

        if (Data.NewProductOutput is null)
        {
            Title = "Добавление объема производства продуктов";
            _productOutput = new ProductOutput();
        }
        else
        {
            Title = "Редактирование объема производства продуктов";
            cbCompany.IsEnabled = false;
            cbProduct.IsEnabled = false;

            var productOutputToEdit = _ctx.ProductOutputs.Find(Data.NewProductOutput.CompanyId, Data.NewProductOutput.ProductId);
            if (productOutputToEdit is null)
            {
                MessageBox.Show("Ошибка загрузки записи", "Запись не найдена", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            _productOutput = productOutputToEdit;
        }

        DataContext = _productOutput;
    }

    private void btnAddEditProductOutput_Click(object sender, RoutedEventArgs e)
    {
        if (!IsEnoughDataProvided()) return;

        try
        {
            if (Data.NewProductOutput is null)
            {
                _ctx.ProductOutputs.Add(_productOutput);
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
