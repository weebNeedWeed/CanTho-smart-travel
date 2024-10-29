import BaseApiClient from "./BaseApiClient";

class ProfileApiClient extends BaseApiClient {
  getAllSettings() {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.get("/profile/settings");
  }

  saveSettings(settings: any) {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.post("/profile/settings", settings);
  }
}

export const defaultProfileApiClient = new ProfileApiClient();
