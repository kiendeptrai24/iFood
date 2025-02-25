using Microsoft.AspNetCore.Mvc;

using iFood.Interfaces;
using iFood.Models;
using System.Security.Claims;

namespace iFood.Controllers;

public class PaymentController : Controller
{
    private readonly IMomoService _momoService;
    private readonly IMomoRepository _momoRepository;
    //private readonly IVnPayService _vnPayService;
    public PaymentController(IMomoService momoService, IMomoRepository momoRepository)
    {
        _momoService = momoService;
        _momoRepository = _momoRepository;
        
    }
    [HttpPost]
    public async Task<IActionResult> CreatePaymentMomo(OrderInfo model)
    {
        var response = await _momoService.CreatePaymentMomoAsync(model);
        return Redirect(response.PayUrl);
    }
    
    [HttpGet]
    public async Task<IActionResult> PaymentCallBack(MomoInfo model)
    {
        var response = await _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
        
        var requestQuery = HttpContext.Request.Query;

        if(requestQuery["resultCode"] != 0)
        {
            var newMomoInsert = new MomoInfo
            {
                FullName = User.FindFirstValue(ClaimTypes.Email),
                Price = decimal.Parse(requestQuery["Amount"]),
                OrderInfo = requestQuery["orderInfo"],
                DatePaid = DateTime.Now,
                MomoTransactionId = requestQuery["transId"]
            };
            // _momoRepository.Add(newMomoInsert);
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
