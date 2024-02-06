import { http } from "@/helpers/axios-instances";

export const fetchUsers = () =>
  http.get("/users").then((response) => response.data);

export const fetchRoles = () =>
  http.get("/roles").then((response) => response.data);

export const fetchUserRoles = (email) =>
  http
    .get(`/roles/get-user-roles?email=${email}`)
    .then((response) => response.data);

export const updateUserRole = (email, role) =>
  http.post(`/roles/add-user-to-role?email=${email}&roleName=${role}`);
