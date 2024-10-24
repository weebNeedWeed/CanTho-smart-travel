import { useQuery } from "react-query";
import { defaultDestinationApiClient } from "../helpers/DestinationApiClient";

export default function useDestsGeoJson() {
  return useQuery(
    "DestsGeoJson",
    () => defaultDestinationApiClient.getAllDestsAsGeoJson(),
    { staleTime: 5 * 60 * 1000 }
  );
}
