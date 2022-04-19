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
    @submit.prevent="createKeep"
  >
    <h2>Create Keep</h2>
    <div class="col-md-4 mb-2">
      <label for="" class="form-label">Name: </label>
      <input
        v-model="editable.name"
        required
        maxlength="10"
        minlength="3"
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
        maxlength="50"
        minlength="3"
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

    <div class="col-12 d-flex justify-content-end">
      <button class="btn btn-primary">create</button>
    </div>
  </form>
</template>


<script>
import { Modal } from "bootstrap"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { ref } from "@vue/reactivity"
export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      async createKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
          await keepsService.createKeep(editable.value);
          editable.value = {}
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