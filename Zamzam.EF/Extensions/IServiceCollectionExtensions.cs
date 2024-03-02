using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Application.Security;
using Zamzam.EF.Repositories;

namespace Zamzam.EF.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddEfLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddIdentity();
        services.AddAuthentication(configuration);

    }
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(connectionString,
               builder =>
               {
                   builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                   builder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null);
               }
               )
           );
    }
    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<ISaleOrderRepository, SaleOrderRepository>();
    }
    private static void AddIdentity(this IServiceCollection services)
    {
        //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
    }
    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
        //services.AddSingleton<IOptionsMonitor<JwtConfig>, OptionsMonitor<JwtConfig>>();
        services.AddTransient<Token>();
        services.AddTransient<AuthResult>();


        byte[]? key = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(option =>
        {
            option.SaveToken = true;
            option.RequireHttpsMetadata = true;
            option.TokenValidationParameters = tokenValidationParameters;

        });

    }
}
