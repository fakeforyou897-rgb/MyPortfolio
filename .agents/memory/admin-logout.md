---
name: Admin logout form
description: Correct way to render logout in admin sidebar; AccountController does not exist
---

## Problem
The original admin layout used `asp-controller="Account" asp-action="Logout"` — no AccountController exists in any area.

## Fix
```html
<form asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="/Identity/Account/Login" method="post">
  <button type="submit">Sign Out</button>
</form>
```

**Why:** ASP.NET Identity uses Razor Pages in the Identity area, not an MVC controller. Always use `asp-area="Identity" asp-page="/Account/Logout"`.
