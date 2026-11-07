(function () {
  'use strict';
  var requested = new URLSearchParams(window.location.search).get('lang');
  var lang = requested === 'en' || requested === 'de' ? requested : ((navigator.language || 'de').slice(0, 2) === 'de' ? 'de' : 'en');
  function apply(next) {
    document.documentElement.lang = next;
    document.title = location.pathname.endsWith('privacy.html') ? 'SafeSort Photos – ' + (next === 'de' ? 'Datenschutz' : 'Privacy') : 'SafeSort Photos – ' + (next === 'de' ? 'Fotos nach Datum sortieren' : 'Sort Photos by Date');
    if (location.pathname.endsWith('index.html') || location.pathname.endsWith('/')) {
      document.querySelector('meta[name="description"]').setAttribute('content', next === 'de'
        ? 'SafeSort Photos sortiert Fotos und Videos lokal nach Datum. Gratis analysieren, Vorschau prüfen und Originale beim Kopieren behalten.'
        : 'SafeSort Photos sorts photos and videos locally by date. Analyse for free, review the preview and keep originals untouched.');
    }
    document.querySelectorAll('a[href^="index.html"], a[href^="privacy.html"]').forEach(function (a) {
      var url = new URL(a.getAttribute('href'), location.href);
      url.searchParams.set('lang', next);
      a.setAttribute('href', url.pathname.split('/').pop() + url.search + url.hash);
    });
    document.querySelectorAll('[data-shot]').forEach(function (img) {
      img.src = 'img/' + next + '-' + img.dataset.shot + '.png';
      if (img.parentElement.classList.contains('shot-link')) img.parentElement.href = img.src;
      img.alt = next === 'de' ? 'SafeSort Photos: Analyse und geplante Zielordner' : 'SafeSort Photos: analysis and planned destination folders';
    });
    document.querySelector('.language').setAttribute('aria-label', next === 'de' ? 'Switch to English' : 'Zu Deutsch wechseln');
  }
  window.toggleLang = function () {
    lang = document.documentElement.lang === 'de' ? 'en' : 'de';
    var url = new URL(location.href);
    url.searchParams.set('lang', lang);
    history.replaceState(null, '', url);
    apply(lang);
  };
  apply(lang);
})();
