---
name: Delete confirmation pattern
description: How delete confirmation works across admin views — inline forms + JS modal
---

## Pattern Used

Index pages use **inline POST forms** with JS modal intercept (no navigation to separate Delete page):

```html
<form asp-action="DeleteConfirmed" method="post" style="display:inline;"
      onsubmit="event.preventDefault();confirmDelete(this, @Html.Raw(titleJson));return false;">
  <input type="hidden" name="id" value="@item.Id" />
  <button type="submit" class="btn btn-danger btn-sm">Delete</button>
</form>
```

The `titleJson` variable is computed in the loop:
```csharp
var titleJson = System.Text.Json.JsonSerializer.Serialize(item.Title);
```

**Why:** Avoids separate page navigation; modal is cleaner UX. `JsonSerializer.Serialize()` safely escapes single quotes, double quotes, and special chars in JS context.

## Separate Delete Pages
`/Delete` pages are kept as HTML fallbacks (with our design system styling) for cases where JS is disabled. They redirect to Index on success.

## confirmDelete() function
Defined in `site.js` and `_AdminLayout.cshtml`. Accepts `(formEl, itemName)` where itemName is already a JS string (from JSON.Serialize).
