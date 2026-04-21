using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Controllers;

public class CartController : Controller
{
    public IActionResult Index() => View();
}