using Microsoft.AspNetCore.Mvc;
using tasinmazYonetimi.Data; 
using Microsoft.EntityFrameworkCore;
using tasinmazYonetimi.Mappings;
using tasinmazYonetimi.Services.Interfaces;
using tasinmazYonetimi.Services;
using tasinmazYonetimi.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using tasinmazYonetimi.Models;
namespace tasinmazYonetimi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IIlService, IlService>();
            builder.Services.AddScoped<IIlceServices, IlceServices>();
            builder.Services.AddScoped<IMahalleServices, MahalleServices>();
            builder.Services.AddScoped<IKullaniciServices, KullaniciServices>();
            builder.Services.AddScoped<ILogServices, LogServices>();
            builder.Services.AddScoped<ITasinmazService, TasinmazService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("VarsayilanBaglanti")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers()
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPasswordHasher<Kullanici>, PasswordHasher<Kullanici>>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();

        }
    }
}