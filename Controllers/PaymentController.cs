using Microsoft.AspNetCore.Mvc;

using iFood.Interfaces;
using iFood.Models;
using System.Security.Claims;
using iFood.Data.Enum;
using Newtonsoft.Json;

namespace iFood.Controllers;

public class PaymentController : Controller
{
    private readonly IMomoService _momoService;
    private readonly IMomoRepository _momoRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    //private readonly IVnPayService _vnPayService;
    public PaymentController(IMomoService momoService, IMomoRepository momoRepository,
     IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _momoService = momoService;
        _momoRepository = momoRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        
    }
    [HttpGet]
    public async Task<IActionResult> CreatePaymentMomo(int id)
    {

        // var orderJson = HttpContext.Session.GetString("NewOrder");

        // if (string.IsNullOrEmpty(orderJson))
        //     return BadRequest("Order not found!");

        // var order = JsonConvert.DeserializeObject<Order>(orderJson);

        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found!";
            return RedirectToAction("Index", "Home");
        }
        
        var curUserId = HttpContext.User.GetUserId();
        _productRepository.CheckAttackProduct(product);
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
        OrderInfo model = new OrderInfo
        {
            OrderId = Guid.NewGuid().ToString(),
            Amount = double.Parse(newOrder.TotalPrice.ToString()),
            FullName = User.Identity.Name,
            OrderInfomation = "Momo payment for iFood website",
        };
        
        var response = await _momoService.CreatePaymentMomoAsync(model);
        if(response != null)
        {
            newOrder.PaymentMethod = PaymentMethod.Momo;
            _orderRepository.Add(newOrder);
        }
        return Redirect(response.PayUrl);
    }
    // [HttpPost]
    // public async Task<IActionResult> CreatePaymentMomo(OrderInfo model)
    // {
    //     var response = await _momoService.CreatePaymentMomoAsync(model);
    //     return Redirect(response.PayUrl);
    // }
    
    [HttpGet]
    public async Task<IActionResult> PaymentCallBack(MomoInfo model)
    {

        var orderJson = HttpContext.Session.GetString("NewOrder");

        if (string.IsNullOrEmpty(orderJson))
            return BadRequest("Order not found!");

        Order order = JsonConvert.DeserializeObject<Order>(orderJson);

        var response = await _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
        
        var requestQuery = HttpContext.Request.Query;
        foreach(var item in order.OrderDetails)
        {
            _productRepository.CheckAttackProduct(item.Product);
        }
        await _orderRepository.AddAsync(order);
        if(requestQuery["resultCode"] != 0)
        {
            var newMomoInsert = new MomoInfo
            {
                FullName = User.FindFirstValue(ClaimTypes.Email),
                Price = decimal.Parse(requestQuery["Amount"]),
                OrderInfo = requestQuery["orderInfo"],
                DatePaid = DateTime.Now,
                MomoTransactionId = requestQuery["transId"],
                AppUserId = order.AppUserId,
                OrderId = order.Id,
                Order = order,
            };
            await _momoRepository.AddAsync(newMomoInsert);

            order.PaymentMethod = PaymentMethod.Momo;
            order.MomoInfoId = newMomoInsert.Id;
            _orderRepository.Update(order);
            TempData["SuccessMessage"] = "Transaction successful";

        }
        else
        {
            TempData["ErrorMessage"] = "Failed transaction";
            return RedirectToAction("Index","Home");
        }
        return View(response);
    }
    
}
