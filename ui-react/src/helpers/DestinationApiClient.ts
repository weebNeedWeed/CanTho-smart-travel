import BaseApiClient from "./BaseApiClient";

class DestinationApiClient extends BaseApiClient {
  getAllDestsAsGeoJson() {
    return this.get("/destinations/:geojson");
  }

  getAllDests() {
    return this.get("/destinations");
  }

  getAllDestCategories() {
    return this.get("/categories");
  }
}

export const defaultDestinationApiClient = new DestinationApiClient();
