import axios, { AxiosInstance } from "axios";

class SearchApiClient {
  client: AxiosInstance;
  constructor() {
    this.client = axios.create({
      baseURL: import.meta.env.VITE_BASE_LLM_URL,
      headers: {
        "Content-Type": "application/json",
      },
      timeout: 15000,
    });
  }
  searchDestination(keyword: string) {
    return this.client.get("/search", {
      params: {
        keyword,
      },
    });
  }

  generateItinerary(payload: any) {
    return this.client.post("/gen", payload);
  }
}

export const defaultSearchApiClient = new SearchApiClient();
