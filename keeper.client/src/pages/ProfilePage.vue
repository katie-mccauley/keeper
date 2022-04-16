<template>
  <div class="container-fluid mt-2">
    <div class="row m-3 ms-0">
      <div class="col-1">
        <img :src="profile.picture" class="img-fluid cropped" alt="" />
      </div>
      <div class="col-5">
        <h1>{{ profile.name }}</h1>
        <h3>Keeps: {{ keeps.length }}</h3>
        <h3>Vaults: {{ vaults.length }}</h3>
      </div>
    </div>
    <h2>Keeps:</h2>
    <div class="masonry-with-columns">
      <div class="" v-for="k in keeps" :key="k.id">
        <!-- <img :src="k.img" class="rounded cropped img-fluid" alt="" /> -->
        <Keep :keepData="k" />
      </div>
    </div>
    <h2>Vaults:</h2>
    <div class="masonry-with-columns">
      <div v-for="v in vaults" :key="v.id">
        <div class="card selectable">
          <img :src="v.img" class="img-fluid" alt="" />
          <div class="card-img-overlay">
            <h1>{{ v.name }}</h1>
            <img :src="v.creator.picture" class="img-fluid cropped" alt="" />
          </div>
        </div>
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