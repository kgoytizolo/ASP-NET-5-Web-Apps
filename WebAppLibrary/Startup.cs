using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using WebAppLibrary.Services;
using WebAppLibrary.Models;
using WebAppLibrary.Repositories.Interface;
using WebAppLibrary.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using WebAppLibrary.ViewModels;

namespace WebAppLibrary
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;             //To be used in other parts of the app

        public Startup(IApplicationEnvironment appEnv) {
            //Create a builder to add environment variables :)
            var builder = new ConfigurationBuilder().               //Please keep same order for configuration
                SetBasePath(appEnv.ApplicationBasePath).            //Application's level
                AddJsonFile("config.json").                         //Additional config file created manually at project level
                AddEnvironmentVariables();                          
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();                                        //Dependency Injection of available services for web application.
            services.AddMvc().AddJsonOptions(opt => {                   //JsonOptions are added through lambda to configure serialization settings.    
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();         //Property names will be in JSon serialization namespace
            });
            services.AddLogging();                                      //DI - Enable logging in the configuration services (part of system assemblies)
            services.AddEntityFramework().                              //Dependency Injection of class LibraryContext only for Entity Framework 7
                AddSqlServer().                                         //Package EntityFramework.SqlServer 7.0.0 - EntityFramework.MicrosoftSqlServer          
                AddDbContext<LibraryContext>();
            services.AddTransient<LibraryContextSeedData>();            //Dependency Injection of class LibraryContextSeedData. Register seeder to add data 
            services.AddScoped<IMailService, DebugMailService >();      //Interface, related class (container with instance + class). 
                                                                        //We can change class for multiple purposes (Mock, testing, environment, etc)
            services.AddScoped<ILibraryRepository, LibraryRepository>();    //Other personalized component to be added into services container and used by DI
            services.AddScoped<GeoLocationService>();                      //Our GeoLocalizator component included into service container for further use through DI
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, LibraryContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            //Consider 2 types of logger factory: 
            loggerFactory.AddDebug(LogLevel.Warning);         //You can choose the type of data if is a critical error, etc. To be shown on Debug Window
            //app.UseDefaultFiles();                          //To define a file by default to be displayed in http://localhost:8080 
            app.UseStaticFiles();                             //Use any default file from wwwroot folder
                                                              //If app.UseDefaultFiles() is not defined yet above this method, you can access to static Files just
                                                              //entering: http://localhost:8080/Index.html (or another static file)
                                                              //But will not appear any default static file if only http://localhost:8080  is called 

            //When you initialize Mapper, they know which type of mapping will work with, also collections. 
            //You don't need to create specific maps for collection types    
            Mapper.Initialize(config =>
            {
                config.CreateMap<Book, BookViewModel>().ReverseMap();                   //To not add new mappings all the time. Is important to configure it fist
                config.CreateMap<ShoppingCart, ShoppingCartViewModel>().ReverseMap();   //Takes an action (lambda) and in the expression configure the action map
            });                                                             

            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",               //The route is still defining site/controller/action/
                    defaults: new { controller = "App", Action = "Index" }
                );
            });

            seeder.EnsureSeedData();
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
