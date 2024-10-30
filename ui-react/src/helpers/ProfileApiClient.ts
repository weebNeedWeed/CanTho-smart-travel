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

  getAllItineraries() {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.get("/profile/itineraries");
  }

  deleteItinerary(id: number) {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.delete(`/profile/itineraries/${id}`);
  }

  createItinerary() {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.post(`/profile/itineraries`);
  }

  updateItinerary(itiId: number, payload: any) {
    const token = localStorage.getItem("access_token");
    if (!token) {
      throw new Error("no token found");
    }
    this.setToken(token);
    return this.patch(`/profile/itineraries/${itiId}`, payload);
  }
}

export const defaultProfileApiClient = new ProfileApiClient();
