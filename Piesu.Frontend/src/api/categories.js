import { http } from "@/helpers/axios-instances";

export const fetchCategories = () =>
  http.get("/categories").then((response) => response.data);

export const saveCategory = (data) => http.post("/categories", data);

export const fetchCategoryById = (id) =>
  http.get(`/categories/${id}`).then((response) => response.data);

export const updateCategory = (id, data) => http.put(`/categories/${id}`, data);

export const deleteCategory = (id) => http.delete(`/categories/${id}`);

export const fetchSubcategories = () =>
  http.get("/subcategories").then((response) => response.data);

export const saveSubcategory = (data) => http.post("/subcategories", data);

export const fetchSubcategoryById = (id) =>
  http.get(`/subcategories/${id}`).then((response) => response.data);

export const updateSubcategory = (id, data) =>
  http.put(`/subcategories/${id}`, data);

export const deleteSubcategory = (id) => http.delete(`/subcategories/${id}`);
