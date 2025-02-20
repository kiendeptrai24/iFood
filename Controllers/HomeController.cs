using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iFood.Models;
using iFood.Data;
using iFood.Interfaces;
using iFood.ViewModels;

namespace iFood.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger,IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeVM = new HomeViewModel();
        homeVM.Products = await _productRepository.GetAll();
        return View(homeVM);
    }

    public async Task<IActionResult> Privacy()
    {
        var products = await _productRepository.GetAll();
        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
