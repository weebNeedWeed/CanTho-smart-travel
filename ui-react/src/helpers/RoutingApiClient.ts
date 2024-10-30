import axios, { AxiosInstance } from "axios";

class RoutingApiClient {
  client: AxiosInstance;
  constructor() {
    this.client = axios.create({
      baseURL: "https://router.project-osrm.org",
      headers: {
        "Content-Type": "application/json",
      },
      timeout: 5000,
    });
  }
  searchDestination(start: number[], end: number[]) {
    return this.client.get(
      `/route/v1/driving/${start[1]},${start[0]};${end[1]},${end[0]}?geometries=geojson&steps=true`
    );
  }
}

export const defaultRoutingApiClient = new RoutingApiClient();
