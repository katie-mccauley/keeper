<template>
  <nav class="navbar navbar-expand-lg navcolor px-3 pt-0 pb-0">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-start logo mb-2">
        <h1 class="logodetail p-0 m-0 borderlogo">Keeper</h1>
      </div>
    </router-link>
    <button
      class="navbar-toggler navbar-light"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="col-md-6 justify-content-center align-items-center mt-2 ms-5">
      <form @submit.prevent="search">
        <i class="fa fa-search"></i>
        <input
          type="text"
          class="form-control form-input"
          placeholder="Search anything..."
          v-model="query"
        />
      </form>
    </div>

    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <!-- <li>
          <router-link
            :to="{ name: 'About' }"
            class="btn text-success lighten-30 selectable text-uppercase"
          >
            About
          </router-link>
        </li> -->
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { ref } from "@vue/reactivity";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
  setup() {
    const query = ref("")
    return {
      query,
      async search() {
        try {
          await keepsService.getAll('?search=' + query.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    };
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.navcolor {
  background-color: #81ecec;
}

.borderlogo {
  border: 4px solid #636e72;
  box-sizing: border-box;
}

.logodetail {
  font-family: "Sansation";
  font-style: normal;
  font-weight: 400;
  font-size: 64px;
  line-height: 72px;
  color: #636e72;
}

body {
  background: #d1d5db;
}

.height {
  height: 100vh;
  max-width: 100px;
}

.form .fa-search {
  position: absolute;
  top: 20px;
  left: 100px;
  color: #9ca3af;
}

.form span {
  right: 17px;
  top: 13px;
  padding: 2px;
  border-left: 1px solid #d1d5db;
}

.left-pan {
  padding-left: 7px;
}

.left-pan i {
  padding-left: 10px;
}

.form-input {
  height: 55px;
  text-indent: 33px;
  border-radius: 10px;
  max-width: 500px;
}

.form-input:focus {
  box-shadow: none;
  border: none;
}
</style>
