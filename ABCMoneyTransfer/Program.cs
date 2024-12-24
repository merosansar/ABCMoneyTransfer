using ABCMoneyTransfer.Model;
using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ABCMoneyTransfer.Data;
using ABCMoneyTransfer.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// 1. Register the AbcremittanceDbContext (used by your main application)
builder.Services.AddDbContext<AbcremittanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Register the ABCMoneyTransferContext for Identity and other database operations
builder.Services.AddDbContext<ABCMoneyTransferContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ABCMoneyTransferContextConnection"))
);

// 3. Register the default Identity services, using your custom Identity model (ABCMoneyTransferUser)
builder.Services.AddDefaultIdentity<ABCMoneyTransferUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ABCMoneyTransferContext>();

// 4. Add controllers with views
builder.Services.AddControllersWithViews();

// 5. Register any scoped services
builder.Services.AddScoped<ExchangeRateService>();
builder.Services.AddScoped<TransactionService>();

// 6. Add Razor Pages (if you're using Razor Pages for the identity-related pages)
builder.Services.AddRazorPages();

// 7. If you want to add Identity management with custom settings (e.g., password policies)
// Uncomment and configure as needed
// builder.Services.AddIdentity<ABCMoneyTransferUser, IdentityRole>(options =>
// {
//     options.Password.RequireDigit = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = true;
//     options.Password.RequireLowercase = true;
//     options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
// })
// .AddEntityFrameworkStores<ABCMoneyTransferContext>()
// .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.

// 1. Use exception handling in production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();  // Enforce HTTPS
app.UseStaticFiles();        // Serve static files (CSS, JS, images)

app.UseRouting();            // Set up routing for controllers and pages

app.UseAuthentication();     // Enable authentication
app.UseAuthorization();      // Enable authorization

// 2. Map controller routes (MVC routes)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ExchangeRate}/{action=GetRates}/{id?}"
);

// 3. Map Razor Pages (for Identity pages and other Razor views)
app.MapRazorPages();

app.Run();