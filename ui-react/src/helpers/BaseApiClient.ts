// apiClient.js
import axios, { AxiosInstance } from "axios";

class BaseApiClient {
  client: AxiosInstance;
  constructor(token: string | undefined = undefined) {
    this.client = axios.create({
      baseURL: import.meta.env.VITE_BASE_URL,
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (token) {
      this.setToken(token);
    }
  }

  // Set Authorization header for all requests
  setToken(token: string) {
    this.client.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }

  // Clear Authorization header
  clearToken() {
    delete this.client.defaults.headers.common["Authorization"];
  }

  // Generic request methods
  get(endpoint: string, params: any = {}, headers: any = {}) {
    return this.client.get(endpoint, { params, headers });
  }

  post(endpoint: string, body: any = {}, headers: any = {}) {
    return this.client.post(endpoint, body, { headers });
  }

  put(endpoint: string, body: any = {}, headers: any = {}) {
    return this.client.put(endpoint, body, { headers });
  }

  delete(endpoint: string, headers: any = {}) {
    return this.client.delete(endpoint, { headers });
  }

  patch(endpoint: string, body: any = {}, headers: any = {}) {
    return this.client.patch(endpoint, body, { headers });
  }
}

export default BaseApiClient;
