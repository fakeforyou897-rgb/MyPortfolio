/* =============================================
   PAGE LOADER
   ============================================= */
(function () {
  const html = document.documentElement;
  const saved = localStorage.getItem("theme");
  if (saved === "dark" || (!saved && window.matchMedia("(prefers-color-scheme: dark)").matches)) {
    html.classList.add("dark");
  } else {
    html.classList.remove("dark");
  }
})();

document.addEventListener("DOMContentLoaded", () => {
  /* ── Page Loader ── */
  const loader = document.getElementById("page-loader");
  if (loader) {
    window.addEventListener("load", () => {
      setTimeout(() => loader.classList.add("hidden"), 150);
    });
    // Fallback – remove after 2s
    setTimeout(() => loader.classList.add("hidden"), 2000);
  }

  /* ── Theme Toggle ── */
  const toggleBtn = document.getElementById("theme-toggle");
  const sunIcon   = document.getElementById("icon-sun");
  const moonIcon  = document.getElementById("icon-moon");

  function applyTheme(isDark) {
    if (isDark) {
      document.documentElement.classList.add("dark");
      localStorage.setItem("theme", "dark");
      if (sunIcon)  sunIcon.classList.remove("hidden");
      if (moonIcon) moonIcon.classList.add("hidden");
    } else {
      document.documentElement.classList.remove("dark");
      localStorage.setItem("theme", "light");
      if (sunIcon)  sunIcon.classList.add("hidden");
      if (moonIcon) moonIcon.classList.remove("hidden");
    }
  }

  // Init icons
  applyTheme(document.documentElement.classList.contains("dark"));

  if (toggleBtn) {
    toggleBtn.addEventListener("click", () => {
      applyTheme(!document.documentElement.classList.contains("dark"));
    });
  }

  /* ── Mobile Menu ── */
  const menuBtn    = document.getElementById("mobile-menu-btn");
  const mobileMenu = document.getElementById("mobile-menu");
  if (menuBtn && mobileMenu) {
    menuBtn.addEventListener("click", () => {
      const open = mobileMenu.classList.toggle("hidden");
    });
  }

  /* ── Navbar scroll shadow ── */
  const navbar = document.getElementById("main-navbar");
  if (navbar) {
    const onScroll = () => navbar.classList.toggle("scrolled", window.scrollY > 20);
    window.addEventListener("scroll", onScroll, { passive: true });
    onScroll();
  }

  /* ── Active nav link ── */
  document.querySelectorAll(".nav-link[data-href]").forEach(link => {
    const href = link.dataset.href;
    if (href && (window.location.pathname === href || window.location.pathname.startsWith(href + "/"))) {
      link.classList.add("active");
    }
  });

  /* ── Modal Confirm Delete ── */
  const modalOverlay = document.getElementById("modal-overlay");
  const modalConfirm = document.getElementById("modal-confirm");
  const modalCancel  = document.getElementById("modal-cancel");
  const modalDesc    = document.getElementById("modal-desc");
  let   pendingDeleteForm = null;

  window.confirmDelete = function (formEl, itemName) {
    pendingDeleteForm = formEl;
    if (modalDesc) modalDesc.textContent = `Are you sure you want to delete "${itemName || "this item"}"? This cannot be undone.`;
    if (modalOverlay) modalOverlay.classList.add("open");
  };

  if (modalCancel) {
    modalCancel.addEventListener("click", () => {
      pendingDeleteForm = null;
      modalOverlay.classList.remove("open");
    });
  }
  if (modalConfirm) {
    modalConfirm.addEventListener("click", () => {
      if (pendingDeleteForm) pendingDeleteForm.submit();
      modalOverlay.classList.remove("open");
    });
  }
  if (modalOverlay) {
    modalOverlay.addEventListener("click", (e) => {
      if (e.target === modalOverlay) {
        modalOverlay.classList.remove("open");
        pendingDeleteForm = null;
      }
    });
  }

  /* ── Auto-show server-side toasts ── */
  const pendingToasts = window.__pendingToasts;
  if (Array.isArray(pendingToasts)) {
    pendingToasts.forEach(t => window.showToast(t.type, t.title, t.msg));
  }
});

/* =============================================
   TOAST SYSTEM (global)
   ============================================= */
window.showToast = function (type, title, msg, duration) {
  duration = duration || 4000;
  const container = document.getElementById("toast-container");
  if (!container) return;

  const icons = {
    success: `<svg class="toast-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M5 13l4 4L19 7"/></svg>`,
    error:   `<svg class="toast-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/></svg>`,
    warning: `<svg class="toast-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M12 9v4m0 4h.01M10.29 3.86L1.82 18a2 2 0 001.71 3h16.94a2 2 0 001.71-3L13.71 3.86a2 2 0 00-3.42 0z"/></svg>`,
    info:    `<svg class="toast-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"/></svg>`,
  };

  const toast = document.createElement("div");
  toast.className = `toast toast-${type}`;
  toast.innerHTML = `
    ${icons[type] || icons.info}
    <div class="toast-body">
      ${title ? `<div class="toast-title">${title}</div>` : ""}
      ${msg    ? `<div class="toast-msg">${msg}</div>`   : ""}
    </div>
    <button class="toast-close" onclick="this.closest('.toast').remove()">×</button>
  `;
  container.appendChild(toast);

  setTimeout(() => {
    toast.classList.add("hiding");
    setTimeout(() => toast.remove(), 300);
  }, duration);
};
