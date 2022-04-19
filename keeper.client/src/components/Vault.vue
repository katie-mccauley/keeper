<template>
  <div
    class="card selectable border border-3 rounded"
    @click="goToVault(vault)"
  >
    <img :src="vault.img" class="img-fluid" alt="" />
    <div
      class="card-img-overlay d-flex align-items-end backgroundfix pb-md-2 pb-0"
    >
      <div class="row justify-content-md-around box">
        <div class="col-md-10 col-8 d-flex align-items-end mb-2">
          <h2 class="text-light text-bold mb-2">{{ vault.name }}</h2>
        </div>
        <div class="col-md-2 col-1 p-1 d-flex align-items-center p-2">
          <img
            :src="vault.creator?.picture"
            class="img-fluid cropped m-md-2"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router"
import { AppState } from "../AppState"
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
      async goToVault(vault) {
        if (vault.isPrivate == true && vault.creatorId != AppState.account.id) {
          router.push({ name: 'Home' })
        } else {
          const id = vault.id
          router.push({ name: 'Vaults', params: { id } })
          // await vaultsService.getById(id)
        }

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

.card:hover {
  transform: scale(1.02);
  transition: all 0.15s linear;
  box-shadow: 0 3px 1px -3px rgba(0, 255, 0, 0.2),
    0 2px 3px 0 rgba(0, 255, 0, 0.14), 0 1px 5px 0 rgba(0, 255, 0, 0.12);
}

@media screen AND (max-width: 600px) {
  .box {
    inline-size: 150px;
  }
  .cropped {
    height: 40px;
    max-width: 40px;
    border-radius: 50%;
  }
}
</style>