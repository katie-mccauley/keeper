<template>
  <div class="container-fluid mt-2">
    <img :src="profile.picture" class="img-fluid cropped" alt="" />
    <h1>{{ profile.name }}</h1>
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
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { profilesService } from "../services/ProfilesService"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getUser(route.params.id)
        await profilesService.getUserKeeps(route.params.id)
        await profilesService.getUserVaults(route.params.id)
      } catch (error) {
        logger.log(error.message)
      }
    })
    return {
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}

.cropped {
  height: 70px;
  max-width: 70px;
  border-radius: 50%;
}
</style>