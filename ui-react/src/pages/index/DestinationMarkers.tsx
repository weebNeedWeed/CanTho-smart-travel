import { Marker, Tooltip } from "react-leaflet";
import useDestsGeoJson from "../../hooks/useDestsGeoJson";
import L from "leaflet";

export default function DestinationMarkers() {
  const {
    data: geoJsonData,
    isLoading: isGeoJsonLoading,
    isError: isGeoJsonError,
  } = useDestsGeoJson();

  return (
    <>
      {!isGeoJsonLoading &&
        !isGeoJsonError &&
        geoJsonData!.data.features.map((f: any, i: number) => {
          const icon = getIcon(f.properties.DestinationCategoryId);

          return (
            <Marker icon={icon} key={i} position={f.geometry.coordinates}>
              <Tooltip
                direction="right"
                offset={[10, -10]}
                opacity={1}
                permanent
              >
                {f.properties.Name}
              </Tooltip>
            </Marker>
          );
        })}
    </>
  );
}

function getIcon(destinationCategoryId: number) {
  const options: L.AwesomeMarkers.AwesomeMarkersIconOptions = {
    icon: "user",
    prefix: "fa",
    markerColor: "red",
    spin: true,
  };

  switch (destinationCategoryId) {
    case 1:
    case 2:
    case 3:
      options.icon = "map-location-dot";
      options.markerColor = "cadetblue";
      break;
  }

  return L.AwesomeMarkers.icon(options);
}
