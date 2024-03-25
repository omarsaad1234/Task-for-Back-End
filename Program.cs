using Microsoft.EntityFrameworkCore;
using Task_for_Back_End.Models;
using Task_for_Back_End.Services;

namespace Task_for_Back_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            var ConnStr = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(ConnStr));

            builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();

            builder.Services.AddScoped<IInvoiceHeaderService, InvoiceHeaderService>();

            builder.Services.AddScoped<ICashierService, CashierService>();

            builder.Services.AddScoped<IBranchService, BranchService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews(o => o.EnableEndpointRouting = false);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Invoice}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
