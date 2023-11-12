using DSED_M03_REST01.Data;
using M01_DAL_Municipalite_SQLServer;
using M01_Srv_Municipalite;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSED_M03_REST01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<MunicipaliteContextMYSQL>(options =>                  //changer pour MunicipalitesContextSQLServer
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MunicipaliteContextMYSQL>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();                                                   //<<<<<<<<<<<<<<<<<<<<<<<
            builder.Services.AddSwaggerDocument();                                              //<<<<<<<<<<<<<<<<<<<<<<<

            builder.Services.AddScoped<IDepotMunicipalites, DepotMunicipalitesMySQL>();         //<<<<<<<<<<<<<<<<<<<<<<<
            builder.Services.AddScoped<ManipulationMunicipalites>();                            //<<<<<<<<<<<<<<<<<<<<<<<

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();                                                //<<<<<<<<<<<<<<<<<<<<<<<
                app.UseDatabaseErrorPage();                                                     //<<<<<<<<<<<<<<<<<<<<<<<                                                   
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseOpenApi();                                               //<<<<<<<<<<<<<<<<<<<<<<< permet de configurer Nswagger
            app.UseSwaggerUi3();                                            //<<<<<<<<<<<<<<<<<<<<<<< permet de generer des page web pour faire des tests

            app.Run();
        }
    }
}