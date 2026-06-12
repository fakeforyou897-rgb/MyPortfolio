# MyPortfolio

A modern, feature-rich portfolio website built with ASP.NET Core 9.0, Entity Framework Core, and Tailwind CSS.

## Features

- **Project Showcase** - Display your projects with categories, tags, and detailed descriptions
- **Blog System** - Publish blog posts with SEO optimization, categories, and tags
- **Contact Form** - Receive and manage contact messages with status tracking
- **Admin Dashboard** - Full CRUD operations for projects, blog posts, and messages
- **User Management** - Role-based access control with ASP.NET Identity
- **Search & Filter** - Advanced search, filtering, sorting, and pagination
- **SEO Ready** - Meta tags, slugs, and sitemap support
- **Responsive Design** - Mobile-first design with Tailwind CSS

## Tech Stack

- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite (Entity Framework Core 9.0)
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Razor Views, Tailwind CSS
- **ORM**: Entity Framework Core with Code-First approach

## Quick Start

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Mostafa-SAID7/MyPortfolio.git
   cd MyPortfolio
   ```

2. **Restore dependencies**
   ```bash
   cd MyPortfolio
   dotnet restore --configfile ..\nuget.config
   ```

3. **Update database**
   ```bash
   dotnet ef database update --configfile ..\nuget.config
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Public site: `http://localhost:5098`
   - Admin panel: `http://localhost:5098/Admin`
   - Default admin credentials:
     - Email: `m.ssaid356@gmail.com`
     - Password: `Memo@3560`

## Project Structure

```
MyPortfolio/
├── Controllers/          # Public-facing controllers
├── Areas/Admin/          # Admin area (dashboard, management)
├── Models/
│   ├── Common/          # Base entities, pagination
│   ├── Entities/        # Domain entities (Project, BlogPost, etc.)
│   ├── Enums/           # Status enums
│   ├── ValueObjects/    # Email, URL, TechnologyStack
│   └── ViewModels/      # DTOs for views
├── Views/               # Razor views
├── Data/                # EF Core DbContext
├── Extensions/          # Helper extensions (Slug, Search, etc.)
└── wwwroot/            # Static files (CSS, JS, images)
```

## Key Features Explained

### Search, Filter & Sort
- Full-text search across titles, descriptions, and content
- Filter by category, tags, status, date ranges
- Sort by title, date, views, rating
- Pagination support with customizable page size

### Categories & Tags
- Hierarchical categories with parent-child relationships
- Many-to-many tagging system
- Tag usage tracking
- SEO-friendly slugs

### Soft Delete
- All entities support soft delete (records never physically deleted)
- Audit trail with created/updated/deleted timestamps
- Track who performed actions (CreatedBy, UpdatedBy, DeletedBy)

### Slug Generation
- Automatic URL-friendly slug generation
- Unique slugs for SEO
- Special character handling and normalization

## API Endpoints

### Public Routes
- `GET /` - Home page
- `GET /Projects` - List all projects
- `GET /Projects/{slug}` - Project details
- `GET /Blog` - List all blog posts
- `GET /Blog/{slug}` - Blog post details
- `GET /Contact` - Contact form
- `POST /Contact` - Submit contact message

### Admin Routes (Requires Authentication)
- `/Admin` - Dashboard
- `/Admin/Projects` - Manage projects
- `/Admin/BlogPosts` - Manage blog posts
- `/Admin/ContactMessages` - Manage messages
- `/Admin/Users` - Manage users and roles

## Configuration

Update `appsettings.json` for custom configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=MyPortfolio.db"
  }
}
```

## Database Schema

- **Projects** - Portfolio projects with categories and tags
- **BlogPosts** - Blog articles with SEO fields
- **ContactMessages** - Contact form submissions
- **Categories** - Hierarchical categories
- **Tags** - Tag system with usage tracking
- **AspNetUsers** - User accounts
- **AspNetRoles** - User roles (Admin, User, etc.)

## Development

### Add Migration
```bash
dotnet ef migrations add MigrationName --configfile ..\nuget.config
```

### Update Database
```bash
dotnet ef database update --configfile ..\nuget.config
```

### Build
```bash
dotnet build --configfile ..\nuget.config
```

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Author

**Mostafa SAID**
- GitHub: [@Mostafa-SAID7](https://github.com/Mostafa-SAID7)
- Email: m.ssaid356@gmail.com

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
