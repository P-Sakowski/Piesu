import { http } from "@/helpers/axios-instances";

export const fetchAds = () =>
  http.get("/adverts").then((response) => response.data);

export const fetchUnmoderatedAds = () =>
  http.get("/adverts/unmoderated").then((response) => response.data);

export const saveAd = (data) => http.post("/adverts", data);

export const fetchAdById = (id) =>
  http.get(`/adverts/${id}`).then((response) => response.data);

export const updateAd = (id, data) => http.put(`/adverts/${id}`, data);

export const deleteAd = (id) => http.delete(`/adverts/${id}`);

export const acceptAd = (id) => http.post(`/adverts/${id}/approve`);

export const declineAd = (id) => http.post(`/adverts/${id}/reject`);
