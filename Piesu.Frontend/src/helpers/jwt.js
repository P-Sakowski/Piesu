import decode from "jwt-decode";

export const isTokenValid = (token) => {
  if (!token) return false;

  try {
    const decodedToken = decode(token);
    const tokenExpirationDate = decodedToken.exp;
    const nowDate = Math.floor(Date.now() / 1000);

    return tokenExpirationDate > nowDate;
  } catch (err) {
    return false;
  }
};

export const decodeToken = (token) => decode(token);
