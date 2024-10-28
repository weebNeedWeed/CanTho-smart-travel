import BaseApiClient from "./BaseApiClient";

class DestinationApiClient extends BaseApiClient {
  getAllDestsAsGeoJson() {
    return this.get("/destinations/:geojson");
  }

  getAllDests(ids: number[] = []) {
    return this.client.get("/destinations", {
      params: {
        ids,
      },
      paramsSerializer: {
        indexes: null, // brackets but no indexes
      },
    });
  }

  getAllDestCategories() {
    return this.get("/categories");
  }
}

export const defaultDestinationApiClient = new DestinationApiClient();
