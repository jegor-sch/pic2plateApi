using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pic2plateApi.Handler;
using pic2plateApi.Helpers;
using pic2plateApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://pic2plate.netlify.app")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Handler
builder.Services.AddScoped<RecipeHandler>();
builder.Services.AddScoped<PreferenceHandler>();

// Repos
builder.Services.AddScoped<RecipeRepository>();
builder.Services.AddScoped<PreferenceRepository>();

// Helper
builder.Services.AddScoped<SqlConnectionProvider>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.UseHttpsRedirection();

app.Run();