import Vue from "vue";
import VueRouter from "vue-router";
import { useUser } from "@/use/user";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    redirect: () => {
      const { isUserLogged } = useUser();
      if (isUserLogged.value) return "/dogs";
      return "/login";
    },
  },
  {
    path: "/login",
    component: () =>
      import(/* webpackChunkName: "login" */ "@/views/ViewLogin.vue"),
  },
  {
    path: "/register",
    component: () =>
      import(/* webpackChunkName: "login" */ "@/views/ViewRegister.vue"),
  },
  {
    path: "/dogs",
    component: () =>
      import(/* webpackChunkName: "dogs" */ "@/views/ViewDogs.vue"),
  },
  {
    path: "/dogs/:id",
    component: () =>
      import(/* webpackChunkName: "dogs" */ "@/views/ViewDogsDetails.vue"),
  },
  {
    path: "/adverts",
    component: () =>
      import(/* webpackChunkName: "ads" */ "@/views/ViewAdverts.vue"),
  },
  {
    path: "/adverts/:id",
    component: () =>
      import(/* webpackChunkName: "ads" */ "@/views/ViewAdvertsDetails.vue"),
  },
  {
    path: "/mod",
    component: () =>
      import(/* webpackChunkName: "mod" */ "@/views/ViewMod.vue"),
  },
  {
    path: "/admin",
    component: () =>
      import(/* webpackChunkName: "mod" */ "@/views/ViewAdmin.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
