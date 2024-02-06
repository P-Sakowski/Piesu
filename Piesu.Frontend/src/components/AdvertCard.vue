<template>
  <v-card outlined class="ad">
    <img
      src="https://dummyimage.com/250x250/ebebeb/c1c1c1.jpg"
      max-width="250px"
    />
    <div class="ad-content">
      <v-card-title>{{ ad.title }}</v-card-title>
      <v-card-subtitle>
        <span>{{ category || "Ogólne" }}</span>
      </v-card-subtitle>
      <v-card-text>{{ ad.description }}</v-card-text>
      <slot name="actions"></slot>
    </div>
  </v-card>
</template>

<script>
import { defineComponent } from "@vue/composition-api";

export default defineComponent({
  props: ["ad", "category"],
});
</script>

<style scoped>
.ad {
  display: flex;
  align-items: flex-start;
  margin: 1rem;
}

.ad-content {
  flex-grow: 1;
}
</style>
