<template>
  <v-container>
    <h1>Moje zwierzaki</h1>

    <div class="pet-container">
      <div v-for="dog in dogs" :key="dog.id">
        <v-menu offset-y absolute>
          <template v-slot:activator="{ on, attrs }">
            <div class="pet-card" v-bind="attrs" v-on="on">
              <v-avatar color="primary" size="128">
                <span class="avatar">{{
                  dog.name.slice(0, 2).toUpperCase()
                }}</span>
              </v-avatar>

              <h3>{{ dog.name }}</h3>
              <h4>{{ dog.breed.name }}</h4>
            </div>
          </template>
          <v-list>
            <v-list-item @click="edit(dog.id)">Edytuj</v-list-item>
            <v-list-item @click="remove(dog.id)">Usuń</v-list-item>
          </v-list>
        </v-menu>
      </div>
      <div class="pet-card add-new" @click="addNew">
        <v-avatar color="primary" size="128"
          ><v-icon size="64" dark>mdi-plus</v-icon></v-avatar
        >
        <h3>Dodaj</h3>
      </div>
    </div>
  </v-container>
</template>

<script>
import { defineComponent, ref } from "@vue/composition-api";
import { fetchDogs, deleteDog } from "@/api/dogs";
import { redirectToRoute } from "../use/router";

export default defineComponent({
  setup() {
    const dogs = ref([]);

    const fetchData = () => {
      fetchDogs().then((data) => {
        dogs.value = data;
      });
    };

    fetchData();

    const addNew = () => {
      redirectToRoute("/dogs/0?new=true");
    };

    const selectedId = ref(null);
    const showDialog = ref(false);

    const showEditDialog = (id) => {
      selectedId.value = id;
      showDialog.value = true;
    };

    const edit = (id) => {
      redirectToRoute(`/dogs/${id}`);
    };

    const remove = (id) => {
      deleteDog(id).then(() => {
        fetchData();
      });
    };

    return { dogs, addNew, showEditDialog, edit, remove };
  },
});
</script>

<style scoped>
.cursor-pointer {
  cursor: pointer;
}

.pet-container {
  display: flex;
  align-items: center;
  justify-content: center;
  align-items: flex-start;
  flex-wrap: wrap;
}

.pet-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  cursor: pointer;
  padding: 2rem;
}

.avatar {
  color: #fff;
}
</style>
