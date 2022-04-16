<template>
  <form
    class="
      bg-grey
      darken-20
      justify-content-center
      elevation-3
      shadow
      col-8
      p-2
      m-5
    "
    @submit.prevent="createVault"
  >
    <h2>Create Project</h2>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Name: </label>
      <input
        v-model="editable.name"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Name....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Description: </label>
      <input
        v-model="editable.description"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Description....."
      />
    </div>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Image: </label>
      <input
        v-model="editable.img"
        required
        type="text"
        class="form-control"
        aria-describedby="helpId"
        placeholder="Cover Image....."
      />
    </div>

    <div class="col-2 mb-2">
      <label for="" class="form-label">Private?</label>
      <input
        v-model="editable.isPrivate"
        type="checkbox"
        class=""
        aria-describedby="helpId"
      />
    </div>

    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { useRoute } from "vue-router"
import { Modal } from "bootstrap"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      async createVault() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-vault')).hide()
          await vaultsService.createVault(editable.value);
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>