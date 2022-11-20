using System.Text;
using DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Service;
using Service.MemorizeValues;
using Service.Statistics;
using Service.Users;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers().AddNewtonsoftJson(opts => 
    opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<UserService>();
services.AddScoped<StatisticsService>();
services.AddScoped<ValuesMemorizeService>();

services.AddAutoMapper(typeof(MapperProfile));
services.AddHttpContextAccessor();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
    .AddJwtBearer(options =>    
    {    
        options.TokenValidationParameters = new TokenValidationParameters    
        {    
            ValidateIssuer = true,    
            ValidateAudience = true,    
            ValidateLifetime = true,    
            ValidateIssuerSigningKey = true,    
            ValidIssuer = configuration["Jwt:Issuer"],    
            ValidAudience = configuration["Jwt:Issuer"],    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))    
        };    
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();