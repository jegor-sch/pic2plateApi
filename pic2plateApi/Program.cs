using pic2plateApi.Handler;
using pic2plateApi.Helpers;
using pic2plateApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Handler
builder.Services.AddScoped<RecipeHandler>();

// Repos
builder.Services.AddScoped<RecipeRepository>();

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

app.MapControllers();

app.UseHttpsRedirection();

app.Run();