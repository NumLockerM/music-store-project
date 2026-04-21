using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Controllers;

public class AccountController : Controller
{
    public IActionResult Login() => View();
    public IActionResult Register() => View();
}