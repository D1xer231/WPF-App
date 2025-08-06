using MyStore.Models;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using CloudIpspSDK;
using CloudIpspSDK.Checkout;



namespace Myapp8;
public partial class MainWindow : Window
{
    public ObservableCollection<Items> CartItems = new ObservableCollection<Items>(); // Коллекция для хранения товаров в корзине
    public int TotalQuantity { get; set; } // Общее количество товаров в корзине
    public int TotalSum { get; set; } // Общая сумма товаров в корзине

    private AppDbContext db;
    public MainWindow()
    {
        InitializeComponent();

        db = new AppDbContext();
        List<Items> items = db.Item.ToList(); // Получаем список товаров из базы данных

        ItemsList.ItemsSource = items; // Устанавливаем источник данных для ListBox
    }

    private void AddToCart_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Добавить в корзину"
    {
        if(sender is Button btn && btn.DataContext is Items item)
        {
            CartItems.Add(item); // Добавляем выбранный товар в корзину

            int totalQuantity = CartItems.Count; // Подсчитываем общее количество товаров в корзине
            int totalSum = CartItems.Sum(i => i.Price);// Calculate total sum of prices

            ZeroCartField.Visibility = Visibility.Hidden;

            QiauntityField.Content = $"Количество: {totalQuantity} шт";
            TotalPriceField.Content = $"Общая стоимость: €{totalSum}";

            QiauntityField.Visibility = Visibility.Visible; // делает поля видимыми после того как появился товар (Hidden)
            TotalPriceField.Visibility = Visibility.Visible;

            MessageBox.Show($"Товар '{item.Name}' добавлен в корзину.");
        }
        if (CartItems.Count == 0)
        {
            // Корзина пустая — скрываем счетчики и показываем сообщение
            QiauntityField.Visibility = Visibility.Hidden;
            TotalPriceField.Visibility = Visibility.Hidden;
            MessageBox.Show("Корзина пуста.");
        }
    }

    private void PayBtn_Click(object sender, RoutedEventArgs e)
    {
        if (CartItems.Count == 0)
        {
            // Корзина пустая — скрываем счетчики и показываем сообщение
            QiauntityField.Visibility = Visibility.Hidden;
            TotalPriceField.Visibility = Visibility.Hidden;
            MessageBox.Show("Корзина пуста.");
        }
        if(CartItems.Count > 0)
        {
            Config.MerchantId = 1396424;
            Config.SecretKey = "test";

            int totalSum = CartItems.Sum(item => item.Price);

            int total = CartItems.Count;

            var req = new CheckoutRequest
            {
                order_id = Guid.NewGuid().ToString("N"),
                amount = totalSum * 100,
                order_desc = $"Общее количество предметов: {total}.", // описание заказа
                currency = "EUR"
            };
            var resp = new Url().Post(req);
            if (resp.Error == null)
            {
                string url = resp.checkout_url;
                if (resp.Error == null)
                {
                    url = resp.checkout_url;

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo // открываем ссылку в браузере тот который выбран у пользователя по умолчанию
                    {
                        FileName = url,// ссылка на платеж
                        UseShellExecute = true // позволяет открыть ссылку в браузере
                    });
                }
                else
                {
                    MessageBox.Show($"Ошибка при создании платежа: {resp.Error}"); // Выводим сообщение об ошибке
                }
            }
        }

    }
}