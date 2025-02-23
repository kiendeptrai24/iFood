using Microsoft.AspNetCore.Mvc;
using iFood.Models;
using iFood.Interfaces;
using iFood.ViewModels;
using iFood.Data.Enum;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using iFood.Data;

namespace iFood.Controllers;

public class CartController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;

    public CartController( UserManager<AppUser> userManager,IProductRepository productRepository, ICartRepository cartRepository)
    {
        _userManager = userManager;
        _productRepository = productRepository;
        _cartRepository = cartRepository;
    }

    // public async Task<IActionResult> Index()
    // {
    //     IEnumerable<Product> products = await _productRepository.GetAll();
    //     return View(products);
    // }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Cart> carts= await _cartRepository.GetAll();

        return View(carts);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Cart cart = await _cartRepository.GetByIdAsync(id);
        return View(cart);

    }
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var curUser = await HttpContext.User.GetUserById(_userManager);
        var curUserId = HttpContext.User.GetUserId();
        var productToCart = await _productRepository.GetByIdAsync(id);
        if (productToCart == null)
        {
            return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
        }
        var cart = new Cart
        {
            AppUserId = curUserId,
            AppUser = curUser,
            ProductId = productToCart.ProductID,
            product = productToCart,
            Quantity = 1,
            Price = productToCart.Price,
        };
        if(cart != null)
        {
            productToCart.Category--;
            _productRepository.Update(productToCart);
        }

        _cartRepository.Add(cart);
        return  Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng!" });
    }
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return View("Error");
        var productVM = new EditProductViewModel
        {
            Title = product.Name,
            Description = product.Description,
            Date = product.DateSell,
            Quantity = product.Quantity,
            Price = product.Price,
            Category = product.Category,
            URL = product.Image
        };
        
        return View(productVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
    {
        // if (!ModelState.IsValid)
        // {
        //     ModelState.AddModelError("", "Failed to edit club");
        //     return View(productVM);
        // }

        // var userProduct = await _productRepository.GetByIdAsyncNoTracking(id);

        // if (userProduct == null)
        // {
        //     return View("Error");   
        // }
        // Product product;
        // if(productVM.Image !=  null)
        // {
        //     var photoResult = await _photoService.AddPhotoAsync(productVM.Image);

        //     if (photoResult.Error != null)
        //     {
        //         ModelState.AddModelError("Image", "Photo upload failed");
        //         return View(productVM);
        //     }
        //     if (!string.IsNullOrEmpty(userProduct.Image))
        //     {
        //         var photo = _photoService.DeletePhotoAsync(userProduct.Image);
        //     }
        //     product = new Product
        //     {
        //         ProductID = id,
        //         Name = productVM.Title,
        //         Description = productVM.Description,
        //         DateSell = productVM.Date,
        //         Image = photoResult.Url.ToString(),
        //         URL = productVM.URL,
        //         Category = productVM.Category,
        //         Quantity = productVM.Quantity,
        //         Price = productVM.Price,
        //     };
        // }
        // else{
        //     product = new Product
        //     {
        //         ProductID = id,
        //         Name = productVM.Title,
        //         Description = productVM.Description,
        //         DateSell = productVM.Date,
        //         Image = productVM.URL,
        //         Category = productVM.Category,
        //         Quantity = productVM.Quantity,
        //         Price = productVM.Price,
        //     };
        // }




        // _productRepository.Update(product);

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var productDetails = await _productRepository.GetByIdAsync(id);
        if (productDetails == null) return View("Error");
        return View(productDetails);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        // var productDetails = await _productRepository.GetByIdAsync(id);

        // if (productDetails == null)
        // {
        //     return View("Error");
        // }

        // if (!string.IsNullOrEmpty(productDetails.Image))
        // {
        //     _ = _photoService.DeletePhotoAsync(productDetails.Image);
        // }

        // _productRepository.Delete(productDetails);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult HandleRequest()
    {
        return Json(new { message = "Yêu cầu đã được xử lý thành công!" });
    }
}
