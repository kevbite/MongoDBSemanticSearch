# Semantic Search & MongoDB — Slidev deck

The lightning-talk deck, rebuilt with [Slidev](https://sli.dev/) so it can be
run locally in the browser — **no PowerPoint required**. It replaces the old
C# `DeckBuilder` (ShapeCrawler) that generated a `.pptx`.

The MongoDB branding (navy `#001E2B`, bright green `#00ED64`, dark green
`#00684A`), the speaker avatar and the two generated diagrams
(`vector-space.png`, `auto-embed.png`) are carried over from the original
template so the look-and-feel matches the branded slides.

## Run it locally (the local API / dev server)

```bash
cd presentation/slidev
npm install        # first time only
npm run dev        # starts Slidev's local server at http://localhost:3030
```

`npm run dev` boots Slidev's Vite-powered dev server (its local HTTP API) and
opens the deck in your browser. Useful extras:

| Command             | What it does                                                       |
| ------------------- | ----------------------------------------------------------------- |
| `npm run dev`       | Local server + auto-open, hot reload while editing `slides.md`     |
| `npm start`         | Local server on port 3030 without auto-open                        |
| `npm run serve`     | Local server exposed on the LAN (`--remote`) for a second machine  |
| `npm run build`     | Static build into `dist/` (host anywhere)                          |
| `npm run export`    | Export to PDF/PNG (requires `playwright-chromium`)                 |

Presenter view (speaker notes for every slide are included) is available at
`http://localhost:3030/presenter`.

## Structure

- `slides.md` — the whole deck (content + speaker notes as `<!-- ... -->`).
- `style.css` — MongoDB brand theme (auto-imported by Slidev).
- `global-bottom.vue` — persistent MongoDB leaf + footer on every slide.
- `public/` — images pulled from the original template
  (`avatar.png`, `vector-space.png`, `auto-embed.png`, brand blobs).
