/* =============================================
   THEME INIT (also in <head> to prevent flash)
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

/* =============================================
   TRANSLATIONS
   ============================================= */
const I18N = {
  en: {
    "nav.home": "Home", "nav.projects": "Projects", "nav.blog": "Blog",
    "nav.contact": "Contact", "nav.admin": "Admin",
    "footer.tagline": "Building modern, scalable web applications with passion and precision.",
    "footer.nav": "Navigation", "footer.getInTouch": "Get in Touch",
    "footer.location": "Available Worldwide",
    "footer.rights": "All rights reserved.", "footer.builtWith": "Built with ASP.NET Core 9",
    "modal.deleteTitle": "Delete Confirmation",
    "modal.deleteDesc": "Are you sure? This cannot be undone.",
    "modal.cancel": "Cancel", "modal.confirm": "Yes, Delete",
    "home.available": "Available for work",
    "home.heroSub": "Full Stack Developer crafting modern web apps with ASP.NET Core, Tailwind CSS, and JavaScript.",
    "home.viewWork": "View My Work", "home.getInTouch": "Get in Touch",
    "home.yearsExp": "Years Experience", "home.projectsCount": "Projects Completed",
    "home.clients": "Happy Clients", "home.rating": "Average Rating",
    "home.aboutBadge": "About Me", "home.aboutTitle": "A developer who loves clean code",
    "home.seeProjects": "See My Projects →",
    "home.portfolioBadge": "Portfolio", "home.featuredTitle": "Featured Projects",
    "home.featuredSub": "Highlights from my recent work",
    "home.viewAll": "View All Projects",
    "home.ctaTitle": "Let's Work Together",
    "home.ctaSub": "Have a project in mind or want to collaborate? Get in touch and let's make it happen.",
    "home.ctaBtn": "Contact Me →",
    "home.skill.backend": "Backend", "home.skill.frontend": "Frontend",
    "home.skill.devops": "DevOps", "home.skill.db": "Databases",
    "blog.badge": "Writing", "blog.title": "Blog",
    "blog.sub": "Thoughts, tutorials, and deep dives on software development.",
    "blog.readMore": "Read More",
    "projects.badge": "Portfolio", "projects.title": "My Projects",
    "projects.sub": "A collection of things I've built — from side projects to client work.",
    "contact.badge": "Get In Touch", "contact.title": "Contact Me",
    "contact.sub": "Have a project or idea? I'd love to hear from you.",
    "contact.connect": "Let's connect", "contact.send": "Send Message →",
    "contact.email": "Email", "contact.location": "Location",
    "contact.responseTime": "Response Time", "contact.responseVal": "Usually within 24 hours",
    "contact.locationVal": "Available Worldwide (Remote)",
  },
  ar: {
    "nav.home": "الرئيسية", "nav.projects": "المشاريع", "nav.blog": "المدونة",
    "nav.contact": "تواصل", "nav.admin": "الإدارة",
    "footer.tagline": "بناء تطبيقات ويب حديثة وقابلة للتطوير بشغف ودقة.",
    "footer.nav": "التصفح", "footer.getInTouch": "تواصل معنا",
    "footer.location": "متاح في جميع أنحاء العالم",
    "footer.rights": "جميع الحقوق محفوظة.", "footer.builtWith": "مبني بـ ASP.NET Core 9",
    "modal.deleteTitle": "تأكيد الحذف",
    "modal.deleteDesc": "هل أنت متأكد؟ لا يمكن التراجع عن هذا.",
    "modal.cancel": "إلغاء", "modal.confirm": "نعم، احذف",
    "home.available": "متاح للعمل",
    "home.heroSub": "مطور ويب متكامل يبني تطبيقات حديثة باستخدام ASP.NET Core وTailwind CSS وJavaScript.",
    "home.viewWork": "عرض أعمالي", "home.getInTouch": "تواصل معي",
    "home.yearsExp": "سنوات خبرة", "home.projectsCount": "مشروع منجز",
    "home.clients": "عميل سعيد", "home.rating": "متوسط التقييم",
    "home.aboutBadge": "عني", "home.aboutTitle": "مطور يحب الكود النظيف",
    "home.seeProjects": "استعرض مشاريعي ←",
    "home.portfolioBadge": "المحفظة", "home.featuredTitle": "المشاريع المميزة",
    "home.featuredSub": "أبرز أعمالي الأخيرة",
    "home.viewAll": "عرض جميع المشاريع",
    "home.ctaTitle": "لنعمل معاً",
    "home.ctaSub": "هل لديك مشروع في ذهنك أو تريد التعاون؟ تواصل معي ولنجعله حقيقة.",
    "home.ctaBtn": "تواصل معي ←",
    "home.skill.backend": "الخلفية", "home.skill.frontend": "الواجهة",
    "home.skill.devops": "ديف أوبس", "home.skill.db": "قواعد البيانات",
    "blog.badge": "كتابة", "blog.title": "المدونة",
    "blog.sub": "أفكار ودروس تعليمية وتعمق في عالم تطوير البرمجيات.",
    "blog.readMore": "اقرأ المزيد",
    "projects.badge": "المحفظة", "projects.title": "مشاريعي",
    "projects.sub": "مجموعة أعمال بنيتها — من المشاريع الجانبية إلى أعمال العملاء.",
    "contact.badge": "تواصل", "contact.title": "اتصل بي",
    "contact.sub": "هل لديك مشروع أو فكرة؟ يسعدني سماعك.",
    "contact.connect": "لنتواصل", "contact.send": "إرسال الرسالة",
    "contact.email": "البريد الإلكتروني", "contact.location": "الموقع",
    "contact.responseTime": "وقت الرد", "contact.responseVal": "عادةً خلال 24 ساعة",
    "contact.locationVal": "متاح عالمياً (عن بعد)",
  }
};

