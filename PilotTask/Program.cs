

using PilotTask.BusinessLogicLayers;
using PilotTask.DataAccessLayers;
using PilotTask.Helpers;

namespace PilotTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<DatabaseHelper>();
            builder.Services.AddScoped<ProfileDataAccessLayer>();
            builder.Services.AddScoped<TaskDataAccessLayer>();
            builder.Services.AddScoped<ProfileBusinessLogicLayer>();
            builder.Services.AddScoped<TaskBusinessLogicLayer>();

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


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
                pattern: "{controller=Profile}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
