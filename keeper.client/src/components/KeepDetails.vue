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
            <div>
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
              <img
                :src="active.creator?.picture"
                @click="goToProfile(active.creator.id)"
                class="img-fluid cropped"
                alt=""
              />
              <div class="d-flex justify-content-around"></div>
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
export default {
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