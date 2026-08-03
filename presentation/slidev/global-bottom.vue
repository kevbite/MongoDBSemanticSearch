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

.mdb-footer.is-dark { color: #9fb8ad ; transition: color 1s ease; }
.mdb-footer.is-dark .mdb-wordmark { color: #00ed64; transition: color 1s ease; }
</style>
