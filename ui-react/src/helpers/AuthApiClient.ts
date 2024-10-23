import BaseApiClient from "./BaseApiClient";

class AuthApiClient extends BaseApiClient {
  register(username: string, password: string) {
    return this.post("/auth/register", { username, password });
  }

  login(username: string, password: string) {
    return this.post("/auth/login", { username, password });
  }
}

export const defaultAuthApiClient = new AuthApiClient();
