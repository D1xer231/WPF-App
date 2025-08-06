using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myapp8.models
{
    class Cart
    {
        public Items Product { get; set; }
        public int Quantity { get; set; } = 1;

        public int TotalPrice => Product.Price * Quantity;
    }
}
