<template>
  <div
    class="card selectable border border-3 rounded"
    @click="setActive(keepData.id)"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
  >
    <img
      :src="keepData.img"
      @click="goToProfile(keepData.creator.id)"
      class="img-fluid cover"
      alt=""
    />
    <div class="card-img-overlay d-flex align-items-end">
      <div class="row justify-content-around">
        <div class="col-10">
          <h1 class="text-light">{{ keepData.name }}</h1>
        </div>
        <div class="col-2 d-flex align-items-center">
          <img
            :src="keepData.creator.picture"
            class="img-fluid cropped"
            alt=""
          />
        </div>
      </div>
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
  height: 40px;
  max-width: 40px;
  border-radius: 50%;
}
.cover {
  object-fit: cover;
}
.border-picked {
  border-radius: 10px;
}
</style>