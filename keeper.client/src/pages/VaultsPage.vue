<template>
  <div class="container-fluid mt-4">
    <div
      class="
        row
        justify-content-md-between justify-content-around
        mb-5
        textcolor
      "
    >
      <div class="col-md-5 col-7">
        <h1>{{ activeVault.name }}</h1>
        <h3>Keeps: {{ vaultkeeps.length }}</h3>
      </div>
      <div v-if="activeVault.creatorId == account.id" class="col-md-2 col-3">
        <button class="btn btn-danger" @click="deleteVault()">
          <h6 class="text-bold p-0 m-0">Delete Vault</h6>
        </button>
      </div>
    </div>
    <div class="masonry-with-columns">
      <div class="space rounded shadow" v-for="vk in vaultkeeps" :key="vk.id">
        <!-- <div
          class="card selectable rounded shadow"
          data-bs-toggle="modal"
          data-bs-target="#active-keep"
          @click="setActive(vk.id)"
        >
          <img :src="vk.img" class="img-fluid" alt="" />
          <div class="card-img-overlay">
            <h1>{{ vk.name }}</h1>
            <img :src="vk.creator?.picture" class="img-fluid cropped" alt="" />
          </div>
        </div> -->
        <div @click="setActiveVk(vk)">
          <Keep :keepData="vk" />
        </div>
      </div>
    </div>
  </div>
  <!-- <Modal id="vaultkeep-details">
    <template #modal-title>VaultKeep Details</template>
    <template #modal-body>
      <div class="col-md-6 p-0">
        <img :src="activeKeep.img" class="w-100 object-fit-cover" alt="" />
      </div>
    </template>
  </Modal> -->
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { useRoute, useRouter } from "vue-router"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    watchEffect(async () => {
      try {
        if (route.name == 'Vaults') {
          await vaultKeepsService.getVaultKeeps(route.params.id)
          await vaultsService.getById(route.params.id)
        }


      } catch (error) {
        logger.error(error.message)
        router.push({ name: 'Home' })
      }
    })
    return {
      vaultkeeps: computed(() => AppState.profileVaultKeeps),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            router.push({ name: 'Home' })
            await vaultsService.deleteVault(route.params.id)
          }

        } catch (error) {
          logger.error(error)
        }
      },
      async setActive(id) {
        try {
          await keepsService.getById(id)
        } catch (error) {
          logger.error(error)

        }
      },
      activeKeep: computed(() => AppState.activeKeep),
      setActiveVk(vk) {
        AppState.activeVaultKeep = {}
        AppState.activeVaultKeep = vk
        // logger.log("vvkvkvkvk", AppState.activeVaultKeep)

      },
      activeVault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
}
.space {
  display: inline-block;
  margin: 0 0 0.5em;
  width: 100%;
}

.textcolor {
  font-family: "Inter";
  font-style: normal;
  color: #000000;
}

.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}
</style>