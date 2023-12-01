using FluentValidation;
using Microsoft.AspNetCore.Identity;
using VebTechTask.BLL.AutoMapper;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.Services.Inrefaces;
using VebTechTask.BLL.Services;
using VebTechTask.BLL.Validation.Interfaces;
using VebTechTask.BLL.Validation;
using VebTechTask.DAL.Repositories.Interfaces;
using VebTechTask.DAL.Repositories;
using VebTechTask.DAL.Data;
using Library.BLL.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VebTechTask.BLL.AutoMapper.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VebTech.BLL.Validation;

namespace VebTechTask.UI.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<TokenService>();
        }

        public static void AddCustomMappers(this IServiceCollection services)
        {
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IValidator<CreateUserDTO>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserValidator>();
            services.AddScoped<IValidationPipelineBehavior<CreateUserDTO, CreateUserDTO>, ValidationPipelineBehavior<CreateUserDTO, CreateUserDTO>>();
            services.AddScoped<IValidationPipelineBehavior<UpdateUserDTO, UpdateUserDTO>, ValidationPipelineBehavior<UpdateUserDTO, UpdateUserDTO>>();
            services.AddScoped<IPasswordHasher<UserDTO>, PasswordHasher<UserDTO>>();
        }

        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy =>
                    policy.RequireRole("Admin", "User"));
            });
        }

        public static void AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "WebTech",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });
        }

        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            });
        }
    }
}
