<template>
  <nav class="navbar navbar-expand-lg navcolor px-3 pt-0 pb-0">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-start logo mb-2">
        <h1 class="logodetail p-0 m-0 borderlogo">Keeper</h1>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
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
export default {
  setup() {
    return {};
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
</style>
