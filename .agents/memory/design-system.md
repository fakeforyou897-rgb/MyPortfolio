---
name: Design system and CSS architecture
description: How CSS is structured across public, admin, and Identity areas; jQuery validation CDN pattern
---

## CSS Architecture

**Public views** (`_Layout.cshtml`) — only loads `site.css`. No Tailwind, no Bootstrap.
- All public views use custom CSS classes: `.btn`, `.card`, `.badge`, `.form-control`, `.section`, `.container`, etc.

**Admin views** (`_AdminLayout.cshtml`) — loads `site.css` PLUS Tailwind CDN.
- Admin views use Tailwind utility classes extensively (text-*, grid-*, etc.)
- Tailwind CDN configured with `darkMode: 'class'`
- Theme init script inline in head to prevent flash

**Identity views** — use `_Layout.cshtml` (public layout, site.css only).
- All Identity pages (Login, Register, ForgotPassword, etc.) have been rewritten to use our CSS classes

**Why:** Separating Tailwind to admin-only keeps public pages lean; Identity pages needed full rewrite from Bootstrap/Tailwind.

## jQuery Validation

There is NO `wwwroot/lib/` folder. Both `_ValidationScriptsPartial.cshtml` files (public + Identity) use CDN:
- jQuery 3.7.1 from cdnjs
- jquery-validate 1.19.5 from cdnjs  
- jquery-validation-unobtrusive 4.0.0 from cdnjs

**Never** reference `~/lib/jquery-validation/...` — those files don't exist.
