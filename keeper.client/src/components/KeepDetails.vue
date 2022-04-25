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
    <template #modal-body class="">
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6 p-0">
            <img
              :src="active.img"
              class="w-100 object-fit-cover heightimg img-fluid"
              alt=""
            />
          </div>
          <div class="col-md-6 card">
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
            </div>
            <div
              class="row justify-content-between align-items-end mt-5 h-50 mb-3"
            >
              <div class="col-2" v-if="account.id && route.name != 'Vaults'">
                <button
                  class="btn outline-color dropdown-toggle"
                  data-bs-toggle="dropdown"
                  title="Add Keep to Vault"
                >
                  <h5 class="text-bold p-0 m-0">Add to Vault</h5>
                </button>
                <ul
                  class="dropdown-menu right-col"
                  aria-labelledby="dropdownMenu2"
                >
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
                  account.id == activeVault.creatorId && route.name == 'Vaults'
                "
              >
                <button
                  class="btn btn-danger text-bold"
                  title="delete saved keeped vaults"
                  @click="deleteVk(activevk)"
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
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
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
  props: {
    activevk: {
      tpye: Object,
      required: false
    }
  },
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
      async deleteVk(body) {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
            await vaultKeepsService.deleteVk(AppState.activeVaultKeep.vaultKeepId)
          }
          logger.log("vkid", body)
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

.heightcol {
  height: 400px;
}

.heightimg {
  height: 400px;
}

.right-col {
  max-height: 400px;
  overflow-y: auto;
}

.outline-color {
  border: 3px solid rgba(85, 239, 196, 1);
}

.textcolor {
  color: rgba(72, 208, 170, 0.926);
}
</style>