<template>
  <v-container>
    <h1>{{ isNew ? "Dodawanie psa" : "Modyfikowanie psa" }}</h1>

    <v-text-field v-model="form.name.value" label="Imię"></v-text-field>
    <v-select
      v-model="form.breedId.value"
      :items="breeds"
      :item-text="(item) => item.name"
      :item-value="(item) => item.id"
      label="Rasa"
    ></v-select>
    <v-text-field v-model="form.description.value" label="Opis"></v-text-field>
    <v-btn color="primary" @click="submit">Zapisz</v-btn>
  </v-container>
</template>

<script>
import { defineComponent, computed, ref } from "@vue/composition-api";
import router from "@/router";
import { fetchDogById, saveDog, updateDog } from "@/api/dogs";
import { fetchBreeds } from "@/api/breeds";
import { useForm } from "@/use/validation";
import { redirectToRoute } from "../use/router";

export default defineComponent({
  setup() {
    const isNew = computed(() => !!router.currentRoute.query.new);

    const breeds = ref([]);

    fetchBreeds().then((data) => {
      breeds.value = data;
    });

    const { form, fields, useField, submit } = useForm({
      onSubmit() {
        const request = isNew.value
          ? saveDog(fields.value)
          : updateDog(fields.value.id, fields.value);

        request.then(() => redirectToRoute("/dogs"));
      },
    });

    useField("name");
    useField("description");
    useField("breedId");

    if (!isNew.value) {
      const id = router.currentRoute.params.id;
      fetchDogById(id).then((data) => {
        fields.value = data;
        form.value.breedId.value = data.breed.id;
      });
    }

    return {
      isNew,
      breeds,
      form,
      submit,
    };
  },
});
</script>
