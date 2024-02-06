<template>
  <v-container>
    <div class="heading-container">
      <h1>Ogłoszenia</h1>
      <v-btn elevation="1" @click="onNewAdvert">Dodaj ogłoszenie</v-btn>
    </div>

    <div class="ad-container">
      <div v-if="adsParsed.length === 0">
        Aktualnie nie ma dostępnych ogłoszeń. Nowo dodane ogłoszenia oczekują na
        akceptację przez administrację
      </div>
      <AdvertCard
        v-for="ad in adsParsed"
        :key="ad.id"
        :ad="ad"
        :category="getCategory(ad.id)"
      ></AdvertCard>
    </div>
  </v-container>
</template>

<script>
import { defineComponent, ref, computed } from "@vue/composition-api";
import { fetchAds } from "@/api/ads";
import { redirectToRoute } from "@/use/router";
import { fetchCategories } from "@/api/categories";
import AdvertCard from "@/components/AdvertCard";

export default defineComponent({
  components: { AdvertCard },
  setup() {
    const ads = ref([]);
    const categories = ref([]);

    fetchCategories().then((data) => {
      categories.value = data;
    });

    fetchAds().then((data) => {
      ads.value = data;
    });

    const onNewAdvert = () => {
      redirectToRoute("/adverts/0?new=true");
    };

    const adsParsed = computed(() =>
      ads.value.map((ad) => ({
        ...ad,
        category: categories.value.find((item) => item.id === ad.categoryId),
      }))
    );

    const getCategory = (id) => {
      const category = categories.value.find((item) => item.id === id);

      return category?.name || "Ogólne";
    };

    return { adsParsed, getCategory, onNewAdvert };
  },
});
</script>

<style scoped>
.heading-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.ad {
  display: flex;
  align-items: flex-start;
  margin: 1rem;
}

.ad-content {
  flex-grow: 1;
}
</style>
