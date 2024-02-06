<template>
  <v-app-bar app>
    <v-toolbar-title>
      <v-btn text to="/"> Piesu </v-btn>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-xs-only">
      <v-btn text to="/dogs">
        <v-icon left dark>mdi-paw</v-icon>
        Moje Zwierzaki
      </v-btn>
      <v-btn text to="/adverts">
        <v-icon left dark>mdi-book</v-icon>
        Ogłoszenia
      </v-btn>
      <v-btn text to="/login" @click="logout">
        <v-icon left dark>mdi-door</v-icon>
        Wyloguj
      </v-btn>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
import { useUser } from "../use/user";

export default {
  name: "UserNav",
  setup() {
    const { isUserLogged, logout } = useUser();

    return {
      isUserLogged,
      logout,
    };
  },
};
</script>
