import { http } from "@/helpers/axios-instances";

export const fetchDogs = () =>
  http.get("/dogs").then((response) => response.data);

export const fetchDogById = (id) =>
  http.get(`/dogs/${id}`).then((response) => response.data);

export const saveDog = (data) => http.post("/dogs", data);

export const updateDog = (id, data) => http.put(`/dogs/${id}`, data);

export const deleteDog = (id) => http.delete(`/dogs/${id}`);
