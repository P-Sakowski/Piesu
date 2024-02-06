import { auth } from "@/helpers/axios-instances";

export const loginRequest = ({ username, password }) =>
  auth.post("/login", { username, password }).then((response) => response.data);

export const refreshTokenRequest = () =>
  auth.post("/refresh").then((response) => response.data);
