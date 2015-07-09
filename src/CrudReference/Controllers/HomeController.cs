using CrudReference.Models;
using Microsoft.AspNet.Mvc;

namespace CrudReference.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private IItemsRepository _items;

        public HomeController(IItemsRepository items)
        {
            _items = items;
        }

        // GET: /
        public IActionResult Index()
        {
            return View(_items.GetAll());
        }
    }
}
