<template>
  <div
    class="card selectable border border-3 rounded"
    @click="setActive(keepData.id)"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
  >
    <img :src="keepData.img" class="img-fluid cover rounded" alt="" />
    <div
      class="card-img-overlay d-flex align-items-end backgroundfix pb-md-2 pb-0"
    >
      <div class="row justify-content-md-around box">
        <div class="col-md-10 col-8 d-flex align-items-end mb-2">
          <h2 class="text-light text-bold mb-0">
            {{ keepData.name }}
          </h2>
        </div>
        <div class="col-md-2 col-1 d-flex align-items-center p-2">
          <img
            :src="keepData.creator?.picture"
            class="img-fluid cropped"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute } from "vue-router"
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
    const route = useRoute()
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
      // goToProfile(id) {
      // router.push({ name: 'Profile', params: { id } })
      // }
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
.backgroundfix {
  background: linear-gradient(
    180.45deg,
    rgba(0, 0, 0, 0) 67.72%,
    rgba(0, 0, 0, 0.53) 99.61%
  );
}

.card:hover {
  transform: scale(1.02);
  transition: all 0.15s linear;
  box-shadow: 0 3px 1px -3px rgba(0, 255, 0, 0.2),
    0 2px 3px 0 rgba(0, 255, 0, 0.14), 0 1px 5px 0 rgba(0, 255, 0, 0.12);
}

@media screen AND (max-width: 600px) {
  .box {
    inline-size: 140px;
  }
}
</style>