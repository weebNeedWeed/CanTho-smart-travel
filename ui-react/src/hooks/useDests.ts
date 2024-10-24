import { useQuery } from "react-query";
import { defaultDestinationApiClient } from "../helpers/DestinationApiClient";

export default function useDests() {
  return useQuery("Dests", () => defaultDestinationApiClient.getAllDests(), {
    staleTime: 5 * 60 * 1000,
  });
}
