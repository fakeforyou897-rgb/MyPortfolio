using MyPortfolio.Configuration;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// SERVICES CONFIGURATION
// ============================================
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// ============================================
// MIDDLEWARE CONFIGURATION
// ============================================
app.UseApplicationMiddleware();
app.MapApplicationEndpoints();

// ============================================
// DATABASE SEEDING
// ============================================
await app.SeedDatabaseAsync();

// ============================================
// RUN APPLICATION
// ============================================
app.Run();

