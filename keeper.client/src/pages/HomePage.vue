<template>
  <div class="container-fluid mt-5 textcolor">
    <div class="masonry-with-columns border-cropped">
      <div class="space" v-for="k in keeps" :key="k.id">
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
import { vaultsService } from "../services/VaultsService"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
        // await vaultsService.getAccountVaults()
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
  column-gap: 0.8rem;
}

.space {
  display: inline-block;
  margin: 0 0 0.8em;
  width: 100%;
}

.border-cropped {
  border-radius: 10px;
}
.cropped {
  height: 300px;
  max-width: 250px;
}
.textcolor {
  font-family: "Inter";
  font-style: normal;
  color: #000000;
}

.backgroundfix {
  background: linear-gradient(
    180.45deg,
    rgba(0, 0, 0, 0) 67.72%,
    rgba(0, 0, 0, 0.53) 99.61%
  );
}

// .cover-img {
//   background-image: v-bind(coverImg);
//   background-size: cover;
//   background-position: center;
// }

@media screen AND (max-width: 600px) {
  .masonry-with-columns {
    columns: 2 100px;
    column-gap: 0.8rem;
  }
}
</style>
