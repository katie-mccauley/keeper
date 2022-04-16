<template>
  <div class="container-fluid mt-2">
    <div class="masonry-with-columns">
      <div class="" v-for="k in keeps" :key="k.id">
        <!-- <img :src="k.img" class="rounded cropped img-fluid" alt="" /> -->
        <Keep :keepData="k" />
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
      keeps: computed(() => AppState.keeps),
      // coverImg: computed(() => `url('${AppState.keeps.img}')`)
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

// .cover-img {
//   background-image: v-bind(coverImg);
//   background-size: cover;
//   background-position: center;
// }
</style>
