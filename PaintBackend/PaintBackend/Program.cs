using Microsoft.EntityFrameworkCore;
using PaintBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS policy to allow both Flutter and Swagger UI access
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://10.0.2.2", "http://localhost")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaintDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Enable CORS
app.UseCors(MyAllowSpecificOrigins);

// Enable Swagger for API testing
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional: disable this if you're only using HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
