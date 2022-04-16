<template>
  <div
    class="card selectable"
    @click="setActive(keepData.id)"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
  >
    <img
      :src="keepData.img"
      @click="goToProfile(keepData.creator.id)"
      class="img-fluid"
      alt=""
    />
    <div class="card-img-overlay">
      <h1>{{ keepData.name }}</h1>
      <img :src="keepData.creator.picture" class="img-fluid cropped" alt="" />
    </div>
  </div>
  <KeepDetails />
</template>


<script>
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
export default {
  props: {
    keepData: {
      type: Object,
      required: false,
    }
  },
  setup(props) {
    return {
      // coverImg: computed(() => `url('${props.keepData.img}')`)
      async setActive(id) {
        try {
          // AppState.activeKeep = props.keepData
          await keepsService.getById(id);
        } catch (error) {
          logger.error(error)
        }
      },
      goToProfile(id) {
        router.push({ name: 'Profile', params: { id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}
</style>