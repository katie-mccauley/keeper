<template>
  <div class="card selectable" @click="setActive">
    <img :src="keepData.img" class="img-fluid" alt="" />
    <div class="card-img-overlay">
      <h1>{{ keepData.name }}</h1>
      <img :src="keepData.creator.picture" class="img-fluid cropped" alt="" />
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState"
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
      setActive() {
        try {
          AppState.activeKeep = props.keepData
          logger.log("active", AppState.activeKeep)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cropped {
  height: 70px;
  max-width: 70px;
  border-radius: 50%;
}
</style>