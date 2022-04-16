<template>
  <div class="container-fluid mt-4">
    <div class="masonry-with-columns">
      <div class="space rounded shadow" v-for="vk in vaultkeeps" :key="vk.id">
        <div class="card selectable rounded shadow">
          <img :src="vk.img" class="img-fluid" alt="" />
          <div class="card-img-overlay">
            <h1>{{ vk.name }}</h1>
            <img :src="vk.creator?.picture" class="img-fluid cropped" alt="" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await vaultKeepsService.getVaultKeeps(route.params.id)
      } catch (error) {
        logger.error(error.message)
      }
    })
    return {
      vaultkeeps: computed(() => AppState.profileVaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}
.space {
  display: inline-block;
  margin: 0 0 0.5em;
  width: 100%;
}

.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}
</style>