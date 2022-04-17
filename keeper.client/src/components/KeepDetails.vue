<template>
  <Modal id="active-keep">
    <template #modal-title>
      <h1>
        KeepDetails
        <i
          @click="deleteKeep(active.id)"
          class="mdi mdi-delete text-danger"
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
            <div class="row">
              <div class="col-md-12 text-center">
                <div class="row">
                  <div class="col-md-4">
                    <h4 class="text-info">
                      <i class="mdi mdi-eye"></i> {{ active.views }}
                    </h4>
                  </div>
                  <div class="col-md-4">
                    <h4 class="text-info">
                      <i class="mdi mdi-save"></i>{{ active.kept }}
                    </h4>
                  </div>
                  <div class="col-md-4">
                    <h4 class="text-info">
                      <i class="mdi mdi-share"></i>{{ active.shares }}
                    </h4>
                  </div>
                </div>
              </div>
              <div class="col-md-12 mt-2">
                <h1 class="mt-2">{{ active.name }}</h1>
                <h1 class="mt-2">{{ active.description }}</h1>
              </div>
              <div class="row justify-content-between align-items-bottom">
                <!-- <div class="row justify-content-between"> -->
                <div class="col-2">
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
                </div>
                <div class="col-2">
                  <h3 class="text-danger selectable">
                    <i class="mdi mdi-delete" @click="deleteVk()"></i>
                  </h3>
                </div>

                <div class="col-2">
                  <img
                    :src="active.creator?.picture"
                    @click="goToProfile(active.creator.id)"
                    class="img-fluid cropped"
                    alt=""
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
import { useRouter } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  // props: {
  //   vaultKeep: {
  //     type: Object,
  //     required: true,
  //   }
  // },
  setup() {
    const router = useRouter()
    return {
      active: computed(() => AppState.activeKeep),
      async deleteKeep(id) {
        try {
          Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
          await keepsService.deleteKeep(id)
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
          Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
          await vaultKeepsService.deleteVk(AppState.activeVaultKeep.vaultKeepId)
        } catch (error) {
          logger.error(error.message)

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

.outline-color {
  border: 2px solid rgba(85, 239, 196, 1);
}
</style>