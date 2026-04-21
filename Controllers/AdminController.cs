using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; 
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers;

[Authorize] 
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}