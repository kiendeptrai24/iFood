using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using iFood.Data;
using iFood.Models;
using iFood.ViewModels;

namespace MyMvcApp;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDBContext _context;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDBContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {
        var reponse = new LoginViewModel();
        
        return View(reponse);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {   
        if(!ModelState.IsValid) return View(loginVM);

        var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
        if(user != null)
        {
            // User is found, check passwork
            var PasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if(PasswordCheck)
            {
                // Password correct, sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if(result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            // password is incorrect
            TempData["Error"] = "Wrong credentials. please, try again";
            return View(loginVM);
        }
        //User not Found
        TempData["Error"] = "Wrong credentials. please, try again";
        return View(loginVM);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var response = new RegisterViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerViewModel);
        }

        var newUser = new AppUser()
        {
            Email = registerViewModel.EmailAddress,
            UserName = registerViewModel.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (!newUserResponse.Succeeded)
        {
            TempData["Error"] = "Your password must be at least 6 characters Include uppercase, lowercase, numbers and special characters";
            return View(registerViewModel);
        }

        await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login","Account");
    }
}
