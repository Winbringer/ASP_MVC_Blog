using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }

    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", Price = 220 });
            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });
            var menuItems = new List<MenuItem>
        {
            new MenuItem{Id=1, Header = "Главная", Url = "/Home/Index", Order = 1},
            new MenuItem{Id=2, Header = "О сайте", Url = "/Home/About", Order = 2},
            new MenuItem{Id=3, Header = "Контакты", Url = "/Home/Contact", Order = 3},
            new MenuItem{Id=4, Header = "Меню второго уровня 1", Url = "#", Order = 1, ParentId = 2},
            new MenuItem{Id=5, Header = "Меню второго уровня 2", Url = "#", Order = 2, ParentId = 2},
            new MenuItem{Id=6, Header = "Меню второго уровня 3", Url = "#", Order = 3, ParentId = 2},
            new MenuItem{Id=7, Header = "Меню третьго уровня 1", Url = "#",  Order = 1, ParentId = 4},
            new MenuItem{Id=8, Header = "Меню третьго уровня 2", Url = "#", Order = 2, ParentId = 4},
            new MenuItem{Id=9, Header = "Меню третьго уровня 3", Url = "#", Order = 3, ParentId = 4}
        };
            db.MenuItems.AddRange(menuItems);
            base.Seed(db);
        }
    }

    public class Book
    {
        // ID книги
        public int Id { get; set; }
        // название книги
        public string Name { get; set; }
        // автор книги
        public string Author { get; set; }
        // цена
        public int Price { get; set; }
    }

    public class Purchase
    {
        // ID покупки
        public int PurchaseId { get; set; }
        // имя и фамилия покупателя
        public string Person { get; set; }
        // адрес покупателя
        public string Address { get; set; }
        // ID книги
        public int BookId { get; set; }
        // дата покупки
        public DateTime Date { get; set; }
    }
}
