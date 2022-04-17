<template>
  <div
    class="card selectable border border-3 rounded"
    @click="goToVault(vault.id)"
  >
    <img :src="vault.img" class="img-fluid" alt="" />
    <div class="card-img-overlay d-flex align-items-end backgroundfix">
      <div class="row">
        <div class="col-10">
          <h1 class="text-light">{{ vault.name }}</h1>
        </div>
        <div class="col-2">
          <img :src="vault.creator.picture" class="img-fluid cropped" alt="" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router"
import { vaultsService } from "../services/VaultsService"
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
      async goToVault(id) {
        router.push({ name: 'Vaults', params: { id } })
        await vaultsService.getById(id)
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

.backgroundfix {
  background: linear-gradient(
    180.45deg,
    rgba(0, 0, 0, 0) 67.72%,
    rgba(0, 0, 0, 0.53) 99.61%
  );
}
</style>