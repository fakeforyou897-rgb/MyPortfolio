# Development Guide

## Getting Started

### Setup
```bash
git clone https://github.com/Mostafa-SAID7/MyPortfolio.git
cd MyPortfolio/MyPortfolio
dotnet restore --configfile ..\nuget.config
dotnet ef database update --configfile ..\nuget.config
dotnet run
```

### Project Structure
```
MyPortfolio/
├── Areas/Admin/              # Admin panel
├── Controllers/              # Public controllers
├── Data/                     # EF Core context
├── Extensions/               # Helper methods
├── Migrations/              # EF Core migrations
├── Models/
│   ├── Common/              # Base classes
│   ├── Entities/            # Domain models
│   ├── Enums/               # Enumerations
│   ├── ValueObjects/        # Value objects
│   └── ViewModels/          # View models
├── Views/                   # Razor views
└── wwwroot/                 # Static assets
```

## Adding New Features

### 1. Create Entity
```csharp
public class MyEntity : BaseEntity
{
    [Required]
    public required string Name { get; set; }
}
```

### 2. Add to DbContext
```csharp
public DbSet<MyEntity> MyEntities { get; set; }
```

### 3. Create Migration
```bash
dotnet ef migrations add AddMyEntity --configfile ..\nuget.config
dotnet ef database update --configfile ..\nuget.config
```

### 4. Create Controller
```csharp
public class MyEntityController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public MyEntityController(ApplicationDbContext context)
    {
        _context = context;
    }
}
```

### 5. Create Views
Create views in `Views/MyEntity/` folder.

## Common Tasks

### Add Category Support
Entities already have `CategoryId` foreign key. Just enable in UI.

### Add Search
Use `QueryableExtensions.ApplySearch()`:
```csharp
query = query.ApplySearch(searchTerm, 
    x => x.Title, 
    x => x.Description);
```

### Add Pagination
Use `QueryableExtensions.ToPagedResultAsync()`:
```csharp
var result = await query.ToPagedResultAsync(pageNumber, pageSize);
```

### Generate Slug
Use `StringExtensions.ToSlug()`:
```csharp
entity.Slug = entity.Title.ToSlug();
```

### Soft Delete
Entities automatically soft delete. To permanently delete:
```csharp
_context.IgnoreQueryFilters()
    .Remove(entity);
```

## Testing

### Unit Tests
```bash
dotnet test
```

### Manual Testing
1. Test public routes
2. Test admin CRUD operations
3. Test authentication/authorization
4. Test search and filters
5. Test pagination

## Code Style

- Use `required` keyword for mandatory properties
- Async/await for all database operations
- `ViewModels` for data transfer
- `Entities` never exposed directly to views
- Validate input with Data Annotations
- Use meaningful variable names
- Add XML documentation comments

## Common Extensions

### Slug Generation
```csharp
string slug = title.ToSlug();
```

### Search
```csharp
query.ApplySearch(searchTerm, x => x.Title, x => x.Content)
```

### Sort
```csharp
query.ApplySort("Title", isDescending: false)
```

### Pagination
```csharp
await query.ToPagedResultAsync(pageNumber, pageSize)
```

## Troubleshooting

### Migration Issues
```bash
dotnet ef migrations remove
dotnet ef database drop --force
dotnet ef migrations add Initial
dotnet ef database update
```

### NuGet Issues
```bash
dotnet nuget locals all --clear
dotnet restore --configfile ..\nuget.config --force
```

### Build Issues
```bash
dotnet clean
dotnet build --configfile ..\nuget.config
```
