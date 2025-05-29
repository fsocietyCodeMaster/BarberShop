using BarberShop.Context;
using BarberShop.Customized.SeedData;
using BarberShop.Model;
using BarberShop.Repository;
using BarberShop.UnitOfWork.Barber;
using BarberShop.UnitOfWork.BarberSchedule;
using BarberShop.UnitOfWork.BarberShop;
using BarberShop.UnitOfWork.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Seri-log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/ResponseInformation.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BarberShopDbContext>(option =>
option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddIdentity<T_User, IdentityRole>(option =>
{
    option.User.RequireUniqueEmail = false;


}).AddEntityFrameworkStores<BarberShopDbContext>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});

var config = builder.Configuration;
builder.Services.AddSwaggerGen(c =>
{
    // Add the JWT Authorization header to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token with Bearer prefix, e.g., 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BarberShop API",
        Version = "v1",
        Description = "An API to perform Barber appointment stuff operations",
        Contact = new OpenApiContact
        {
            Name = "Meisam Sohrabi",
            Email = "empty.",
        },
    });
});

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthentication,AuthService>();
builder.Services.AddScoped<IUser,UserService>();
builder.Services.AddScoped<IBarberShop,BarberShopService>();
builder.Services.AddScoped<IBarber,BarberService>();
builder.Services.AddScoped<IBarberSchedule,BarberScheduleService>();
builder.Services.AddScoped<SeedingData>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(policy =>
{
    policy.AddPolicy(name: "AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:7150");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())  // The 'using' ensures proper disposal
    {
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<SeedingData>();
        await seeder.SeedAdmins();  // Seed data
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
