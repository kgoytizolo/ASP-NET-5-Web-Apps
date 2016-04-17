using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppLibrary
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();              //To define a file by default to be displayed in http://localhost:8080 
            app.UseStaticFiles();               //Use any default file from wwwroot folder
                                                //If app.UseDefaultFiles() is not defined yet above this method, you can access to static Files just
                                                //entering: http://localhost:8080/Index.html (or another static file)
                                                //But will not appear any default static file if only http://localhost:8080  is called 
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
