import axios, { AxiosInstance } from "axios";

class SearchApiClient {
  client: AxiosInstance;
  constructor() {
    this.client = axios.create({
      baseURL: import.meta.env.VITE_BASE_LLM_URL,
      headers: {
        "Content-Type": "application/json",
      },
      timeout: 5000,
    });
  }
  searchDestination(keyword: string) {
    return this.client.get("/search", {
      params: {
        keyword,
      },
    });
  }
}

export const defaultSearchApiClient = new SearchApiClient();
