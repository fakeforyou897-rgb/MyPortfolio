# API Reference

## Public Endpoints

### Home
```
GET /
```
Returns the homepage.

### Projects

**List Projects**
```
GET /Projects
```
Query Parameters:
- `pageNumber` (int) - Page number (default: 1)
- `pageSize` (int) - Items per page (default: 10, max: 100)
- `searchTerm` (string) - Search in title and description
- `categoryId` (Guid) - Filter by category
- `status` (enum) - Filter by project status
- `isFeatured` (bool) - Filter featured projects
- `sortBy` (string) - Sort field (Title, CreatedAt, DisplayOrder)
- `sortOrder` (string) - "asc" or "desc"

**Project Details**
```
GET /Projects/{slug}
```
Returns project details by slug. Increments view count.

### Blog

**List Blog Posts**
```
GET /Blog
```
Query Parameters (same as Projects):
- Standard pagination and search parameters
- `categoryId`, `author`, `status`, `isFeatured`

**Blog Post Details**
```
GET /Blog/{slug}
```
Returns blog post details by slug. Increments view count.

### Contact

**Contact Form**
```
GET /Contact
```
Returns the contact form.

**Submit Message**
```
POST /Contact
Body: ContactMessageViewModel
{
  "name": "string",
  "email": "string",
  "phone": "string?",
  "subject": "string",
  "message": "string"
}
```

## Admin Endpoints

All admin endpoints require authentication with Admin role.

### Dashboard
```
GET /Admin
```
Returns dashboard with statistics and recent activity.

### Projects Management

**List Projects**
```
GET /Admin/Projects
```

**Create Project**
```
GET /Admin/Projects/Create
POST /Admin/Projects/Create
Body: Project entity
```

**Edit Project**
```
GET /Admin/Projects/Edit/{id}
POST /Admin/Projects/Edit/{id}
Body: Project entity
```

**Delete Project**
```
GET /Admin/Projects/Delete/{id}
POST /Admin/Projects/DeleteConfirmed
```

### Blog Management

Similar CRUD operations for BlogPosts:
- `/Admin/BlogPosts`
- `/Admin/BlogPosts/Create`
- `/Admin/BlogPosts/Edit/{id}`
- `/Admin/BlogPosts/Delete/{id}`

### Contact Messages

**List Messages**
```
GET /Admin/ContactMessages
```

**View Details**
```
GET /Admin/ContactMessages/Details/{id}
```

**Delete Message**
```
POST /Admin/ContactMessages/Delete/{id}
```

### User Management

**List Users**
```
GET /Admin/Users
```

**Edit User Roles**
```
GET /Admin/Users/EditRoles/{id}
POST /Admin/Users/EditRoles
Body: EditRolesViewModel
```

## Response Formats

### Paginated Response
```json
{
  "items": [],
  "totalCount": 0,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 0,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### Error Response
Standard ASP.NET Core ModelState validation errors.
