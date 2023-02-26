using Microsoft.AspNetCore.Mvc;
using E_CommerceAppLearningBinding.Models;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceAppLearningBinding.Controllers
{
    public class HomeController : Controller
    {
        [Route("/order")]
        public IActionResult Index([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice),
            nameof(Order.Products))] Order order)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorList = 
                    ModelState.Values.SelectMany(value =>
                    value.Errors).Select(value => value.ErrorMessage).ToList();

                string errors = string.Join("\n", errorList);
                return BadRequest(errors);
            }

            return Json(new { orderNumber = order.OrderNo });
        }
    }
}
