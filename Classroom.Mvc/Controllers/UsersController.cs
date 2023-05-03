using Classroom.Dto.Entities;
using Classroom.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Classroom.Mvc.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;


    public UsersController(UserManager<User> userManager,SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm] CreateUserDto createUserDto)
    {

        if (!ModelState.IsValid)
        {
            return View(createUserDto);
        }

        var user = new User()
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            PhoneNumber = createUserDto.PhoneNumber,
            UserName = createUserDto.Username,
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Username", result.Errors.First().Description);
            return View();
        }

        await _signInManager.SignInAsync(user, isPersistent: true);

        return RedirectToAction("Profile");
    }

    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User); 

        return View(user);
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromForm] SignInUserDto signInUserDto)
    {
        var result =
            await _signInManager.PasswordSignInAsync(signInUserDto.Username, signInUserDto.Password, true, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("Username","Username or password incorrect");
            return View();
        }
        return RedirectToAction("Profile");
    }
}