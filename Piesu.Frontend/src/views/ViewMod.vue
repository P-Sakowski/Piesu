<template>
  <v-container>
    <div class="heading-container">
      <h1>Moderacja ogłoszeń</h1>
    </div>

    <div class="ad-container">
      <div v-if="adsParsed.length === 0">Brak ogłoszeń do moderacji</div>
      <AdvertCard
        v-for="ad in adsParsed"
        :key="ad.id"
        :ad="ad"
        :category="getCategory(ad.id)"
      >
        <template #actions>
          <div class="actions-container">
            <v-btn color="error" @click="decline(ad.id)">Odrzuć</v-btn>
            <v-btn color="success" @click="accept(ad.id)">Zatwierdź</v-btn>
          </div>
        </template>
      </AdvertCard>
    </div>
  </v-container>
</template>

<script>
import { defineComponent, ref, computed } from "@vue/composition-api";
import { fetchUnmoderatedAds, acceptAd, declineAd } from "@/api/ads";
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

    const fetchData = () => {
      fetchUnmoderatedAds().then((data) => {
        ads.value = data;
      });
    };

    fetchData();

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

    const accept = async (id) => {
      await acceptAd(id);
      fetchData();
    };

    const decline = async (id) => {
      await declineAd(id);
      fetchData();
    };

    return { adsParsed, getCategory, accept, decline };
  },
});
</script>

<style scoped>
.heading-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.actions-container {
  display: flex;
  justify-content: flex-end;
  align-items: flex-end;
  gap: 1rem;
  margin: 1rem;
}
</style>
