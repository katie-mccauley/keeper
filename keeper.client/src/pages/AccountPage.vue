<template>
  <div class="about text-start">
    <div class="d-flex align-items-centerpb-5 m-2">
      <img class="rounded" :src="account.picture" alt="" />
      <h1 class="p-5 pt-2 ps-4">{{ account.name }}</h1>
      <h3>{{ keepsAmount?.length }}</h3>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account),
      keepsAmount: computed(() => AppState.keeps.filter(k => k.creatorId == AppState.account.id))
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
