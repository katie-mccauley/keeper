<template>
  <div class="container-fluid">
    <div class="masonry-with-columns">
      <div class="space" v-for="k in keeps" :key="k.id">
        <img :src="k.img" class="rounded cropped img-fluid" alt="" />
        {{ k.name }}
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
  height: 300px;
  max-width: 250px;
}
</style>
