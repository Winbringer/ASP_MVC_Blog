using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class Phone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Producer { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }

    public class PageController : Controller
    {
        List<Phone> phones;
        public PageController()
        {
            phones = new List<Phone>();
            phones.Add(new Phone { Id = 1, Model = "Samsung Galaxy III", Producer = "Samsung" });
            phones.Add(new Phone { Id = 2, Model = "Samsung Ace II", Producer = "Samsung" });
            phones.Add(new Phone { Id = 3, Model = "HTC Hero", Producer = "HTC" });
            phones.Add(new Phone { Id = 4, Model = "HTC One S", Producer = "HTC" });
            phones.Add(new Phone { Id = 5, Model = "HTC One X", Producer = "HTC" });
            phones.Add(new Phone { Id = 6, Model = "LG Optimus 3D", Producer = "LG" });
            phones.Add(new Phone { Id = 7, Model = "Nokia N9", Producer = "Nokia" });
            phones.Add(new Phone { Id = 8, Model = "Samsung Galaxy Nexus", Producer = "Samsung" });
            phones.Add(new Phone { Id = 9, Model = "Sony Xperia X10", Producer = "SONY" });
            phones.Add(new Phone { Id = 10, Model = "Samsung Galaxy II", Producer = "Samsung" });

        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 3; // количество объектов на страницу
            IEnumerable<Phone> phonesPerPages = phones.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = phones.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Phones = phonesPerPages };
            return View(ivm);
        }
    }
}