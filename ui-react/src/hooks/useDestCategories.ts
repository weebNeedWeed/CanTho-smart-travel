import { useQuery } from "react-query";
import { defaultDestinationApiClient } from "../helpers/DestinationApiClient";

export default function useDestCategories() {
  return useQuery("DestCategories", () =>
    defaultDestinationApiClient.getAllDestCategories()
  );
}
