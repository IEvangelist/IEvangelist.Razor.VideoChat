using IEvangelist.Razor.VideoChat.Options;
using IEvangelist.Razor.VideoChat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Environment;

namespace IEvangelist.Razor.VideoChat
{
    public class Startup
    {
        readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.Configure<TwilioSettings>(settings =>
            {
                settings.AccountSid = GetEnvironmentVariable("TWILIO__ACCOUNT__SID");
                settings.ApiSecret = GetEnvironmentVariable("TWILIO__API__SECRET");
                settings.ApiKey = GetEnvironmentVariable("TWILIO__API__KEY");
            })
            .AddSingleton<ITwilioService, TwilioService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
