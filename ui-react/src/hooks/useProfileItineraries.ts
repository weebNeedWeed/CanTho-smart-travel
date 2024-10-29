import { useQuery } from "react-query";
import { defaultProfileApiClient } from "../helpers/ProfileApiClient";

export default function useProfileItineraries() {
  return useQuery("ProfileItineraries", () =>
    defaultProfileApiClient.getAllItineraries()
  );
}
