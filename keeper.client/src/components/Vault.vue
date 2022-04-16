<template>
  <div @click="goToVault(vault.id)">
    <img :src="vault.img" class="img-fluid" alt="" />
    <div class="card-img-overlay">
      <h1>{{ vault.name }}</h1>
      <img :src="vault.creator.picture" class="img-fluid cropped" alt="" />
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router"
import { logger } from "../utils/Logger"
export default {
  props: {
    vault: {
      type: Object,
      required: false,
    }
  },
  setup() {
    const router = useRouter()
    return {
      goToVault(id) {
        router.push({ name: 'Vaults', params: { id } })
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