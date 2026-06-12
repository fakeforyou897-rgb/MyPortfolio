# Database Schema

## Entities

### Projects
Portfolio projects with full metadata.

**Key Fields:**
- `Id` (Guid) - Primary key
- `Title` (string) - Project name
- `Slug` (string) - URL-friendly identifier (unique)
- `Description` (string) - Full description
- `Technologies` (string) - Tech stack used
- `CategoryId` (Guid?) - Foreign key to Category
- `Status` (enum) - Planning, InProgress, Completed, OnHold, Archived
- `IsFeatured` (bool) - Show on homepage
- `IsPublished` (bool) - Public visibility
- `ViewCount` (int) - Number of views
- Audit fields inherited from BaseEntity

**Relationships:**
- Many-to-One with Category
- Many-to-Many with Tags (via ProjectTag)

### BlogPosts
Blog articles with SEO support.

**Key Fields:**
- `Id` (Guid) - Primary key
- `Title` (string) - Post title
- `Slug` (string) - URL-friendly identifier (unique)
- `Content` (string) - Full article content
- `Summary` (string?) - Short description
- `CategoryId` (Guid?) - Foreign key to Category
- `Status` (enum) - Draft, Published, Archived, Scheduled
- `PublishedAt` (DateTime?) - Publication date
- `IsFeatured` (bool) - Show on homepage
- `IsPublished` (bool) - Public visibility
- `ViewCount` (int) - Number of views
- `MetaTitle`, `MetaDescription`, `MetaKeywords` - SEO fields

**Relationships:**
- Many-to-One with Category
- Many-to-Many with Tags (via BlogPostTag)

### ContactMessages
Contact form submissions with tracking.

**Key Fields:**
- `Id` (Guid) - Primary key
- `Name` (string) - Sender name
- `Email` (string) - Sender email
- `Subject` (string) - Message subject
- `Message` (string) - Message content
- `Status` (enum) - New, Read, Replied, Archived, Spam
- `Priority` (enum) - Low, Medium, High, Critical
- `IsRead` (bool) - Read status
- `IsReplied` (bool) - Reply status
- `Reply` (string?) - Admin reply

### Categories
Hierarchical categories for organization.

**Key Fields:**
- `Id` (Guid) - Primary key
- `Name` (string) - Category name
- `Slug` (string) - URL-friendly identifier (unique)
- `ParentCategoryId` (Guid?) - Self-referencing foreign key
- `DisplayOrder` (int) - Sort order
- `IsActive` (bool) - Visibility

**Relationships:**
- Self-referencing (Parent/Children)
- One-to-Many with Projects
- One-to-Many with BlogPosts

### Tags
Tag system with usage tracking.

**Key Fields:**
- `Id` (Guid) - Primary key
- `Name` (string) - Tag name
- `Slug` (string) - URL-friendly identifier (unique)
- `UsageCount` (int) - Number of times used
- `IsActive` (bool) - Visibility

**Relationships:**
- Many-to-Many with Projects (via ProjectTag)
- Many-to-Many with BlogPosts (via BlogPostTag)

## Indexes

Performance-critical indexes:
- Unique indexes on all Slug fields
- Index on Status fields
- Index on IsFeatured, IsPublished flags
- Index on CategoryId foreign keys
- Index on PublishedAt date fields
- Index on IsActive flags

## Soft Delete

All entities use soft delete via `IsDeleted` flag:
- Records are never physically deleted
- Global query filters automatically exclude deleted records
- Use `.IgnoreQueryFilters()` to query deleted records
- Track deletion with `DeletedAt` and `DeletedBy` fields
