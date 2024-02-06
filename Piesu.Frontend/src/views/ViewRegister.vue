<template>
  <v-main>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Rejestracja</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <form @submit.prevent="onSubmit">
                <v-text-field
                  v-model="form.userName"
                  name="username"
                  label="Nazwa konta"
                  type="text"
                ></v-text-field>
                <v-text-field
                  v-model="form.email"
                  name="email"
                  label="Email"
                  type="text"
                ></v-text-field>
                <v-text-field
                  v-model="form.password"
                  :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                  :rules="[rules.required, rules.min]"
                  :type="show1 ? 'text' : 'password'"
                  name="input-10-1"
                  label="Hasło"
                  @click:append="show1 = !show1"
                ></v-text-field>
                <v-text-field
                  v-model="confirmpassword"
                  :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                  :rules="[rules.required, rules.min, rules.passwordMatch]"
                  :type="show1 ? 'text' : 'password'"
                  name="input-10-1"
                  label="Powtórz hasło"
                  @click:append="show2 = !show2"
                ></v-text-field>
                <v-btn color="primary" type="submit">Zarejestruj</v-btn>
              </form>
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-main>
</template>

<script>
import { reactive, ref } from "@vue/composition-api";
import { auth } from "../helpers/axios-instances";
import { redirectToRoute } from "../use/router";

export default {
  name: "ViewRegister",
  setup() {
    const show1 = ref(false);
    const show2 = ref(false);
    const rules = ref({
      required: (value) => !!value || "Pole wymagane",
      min: (v) => v.length >= 6 || "Minimum 6 znaków",
      passwordMatch: (v) => v === form.password || `Hasło nie pasuje`,
    });

    const form = reactive({
      userName: "",
      password: "",
      email: "",
    });

    const confirmpassword = ref("");

    const onSubmit = async () => {
      await auth.post("/register", form);
      redirectToRoute("/login");
    };

    return {
      show1,
      show2,
      rules,
      form,
      onSubmit,
      confirmpassword,
    };
  },
};
</script>
