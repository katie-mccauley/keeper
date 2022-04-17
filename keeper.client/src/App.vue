<template>
  <header>
    <Navbar />
  </header>
  <main class="colormain">
    <router-view />
  </main>
  <footer class="navcolor">
    <div class="text-dark text-bold text-center p-4">
      Made with 💖 by Katie McCauley
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.colormain {
  background-color: aliceblue;
}

.navcolor {
  background-color: #81ecec;
}
</style>
