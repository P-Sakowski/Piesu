<template>
  <v-app>
    <NavLogout v-if="!isUserLogged"></NavLogout>
    <template v-else>
      <NavAdmin v-if="isAdmin"></NavAdmin>
      <NavUser v-else></NavUser>
    </template>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import { useUser } from "./use/user";
import NavLogout from "./components/NavLogout.vue";
import NavAdmin from "./components/NavAdmin.vue";
import NavUser from "./components/NavUser.vue";

export default {
  name: "App",
  components: { NavLogout, NavAdmin, NavUser },
  setup() {
    const { isUserLogged, isAdmin, user } = useUser();

    console.log(user);

    return {
      isUserLogged,
      isAdmin,
    };
  },
};
</script>

<style>
h1 {
  padding: 2rem 0;
}
</style>
