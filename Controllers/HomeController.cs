using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

   
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        
        var products = await _context.Products.Take(3).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Catalog()
    {
       
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    public IActionResult Contacts() => View();
    public IActionResult Shipping() => View();
}