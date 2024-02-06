<template>
  <v-container>
    <h1>{{ isNew ? "Dodawanie ogłoszenia" : "Modyfikowanie ogłoszenia" }}</h1>

    <v-text-field
      v-model="form.title.value"
      label="Tytuł"
      :error-messages="form.title.errorMessages"
    ></v-text-field>
    <v-select
      v-model="form.categoryId.value"
      :items="categories"
      :item-text="(item) => item.name"
      :item-value="(item) => item.id"
      label="Kategoria"
    ></v-select>
    <v-select
      v-model="form.subcategoryId.value"
      :items="availableSubcategories"
      :item-text="(item) => item.name"
      :item-value="(item) => item.id"
      :error-messages="form.subcategoryId.errorMessages"
      label="Subkategoria"
    ></v-select>
    <v-textarea
      v-model="form.description.value"
      :error-messages="form.description.errorMessages"
      label="Opis"
      auto-grow
      rows="3"
    ></v-textarea>
    <v-text-field v-model="form.price.value" label="Cena"></v-text-field>
    <v-checkbox
      v-model="form.isNegotiable.value"
      label="Czy cena podlega negocjacji"
    />
    <v-btn color="primary" @click="submit">Zapisz</v-btn>
  </v-container>
</template>

<script>
import { defineComponent, computed, ref } from "@vue/composition-api";
import router from "@/router";
import { fetchAdById, saveAd, updateAd } from "@/api/ads";
import { fetchCategories, fetchSubcategories } from "@/api/categories";
import { useForm } from "@/use/validation";
import { redirectToRoute } from "../use/router";

export default defineComponent({
  setup() {
    const isNew = computed(() => !!router.currentRoute.query.new);
    const categories = ref([]);
    const subcategories = ref([]);

    fetchCategories().then((data) => {
      categories.value = data;
    });
    fetchSubcategories().then((data) => {
      subcategories.value = data;
    });

    const availableSubcategories = computed(() =>
      subcategories.value.filter(
        (subcategory) => subcategory.categoryId === form.value.categoryId.value
      )
    );

    const { form, fields, useField, submit } = useForm({
      onSubmit() {
        const request = isNew.value
          ? saveAd(fields.value)
          : updateAd(fields.value.id, fields.value);

        request.then(() => redirectToRoute("/adverts"));
      },
    });

    useField("title", {
      rules: {
        required: (value) => ({
          valid: value.length >= 3,
          message: "Wartość jest zbyt krótka",
        }),
      },
      value: "",
    });
    useField("description", {
      rules: {
        required: (value) => ({
          valid: value.length >= 3,
          message: "Wartość jest zbyt krótka",
        }),
      },
      value: "",
    });
    useField("price", { value: 0 });
    useField("isNegotiable", { value: false });
    useField("locationId", { value: 0 });
    useField("categoryId", { value: 1 });
    useField("subcategoryId", {
      rules: {
        validSubcategory: (subcategoryId) => {
          const subcategory = subcategories.value.find(
            (item) => item.id === subcategoryId
          );

          if (subcategory.categoryId === form.value.categoryId.value)
            return true;

          return {
            valid: false,
            message: "Proszę wybrać prawidłową katerogię",
          };
        },
      },
      value: 1,
    });

    if (!isNew.value) {
      const id = router.currentRoute.params.id;
      fetchAdById(id).then((data) => {
        fields.value = data;
      });
    }

    return {
      isNew,
      form,
      fields,
      categories,
      availableSubcategories,
      submit,
    };
  },
});
</script>
