import { useQuery } from "react-query";
import { defaultProfileApiClient } from "../helpers/ProfileApiClient";

export default function useProfileSettings() {
  return useQuery(
    "ProfileSettings",
    () => defaultProfileApiClient.getAllSettings(),
    { staleTime: 5 * 60 * 1000 }
  );
}
