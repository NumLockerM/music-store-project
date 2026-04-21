using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ИНИЦИАЛИЗАЦИЯ ДАННЫХ
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    // 1. Расширяем список категорий
    if (!context.Categories.Any())
    {
        var guitarCat = new Category { Name = "Гитары" };
        var keysCat = new Category { Name = "Клавишные" };
        var drumsCat = new Category { Name = "Ударные" };
        var windCat = new Category { Name = "Духовые" };
        var accCat = new Category { Name = "Аксессуары" };
        
        context.Categories.AddRange(guitarCat, keysCat, drumsCat, windCat, accCat);
        context.SaveChanges();

        // 2. Добавляем тестовый товар с указанием количества (StockQuantity)
        if (!context.Products.Any())
        {
            context.Products.Add(new Product { 
                Name = "Fender Stratocaster", 
                Price = 95000, 
                CategoryId = guitarCat.Id,
                StockQuantity = 5, // Теперь это поле будет заполнено в базе
                Description = "Легендарная электрогитара" 
            });
            context.SaveChanges();
        }
    }
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();