using FluentValidation;
using UserPtoject.Interfaces;
using UserPtoject.Models;
using UserPtoject.Repositories;
using UserPtoject.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IValidator<User>, CreateUserValidation>();
builder.Services.AddTransient<IValidator<User>, UpdateUserValidation>();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.AccessDeniedPath = "/User/Error";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");
app.Run();
