using System.Text;
using System.Windows;
using FoodProducts.Models;

namespace FoodProducts.Windows.AddEditForms;

/// <summary>
/// Логика взаимодействия для AddEditCompanyWindow.xaml
/// </summary>
public partial class AddEditCompanyWindow : Window
{
    private readonly FoodProductsContext _ctx = new();
    private Company _company = new();

    public AddEditCompanyWindow()
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
            errors.AppendLine("Введите название");
        if (tbAddress.Text.Length == 0)
            errors.AppendLine("Введите адрес");
        if (tbDirectorFio.Text.Length == 0)
            errors.AppendLine("Введите ФИО директора");

        if (errors.Length <= 0) return true;

        MessageBox.Show($"""
            Недостаточно данных:
            {errors}
            """, "Ошибка добавления записи", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (Data.NewCompany is null)
        {
            Title = "Добавление фирмы";
            _company = new Company();
        }
        else
        {
            Title = "Редактирование фирмы";
            var companyToEdit = _ctx.Companies.Find(Data.NewCompany.Id);
            if (companyToEdit is null)
            {
                MessageBox.Show("Ошибка загрузки записи", "Запись не найдена", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            _company = companyToEdit;
        }

        DataContext = _company;
    }

    private void btnAddEditCompany_Click(object sender, RoutedEventArgs e)
    {
        if (!IsEnoughDataProvided()) return;

        try
        {
            if (Data.NewCompany is null)
            {
                _ctx.Companies.Add(_company);
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
