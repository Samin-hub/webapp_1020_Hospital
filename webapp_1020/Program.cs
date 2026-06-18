using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapp_1020.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddRazorPages(options =>
{
    // Admin_1020 ফোল্ডারের সব পেজ শুধুমাত্র Admin রোলের জন্য লক করা হলো
    options.Conventions.AuthorizeFolder("/Admin_1020", "RequireAdminRole");
    // Student_1020 ফোল্ডারের সব পেজ শুধুমাত্র Student রোলের জন্য লক করা হলো
    options.Conventions.AuthorizeFolder("/Student_1020", "RequireStudentRole");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
// ডাটাবেজ সিডিং এবং রোল ম্যানেজমেন্ট ইনিশিয়ালাইজেশন
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await webapp_1020.Data.DbInitializer.SeedRolesAndUsersAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "রোল ও ইউজার সিডিং করার সময় ত্রুটি ঘটেছে।");
    }
}
app.Run();
