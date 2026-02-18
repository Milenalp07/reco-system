var builder = WebApplication.CreateBuilder(args);

// Fixed URL for local development (optional but helpful)
builder.WebHost.UseUrls("http://localhost:5177");

// Add Controllers (MVC-style API)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable HTTPS redirect for now (simplifies local + Docker)
// app.UseHttpsRedirection();

// Map Controllers
app.MapControllers();

app.Run();
