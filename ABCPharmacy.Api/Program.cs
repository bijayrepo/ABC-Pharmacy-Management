
using ABCPharmacy.Api.Middleware;
using ABCPharmacy.Api.Repositories;
using ABCPharmacy.Api.Services;

namespace ABCPharmacy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add Controllers
            builder.Services.AddControllers();


            builder.Services.AddOpenApi();

            // Repository Registration
            builder.Services.AddScoped<IMedicineRepository, JsonMedicineRepository>();

            builder.Services.AddScoped<ISaleRepository, JsonSaleRepository>();


            // Service Registration
            builder.Services.AddScoped<IMedicineService, MedicineService>();

            builder.Services.AddScoped<ISaleService, SaleService>();


            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularApp",
                    policy =>
                    {
                        policy
                        .WithOrigins(
                            "http://localhost:60429",
                            "http://localhost:4200"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });


            var app = builder.Build();


            // Exception Middleware
            app.UseMiddleware<ExceptionMiddleware>();


            // Swagger
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();


            }


            app.UseHttpsRedirection();


            // Enable Angular CORS
            app.UseCors("AngularApp");


            app.MapControllers();


            app.Run();
            
        }
    }
}
