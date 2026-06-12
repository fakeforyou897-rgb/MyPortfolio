# Changelog

All notable changes to this project will be documented in this file.

## [2.0.0] - 2025-01-XX

### Added
- **Complete Architecture Overhaul**
  - GUID primary keys instead of int
  - Base entity with audit fields (CreatedAt, UpdatedAt, DeletedAt)
  - Soft delete support across all entities
  - User tracking (CreatedBy, UpdatedBy, DeletedBy)

- **Category System**
  - Hierarchical categories with parent-child relationships
  - Category support for projects and blog posts
  - Slug-based URLs for categories

- **Tag System**
  - Many-to-many tagging for projects and blog posts
  - Tag usage tracking
  - Tag filtering and search

- **Advanced Search & Filter**
  - Full-text search across multiple fields
  - Category and tag filtering
  - Status, date range, and custom filters
  - Dynamic sorting (ascending/descending)
  - Pagination with customizable page size

- **SEO Enhancements**
  - Unique slug generation for all entities
  - Meta title, description, keywords for blog posts
  - Clean, SEO-friendly URLs
  - Slug-based routing

- **Enhanced Models**
  - ProjectStatus enum (Planning, InProgress, Completed, OnHold, Archived)
  - BlogPostStatus enum (Draft, Published, Archived, Scheduled)
  - MessageStatus enum (New, Read, Replied, Archived, Spam)
  - Priority enum (Low, Medium, High, Critical)
  - View counter for projects and blog posts
  - Reading time calculation for blog posts
  - Featured flags for highlighting content

- **Value Objects**
  - Email value object with validation
  - URL value object with validation
  - TechnologyStack value object for parsing

- **Query Extensions**
  - Pagination extension
  - Search extension
  - Sort extension
  - String extensions (ToSlug, Truncate, StripHtml, CalculateReadingTime)

- **Database Improvements**
  - Comprehensive indexes for performance
  - Global query filters for soft delete
  - Many-to-many junction tables
  - Self-referencing categories

- **Documentation**
  - Complete README with quick start guide
  - Architecture documentation
  - Database schema documentation
  - API reference
  - Deployment guide
  - Development guide
  - Features list
  - Contributing guidelines
  - Code of conduct

### Changed
- Migrated from SQLServer to SQLite for easier setup
- Updated to .NET 9.0
- Improved controller structure with proper namespaces
- Enhanced view models with validation attributes
- Better separation between entities and view models

### Fixed
- NuGet package restoration issues
- Namespace inconsistencies
- Missing required properties

## [1.0.0] - 2024-XX-XX

### Initial Release
- Basic project portfolio
- Simple blog system
- Contact form
- Admin dashboard
- User authentication with ASP.NET Identity
- SQL Server database
