<template>
  <v-app-bar app>
    <v-toolbar-title>
      <v-btn text to="/"> Piesu </v-btn>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-xs-only">
      <v-btn text to="/login">
        <v-icon left dark>mdi-lock-open</v-icon>
        Zaloguj
      </v-btn>
      <v-btn text to="/register">
        <v-icon left dark>mdi-account-circle</v-icon>
        Zarejestuj
      </v-btn>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
export default {
  name: "UserNav",
};
</script>
