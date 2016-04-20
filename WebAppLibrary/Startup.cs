using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using WebAppLibrary.Services;
using WebAppLibrary.Models;

namespace WebAppLibrary
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;             //To be used in other parts of the app

        public Startup(IApplicationEnvironment appEnv) {
            //Create a builder to add environment variables :)
            var builder = new ConfigurationBuilder().
                SetBasePath(appEnv.ApplicationBasePath).            //Application's level
                AddJsonFile("config.json").                         //Additional config file created manually at project level
                AddEnvironmentVariables();                          
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();                                          //Dependency Injection of available services for web application.
            services.AddEntityFramework().                              //Dependency Injection of class LibraryContext only for Entity Framework 7
                AddSqlServer().                                         //Package EntityFramework.SqlServer 7.0.0 - EntityFramework.MicrosoftSqlServer          
                AddDbContext<LibraryContext>();
            services.AddScoped<IMailService, DebugMailService >();      //Interface, related class (container with instance + class). 
                                                                        //We can change class for multiple purposes (Mock, testing, environment, etc)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //app.UseDefaultFiles();              //To define a file by default to be displayed in http://localhost:8080 
            app.UseStaticFiles();               //Use any default file from wwwroot folder
                                                //If app.UseDefaultFiles() is not defined yet above this method, you can access to static Files just
                                                //entering: http://localhost:8080/Index.html (or another static file)
                                                //But will not appear any default static file if only http://localhost:8080  is called 
            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",               //The route is still defining site/controller/action/
                    defaults: new { controller = "App", Action = "Index" }
                );
            });
            //app.UseIISPlatformHandler();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
