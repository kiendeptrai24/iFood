using Microsoft.AspNetCore.Mvc;

using iFood.Interfaces;
using iFood.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace iFood.Controllers;

public class OrderController : Controller
{

    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<AppUser> _userManager;
    public OrderController(UserManager<AppUser> userManager,IOrderRepository orderRepository,IProductRepository productRepository)
    {
        _userManager = userManager;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
   
    [HttpGet]
    public async Task<IActionResult> CreateOrder(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found!";
            return RedirectToAction("Index", "Home");
        }
        
        var curUserId = HttpContext.User.GetUserId();
        var newOrder = new Order
        {
            AppUserId = curUserId,
            OrderDate = DateTime.Now,
            OrderDetails = new List<OrderDetail>
            {
                new OrderDetail()
                {
                    ProductId = product.ProductID,
                    Product = product,
                    Quantity = 1,
                }
            }
        };
        HttpContext.Session.SetString("NewOrder", JsonConvert.SerializeObject(newOrder));
        return RedirectToAction("CreatePaymentMomo", "Payment",id);
    }
}
