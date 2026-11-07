# SafeSort Photos – statische Website 

Landingpage und Datenschutz auf Deutsch/Englisch. Lokale Dateien, keine externen Fonts, kein Tracking, keine Cookies und kein Browser-Speicher. Die Sprache steht bei Auswahl in der URL (`?lang=de` / `?lang=en`) und bleibt über interne Seitenlinks erhalten.

**Nicht vAusgeliefert über GitHub Pages unter `https://minh.systems/SafeSort-Photos/` (Custom Domain der Nutzer-Seite `pinkysworld.github.io`). Nur relative Pfade, kein Build; `.nojekyll` verhindert Jekyll-Verarbeitung. Kanonische und OG-URLs zeigen auf `https://minh.systems/SafeSort-Photos/`.

Lokal ansehen:

```sh
python3 -m http.server 8765 --directory website
# http://localhost:8765/?lang=de
```

## Veröffentlichen

1. Den **Inhalt** von `website/` (inklusive `.nojekyll`, `img/`, `evidence/`) in das Root des Repositorys `pinkysworld/SafeSort-Photos`, Branch `main`, kopieren und pushen.
2. Im Repository unter *Settings → Pages* als Quelle „Deploy from a branch“, Branch `main`, Ordner `/ (root)` wählen. Keine eigene CNAME-Datei nötig: die Domain `minh.systems` gehört zur Nutzer-Seite.
3. Danach `https://minh.systems/SafeSort-Photos/` und `…/privacy.html` in DE/EN prüfen.

## Offen vor Livebetrieb

- Store-Button bleibt Platzhalter („Bald im Microsoft Store“) ohne Link, bis die echte Store-ID existiert.
- 9,99 € / €9.99 mit Kaufkraftpreisen ist entschieden, im Partner Center aber noch nicht bestätigt.
- Betreiber- und Kontaktangaben stehen ausschließlich im zentralen Impressum `https://minh.systems/impressum/` (Anbieter Minh Studios); Datenschutz analog Pathrova (GitHub Pages, keine Cookies/Tracker).
- App-Abbildungen sind macOS-Demoaufnahmen (Avalonia), keine native Windows-Aufnahme.



`img/de-preview.png` und `img/en-preview.png`: Kopien von `store/screenshots/{de,en}/light/02-analysis-preview.png` (aktuelle Navigation „Vorbereiten / Analyse & Vorschau / Kopieren & Ergebnis“). `img/*-detail.png`: Ausschnitt daraus. Quelle und QA-Belege siehe Projektbericht. Keine fremden Fotos oder persönlichen Pfade verwenden.

## Durchgeführte lokale QA (02.11.2026)

Headless Google Chrome über CDP: Landingpage und Datenschutz, je DE/EN, bei 375 und 1440 px (acht Ansichten). In allen Fällen entspricht `document.documentElement.scrollWidth` der Viewportbreite; App-Bilder sind geladen und interne Fragmentziele vorhanden. JS-Syntaxcheck (`node --check website/lang.js`) bestanden. Aufnahmen von mobilen Einstiegen/Seitenenden sowie Desktop-Übersicht visuell geprüft: keine abgeschnittenen Inhalte; App-Aufnahme lässt sich für Details in voller Größe öffnen.

Temporäre Belege: `/tmp/fotokehr-web-qa/results.json` und `/tmp/fotokehr-web-qa/{index,privacy}.html-{de,en}-{375,1440}.png`. Diese QA prüft lokale Darstellung, keine Veröffentlichung und keine externen Rechtstexte oder Store-Ziele.

## Konkreter Beispiellauf (Review 4, Maßnahme 10)

Die zusätzlichen Belegkarten sind eine lesbare Erläuterung eines tatsächlich ausgeführten, isolierten Core-Laufs, keine nachgebaute App-Oberfläche. Eigene Illustrationen aus `src/SafeSort Photos.App/Assets/Demo/`: drei JPG-Quellen, darunter zwei bytegleiche Dateien mit unterschiedlichen Namen; Datumsquelle Dateiname, Kopierplan mit `SkipDuplicates: true`. Ergebnis: zwei erstellte Kopien, eine ausgelassene Dublette, keine fehlgeschlagene Aktion. Der Generator prüft bytegleichen Erhalt aller Quellen und Bytegleichheit beider Zielkopien.

Reproduzieren (benötigt .NET 10; ausschließlich temporäre synthetische Quelldateien):

```sh
bash website/evidence/generate.sh
```

`evidence/run.json` enthält die tatsächlich ermittelten Status, Pfade und SHA-256-Werte. `evidence/report.csv` stammt aus dem wirklichen Laufbericht; nur der zufällige temporäre Demowurzelpfad wurde durch `Demo` ersetzt. Die Bilddateien sind dieselben verwendeten Demoillustrationen. Bei einer Wiederholung werden diese vier abgeleiteten Belegdateien aktualisiert. Die Kartenwerte müssen bei verändertem Ergebnis erneut abgeglichen werden. Das Beispiel belegt weder reale Kameraformate noch native Windows-Ausführung oder allgemeine Absturzsicherheit.

Lokale Nachprüfung am 02.11.2026: Headless Chrome/CDP, Landingpage und Datenschutz jeweils DE/EN bei 375 und 1440 px; acht Ansichten ohne horizontalen Überlauf, sämtliche Bilder geladen, lokale Fragmentziele vorhanden. Mobil wurden Vorschau und erhaltener Detailausschnitt sowie neue Belegkarten auf Deutsch und Englisch visuell geprüft. JS-Syntaxprüfung bestanden. Der Playwright-Skill-Wrapper scheiterte an einem fehlenden `playwright-cli`; deshalb wurde die bereits vorhandene lokale CDP-Prüfung verwendet. Temporäre Belege: `/tmp/fotokehr-web-review5/`. Keine Veröffentlichung, externe Links und Rechtstexte nicht freigegeben.

## Lokale Nachprüfung Runde 6 (03.11.2026)

Die App-Bilder wurden aus der aktuellen lokalen macOS-Demotour ersetzt. Landingpage DE/EN wurde bei 375 px gerendert und visuell geprüft: kein horizontaler Überlauf, alle Bilder nach explizitem Laden dekodiert, Preis je Sprache korrekt. EN-Desktop wurde ebenfalls angesehen; Datenschutz EN bei 375 px ohne horizontalen Überlauf geprüft. Diese Ergebnisse ersetzen weder Windows-Abnahme noch die oben genannten Angaben für einen Livebetrieb.
