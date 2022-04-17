<template>
  <div class="container-fluid mt-2">
    <div class="row m-3 ms-0">
      <div class="col-1">
        <img :src="profile.picture" class="img-fluid cropped" alt="" />
      </div>
      <div class="col-5">
        <h1>{{ profile.name }}</h1>
        <h3>Keeps: {{ keeps.length }}</h3>
        <h3>Vaults: {{ vaults.length }}</h3>
      </div>
    </div>
    <h2>
      Keeps:
      <i
        data-bs-toggle="modal"
        data-bs-target="#create-keep"
        class="mdi mdi-plus text-primary"
      ></i>
    </h2>
    <div class="masonry-with-columns">
      <div class="space" v-for="k in keeps" :key="k.id">
        <!-- <img :src="k.img" class="rounded cropped img-fluid" alt="" /> -->
        <Keep :keepData="k" />
      </div>
    </div>
    <h2 class="mt-5">
      Vaults:
      <i
        data-bs-toggle="modal"
        data-bs-target="#create-vault"
        class="mdi mdi-plus text-primary"
      ></i>
    </h2>
    <div class="masonry-with-columns">
      <div class="space" v-for="v in vaults" :key="v.id">
        <div class="card selectable border border-3">
          <Vault :vault="v" />
        </div>
      </div>
    </div>
  </div>
  <Modal id="create-keep">
    <template #modal-title>Create Keep</template>
    <template #modal-body>
      <CreateKeep />
    </template>
  </Modal>
  <Modal id="create-vault">
    <template #modal-title>Create Vault</template>
    <template #modal-body>
      <CreateVault />
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { profilesService } from "../services/ProfilesService"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await profilesService.getUser(route.params.id)
        await profilesService.getUserKeeps(route.params.id)
        await profilesService.getUserVaults(route.params.id)
      } catch (error) {
        logger.log(error.message)
      }
    })
    return {
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}
.space {
  display: inline-block;
  margin: 0 0 0.5em;
  width: 100%;
}

.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}
</style>