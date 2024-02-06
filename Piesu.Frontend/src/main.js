import Vue from "vue";
import "@/plugins/composition-api";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";

Vue.config.productionTip = false;

const vue = new Vue({
  router,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");

export default vue;
