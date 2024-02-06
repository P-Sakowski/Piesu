<template>
  <v-container>
    <div class="heading-container">
      <h1>Administracja</h1>
    </div>

    <v-data-table :headers="headers" :items="users">
      <template #item.emailConfirmed="{ item }">
        <v-chip v-if="item.emailConfirmed" color="success">Tak</v-chip>
        <v-chip v-else color="error">Nie</v-chip>
      </template>
      <template #item.actions="{ item }">
        <v-menu offset-y>
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark v-bind="attrs" v-on="on">
              Nadaj rolę
            </v-btn>
          </template>
          <v-list>
            <!-- eslint-disable vue/no-use-v-if-with-v-for -->
            <v-list-item
              v-if="!(item.roles && item.roles.includes(role.name))"
              v-for="(role, index) in roles"
              :key="index"
            >
              <v-list-item-title
                style="cursor: pointer"
                @click="onUpdateUserRole(item.email, role.name)"
                >{{ role.name }}</v-list-item-title
              >
            </v-list-item>
          </v-list>
        </v-menu>
      </template>
    </v-data-table>
    <div class="ad-container"></div>
  </v-container>
</template>

<script>
import { defineComponent, ref } from "@vue/composition-api";
import AdvertCard from "@/components/AdvertCard";
import { fetchUsers, fetchRoles, updateUserRole } from "@/api/users";

export default defineComponent({
  components: { AdvertCard },
  setup() {
    const users = ref([]);
    const roles = ref([]);

    const fetchData = () => {
      fetchUsers().then((data) => {
        users.value = data;
      });
    };

    fetchData();

    fetchRoles().then((data) => {
      roles.value = data;
    });

    const onUpdateUserRole = (email, role) => {
      console.log(email, role, updateUserRole);
      updateUserRole(email, role);
    };

    const headers = [
      {
        text: "Nazwa konta",
        value: "userName",
      },
      {
        text: "Email",
        value: "email",
      },
      {
        text: "Email potwierdzony",
        value: "emailConfirmed",
      },
      {
        text: "Role",
        value: "roles",
      },
      {
        text: "Akcje",
        value: "actions",
      },
    ];

    return {
      users,
      roles,
      headers,
      onUpdateUserRole,
    };
  },
});
</script>

<style scoped>
.heading-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
