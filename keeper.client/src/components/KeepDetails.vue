<template>
  <Modal id="active-keep">
    <template #modal-title>
      <h1>
        {{ active.name }}
        <i
          v-if="account.id == active.creatorId && route.name != 'Vaults'"
          title="delete keep"
          @click="deleteKeep(active.id)"
          class="mdi mdi-delete text-danger selectable"
        ></i>
      </h1>
    </template>
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6 p-0">
            <img :src="active.img" class="w-100 object-fit-cover" alt="" />
          </div>
          <div class="col-md-6">
            <!-- <div>
              <div class="d-flex justify-content-around">
                <h4 class="text-info">
                  <i class="mdi mdi-eye"></i> {{ active.views }}
                </h4>
                <h4 class="text-info">
                  <i class="mdi mdi-save"></i>{{ active.kept }}
                </h4>
                <h4 class="text-info">
                  <i class="mdi mdi-share"></i>{{ active.shares }}
                </h4>
              </div>

              <h1>{{ active.name }}</h1>
              <h1>{{ active.description }}</h1>

              <div class="row mt-5 align-items-end">
                <div class="col-10 d-flex justify-content-between">
                  <button
                    class="btn outline-color dropdown-toggle"
                    data-bs-toggle="dropdown"
                  >
                    Add to Vault
                  </button>
                  <ul class="dropdown-menu" aria-labelledby="dropdownMenu2">
                    <li v-for="p in profileVaults" :key="p.id">
                      <button
                        class="dropdown-item"
                        type="button"
                        @click="createVaultKeep(p)"
                      >
                        {{ p.name }}
                      </button>
                    </li>
                  </ul>
                  <img
                    :src="active.creator?.picture"
                    @click="goToProfile(active.creator.id)"
                    class="img-fluid cropped"
                    alt=""
                  />
                </div>
              </div>
            </div> -->
            <div class="row m-2">
              <div class="col-md-12 text-center">
                <div class="row">
                  <div class="col-4">
                    <h4 class="textcolor">
                      <i class="mdi mdi-eye"></i>{{ active.views }}
                    </h4>
                  </div>
                  <div class="col-4">
                    <h4 class="textcolor">
                      <i class="mdi mdi-content-save"></i>{{ active.kept }}
                    </h4>
                  </div>
                  <div class="col-4">
                    <h4 class="textcolor">
                      <i class="mdi mdi-share"></i>{{ active.shares }}
                    </h4>
                  </div>
                </div>
              </div>
              <div class="col-md-12 mt-2">
                <h1 class="mt-2">{{ active.name }}</h1>
                <h1 class="mt-2">{{ active.description }}</h1>
              </div>
              <div class="row justify-content-between align-items-bottom mt-5">
                <!-- <div class="row justify-content-between"> -->
                <div class="col-2" v-if="account.id && route.name != 'Vaults'">
                  <button
                    class="btn outline-color dropdown-toggle"
                    data-bs-toggle="dropdown"
                    title="Add Keep to Vault"
                  >
                    <h5 class="text-bold p-0 m-0">Add to Vault</h5>
                  </button>
                  <ul class="dropdown-menu" aria-labelledby="dropdownMenu2">
                    <li v-for="p in accountVaults" :key="p.id">
                      <button
                        class="dropdown-item"
                        type="button"
                        @click="createVaultKeep(p)"
                      >
                        <h5>{{ p.name }}</h5>
                      </button>
                    </li>
                  </ul>
                </div>
                <div
                  class="col-md-5 col-8"
                  v-if="
                    account.id == activeVault.creatorId &&
                    route.name == 'Vaults'
                  "
                >
                  <button
                    class="btn btn-danger text-bold"
                    title="delete saved keeped vaults"
                    @click="deleteVk()"
                  >
                    Delete Keeped Vault
                  </button>
                </div>

                <div class="col-2">
                  <img
                    :src="active.creator?.picture"
                    @click="goToProfile(active.creatorId)"
                    class="img-fluid cropped selectable"
                    :alt="active.name + 'profile picture'"
                    :title="active.name + 'profile picture'"
                  />
                </div>
                <!-- </div> -->
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
import { useRoute, useRouter } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { watchEffect } from "@vue/runtime-core"
import { profilesService } from "../services/ProfilesService"
import Pop from "../utils/Pop"
export default {
  // props: {
  //   vaultKeep: {
  //     type: Object,
  //     required: true,
  //   }
  // },

  setup() {
    const router = useRouter()
    const route = useRoute()
    return {
      route,
      active: computed(() => AppState.activeKeep),
      activeVault: computed(() => AppState.activeVault),
      async deleteKeep(id) {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
            await keepsService.deleteKeep(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()

        router.push({ name: 'Profile', params: { id } })
      },
      profileVaults: computed(() => AppState.profileVaults),
      async createVaultKeep(vk) {
        try {
          const createData = {
            keepId: AppState.activeKeep.id,
            vaultId: vk.id,
          }
          await vaultKeepsService.createVaultKeep(createData)
        } catch (error) {
          logger.error(error.message)
        }
      },
      async deleteVk() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
            await vaultKeepsService.deleteVk(AppState.activeVaultKeep.vaultKeepId)
          }

        } catch (error) {
          logger.error(error.message)

        }
      },
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      accountVaults: computed(() => AppState.accountVaults),
      activevk: computed(() => AppState.activeVaultKeep)
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

.outline-color {
  border: 3px solid rgba(85, 239, 196, 1);
}

.textcolor {
  color: rgba(72, 208, 170, 0.926);
}
</style>