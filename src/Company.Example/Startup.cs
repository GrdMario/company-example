namespace Company.Example
{
    using Company.Example.Application;
    using Company.Example.Infrastructure.Database.Mssql;
    using Company.Example.Presentation.Api;
    using Hellang.Middleware.ProblemDetails;

    internal sealed class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public MssqlSettings MssqlSettings =>
            Configuration
                .GetSection(MssqlSettings.Key)
                .Get<MssqlSettings>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddInfrastructureDatabaseMssqlConfiguration(MssqlSettings);
            services.AddApplicationConfiguration();
            services.AddPresentationConfiguration(Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            if (!Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}