import { http } from "@/helpers/axios-instances";

export const fetchBreeds = () =>
  http.get("/breeds").then((response) => response.data);
