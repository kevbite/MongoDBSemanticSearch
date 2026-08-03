<script setup lang="ts">
import { computed } from 'vue'
import { useSlideContext } from '@slidev/client'

const { $slidev, $page } = useSlideContext()

const darkLayouts = ['cover', 'section', 'statement', 'end', 'intro']

const isDark = computed(() => {
  const layout = $slidev.nav.currentSlideRoute?.meta?.slide?.frontmatter?.layout
  return darkLayouts.includes(layout)
})
</script>

<template>
  <footer class="mdb-footer" :class="{ 'is-dark': isDark }">
    <svg class="mdb-leaf" viewBox="0 0 32 32" aria-hidden="true">
      <path
        fill="#00ED64"
        d="M16 2c3.2 6.4 8 9.4 8 16.2 0 5-3.1 8.9-7.1 10.8L16 32l-.9-3C11.1 27.1 8 23.2 8 18.2 8 11.4 12.8 8.4 16 2Z"
      />
      <path
        fill="#00684A"
        d="M16 2c3.2 6.4 8 9.4 8 16.2 0 5-3.1 8.9-7.1 10.8L16 32V2Z"
        opacity="0.55"
      />
    </svg>
    <span class="mdb-wordmark">MongoDB</span>
    <span class="mdb-sep">·</span>
    <span class="mdb-talk">Semantic Search, Lightning Talk</span>
    <span class="mdb-sep">·</span>
    <span class="mdb-socials">
      <a href="https://bsky.app/profile/kevsoft.net" target="_blank" class="mdb-social-link" title="Bluesky">
        <svg fill="none" viewBox="0 0 64 57" aria-hidden="true">
          <path fill="currentColor" d="M13.873 3.805C21.21 9.332 29.103 20.537 32 26.55v15.882c0-.338-.13.044-.41.867-1.512 4.456-7.418 21.847-20.923 7.944-7.111-7.32-3.819-14.64 9.125-16.85-7.405 1.264-15.73-.825-18.014-9.015C1.12 23.022 0 8.51 0 6.55 0-3.268 8.579-.182 13.873 3.805ZM50.127 3.805C42.79 9.332 34.897 20.537 32 26.55v15.882c0-.338.13.044.41.867 1.512 4.456 7.418 21.847 20.923 7.944 7.111-7.32 3.819-14.64-9.125-16.85 7.405 1.264 15.73-.825 18.014-9.015C62.88 23.022 64 8.51 64 6.55c0-9.818-8.578-6.732-13.873-2.745Z"/>
        </svg>
        <span>@kevsoft.net</span>
      </a>
      <a href="https://x.com/kev_bite" target="_blank" class="mdb-social-link" title="X">
        <svg viewBox="0 0 24 24" aria-hidden="true">
          <path d="M18.244 2.25h3.308l-7.227 8.26 8.502 11.24H16.17l-4.714-6.231-5.401 6.231H2.748l7.73-8.835L2.16 2.25h6.961l4.263 5.633 5.86-5.633Zm-1.161 17.52h1.833L7.084 4.126H5.117Z"/>
        </svg>
        <span>@kev_bite</span>
      </a>
    </span>
    <span class="mdb-page">{{ $page }} / {{ $slidev.nav.total }}</span>
  </footer>
</template>

<style scoped>
.mdb-footer {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1.4rem;
  font-size: 0.72rem;
  color: #4a6a5f;
  opacity: 0.9;
  pointer-events: none;
  z-index: 20;
  transition: color 1s ease;
}
.mdb-leaf { width: 1.1rem; height: 1.1rem; }
.mdb-wordmark { font-weight: 700; color: #023430; letter-spacing: 0.01em; transition: color 1s ease; }
.mdb-sep { opacity: 0.5; }
.mdb-talk { font-weight: 500; }
.mdb-page { margin-left: auto; font-variant-numeric: tabular-nums; }
.mdb-socials { display: flex; align-items: center; gap: 0.75rem; pointer-events: all; }
.mdb-social-link {
  display: flex;
  align-items: center;
  gap: 0.25rem;
  color: inherit;
  opacity: 0.6;
  transition: opacity 0.2s, color 1s ease;
  text-decoration: none;
}
.mdb-social-link:hover { opacity: 1; }
.mdb-social-link svg { width: 0.85rem; height: 0.85rem; fill: currentColor; flex-shrink: 0; }

.mdb-footer.is-dark { color: #9fb8ad ; transition: color 1s ease; }
.mdb-footer.is-dark .mdb-wordmark { color: #00ed64; transition: color 1s ease; }
</style>
