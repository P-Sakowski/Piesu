import axios from "axios";
import { logout, getAccessToken, setToken } from "@/use/user";
import { refreshTokenRequest } from "@/api/auth";
import { redirectToRoute } from "@/use/router";
import { isTokenValid } from "./jwt";

export const http = axios.create({
  baseURL: `${process.env.VUE_APP_API_PREFIX}/api`,
});

// Response interceptor
http.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      logout();
      redirectToRoute("/login");
    }
    console.error(error.response); // eslint-disable-line

    return Promise.reject(error);
  }
);

// Request interceptor
http.interceptors.request.use(async (config) => {
  try {
    if (Object.hasOwnProperty.call(config.headers, "Authorization")) {
      return config;
    }

    const authorizationToken = getAccessToken();
    if (isTokenValid(authorizationToken)) {
      config.headers.Authorization = `Bearer ${authorizationToken}`;

      return config;
    }

    const { token } = await refreshTokenRequest();
    if (isTokenValid(token)) {
      config.headers.Authorization = `Bearer ${token}`;
      setToken({ token });
    }

    return config;
  } catch (err) {
    redirectToRoute("/login");
  }
});

export const auth = axios.create({
  baseURL: `${process.env.VUE_APP_API_PREFIX}/api/auth`,
});
