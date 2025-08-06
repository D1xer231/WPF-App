using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class Items     {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public Items(int id, string name, string description, int price, string image)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
        }
        public Items() {}

        public override string ToString()
        {
            return Name + Description + Price; // Переопределяем метод ToString для удобного отображения информации о товаре
        }

        public string ImagePath => $"pack://application:,,,/img/{Image}"; // путь к изображению товара
    }
}