function applyLang(lang) {
  const html = document.documentElement;
  const dict = I18N[lang] || I18N.en;
  const isAr = lang === "ar";
  html.setAttribute("lang", lang);
  html.setAttribute("dir", isAr ? "rtl" : "ltr");
  localStorage.setItem("lang", lang);

  document.querySelectorAll("[data-i18n]").forEach(el => {
    const key = el.dataset.i18n;
    if (dict[key] !== undefined) el.textContent = dict[key];
  });

  const label = document.getElementById("lang-label");
  if (label) label.textContent = isAr ? "EN" : "AR";
}

/* =============================================
   DOM READY
   ============================================= */
document.addEventListener("DOMContentLoaded", () => {

  /* ── Page Loader ── */
  const loader = document.getElementById("page-loader");
  if (loader) {
    window.addEventListener("load", () => setTimeout(() => loader.classList.add("hidden"), 120));
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
      sunIcon  && sunIcon.classList.remove("hidden");
      moonIcon && moonIcon.classList.add("hidden");
    } else {
      document.documentElement.classList.remove("dark");
      localStorage.setItem("theme", "light");
      sunIcon  && sunIcon.classList.add("hidden");
      moonIcon && moonIcon.classList.remove("hidden");
    }
  }
  applyTheme(document.documentElement.classList.contains("dark"));
  toggleBtn && toggleBtn.addEventListener("click", () =>
    applyTheme(!document.documentElement.classList.contains("dark"))
  );

  /* ── Language Toggle ── */
  const langBtn = document.getElementById("lang-toggle");
  const savedLang = localStorage.getItem("lang") || "en";
  applyLang(savedLang);
  langBtn && langBtn.addEventListener("click", () => {
    const current = localStorage.getItem("lang") || "en";
    applyLang(current === "en" ? "ar" : "en");
  });

  /* ── Mobile Menu ── */
  const menuBtn    = document.getElementById("mobile-menu-btn");
  const mobileMenu = document.getElementById("mobile-menu");
  menuBtn && mobileMenu && menuBtn.addEventListener("click", () =>
    mobileMenu.classList.toggle("hidden")
  );

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
    const path = window.location.pathname;
    if (href === "/" ? path === "/" : path.startsWith(href)) {
      link.classList.add("active");
    }
  });

  /* ── Modal Confirm Delete ── */
  const modalOverlay = document.getElementById("modal-overlay");
  const modalConfirm = document.getElementById("modal-confirm");
  const modalCancel  = document.getElementById("modal-cancel");
  const modalDesc    = document.getElementById("modal-desc");
  let pendingDeleteForm = null;

  window.confirmDelete = function (formEl, itemName) {
    pendingDeleteForm = formEl;
    const lang = localStorage.getItem("lang") || "en";
    const dict = I18N[lang] || I18N.en;
    if (modalDesc) {
      const name = itemName ? `"${itemName}"` : (lang === "ar" ? "هذا العنصر" : "this item");
      modalDesc.textContent = lang === "ar"
        ? `هل أنت متأكد من حذف ${name}؟ لا يمكن التراجع.`
        : `Are you sure you want to delete ${name}? This cannot be undone.`;
    }
    modalOverlay && modalOverlay.classList.add("open");
  };

  modalCancel && modalCancel.addEventListener("click", () => {
    pendingDeleteForm = null;
    modalOverlay.classList.remove("open");
  });
  modalConfirm && modalConfirm.addEventListener("click", () => {
    if (pendingDeleteForm) pendingDeleteForm.submit();
    modalOverlay.classList.remove("open");
  });
  modalOverlay && modalOverlay.addEventListener("click", e => {
    if (e.target === modalOverlay) { modalOverlay.classList.remove("open"); pendingDeleteForm = null; }
  });

  /* ── Server-side toast ── */
  const pendingToasts = window.__pendingToasts;
  Array.isArray(pendingToasts) && pendingToasts.forEach(t => window.showToast(t.type, t.title, t.msg));
});

/* =============================================
   TOAST (global)
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
  toast.innerHTML = `${icons[type] || icons.info}<div class="toast-body">${title ? `<div class="toast-title">${title}</div>` : ""}${msg ? `<div class="toast-msg">${msg}</div>` : ""}</div><button class="toast-close" onclick="this.closest('.toast').remove()">×</button>`;
  container.appendChild(toast);
  setTimeout(() => { toast.classList.add("hiding"); setTimeout(() => toast.remove(), 300); }, duration);
};
