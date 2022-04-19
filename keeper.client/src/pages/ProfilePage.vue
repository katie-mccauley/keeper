<template>
  <div class="container-fluid mt-2 textcolor">
    <div class="row m-3 ms-0">
      <div class="col-md-1 col-3">
        <div class="card">
          <img :src="profile.picture" class="img-fluid" alt="" />
        </div>
      </div>
      <div class="col-md-5 col-7">
        <h1>{{ profile.name }}</h1>
        <h3>Keeps: {{ keeps.length }}</h3>
        <h3>Vaults: {{ vaults.length }}</h3>
      </div>
    </div>
    <h2>
      Keeps:
      <i
        v-if="account.id == profile.id"
        data-bs-toggle="modal"
        data-bs-target="#create-keep"
        class="mdi mdi-plus text-primary selectable"
        title="Create Keep"
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
        v-if="account.id == profile.id"
        data-bs-toggle="modal"
        data-bs-target="#create-vault"
        class="mdi mdi-plus text-primary selectable"
        title="Create Vault"
      ></i>
    </h2>
    <div class="masonry-with-columns">
      <div class="space" v-for="v in vaults" :key="v.id">
        <Vault :vault="v" />
      </div>
    </div>
  </div>
  <KeepDetails />

  <ModalForm id="create-keep">
    <template #title>Create Keep</template>
    <template #body>
      <CreateKeep />
    </template>
  </ModalForm>
  <ModalForm id="create-vault">
    <template #title>Create Vault</template>
    <template #body>
      <CreateVault />
    </template>
  </ModalForm>
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { profilesService } from "../services/ProfilesService"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        if (route.name == 'Profile') {
          await profilesService.getUser(route.params.id)
          await profilesService.getUserKeeps(route.params.id)
          await profilesService.getUserVaults(route.params.id)
          // await vaultsService.getAccountVaults()
        }

      } catch (error) {

        logger.log(error.message)
      }
    })
    return {
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}

@media screen AND (max-width: 600px) {
  .masonry-with-columns {
    columns: 2 150px;
    column-gap: 0.8rem;
  }
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

.textcolor {
  font-family: "Inter";
  font-style: normal;
  color: #000000;
}
</style>