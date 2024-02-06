import router from "@/router";

export const redirectToRoute = (route) => {
  router.push(route).catch(() => {});
};
