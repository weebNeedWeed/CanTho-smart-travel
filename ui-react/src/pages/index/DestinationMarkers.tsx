import { Marker, Popup, Tooltip } from "react-leaflet";
import useDestsGeoJson from "../../hooks/useDestsGeoJson";
import L from "leaflet";
import { useMapContext } from "../../contexts/MapContext";

export default function DestinationMarkers() {
  const {
    data: geoJsonData,
    isLoading: isGeoJsonLoading,
    isError: isGeoJsonError,
  } = useDestsGeoJson();
  const { setSelectedDestination, selectedDestination, mapSelecting } =
    useMapContext();

  const selectDest = (e: L.LeafletMouseEvent, feature: any) => {
    if (!mapSelecting) {
      return;
    }

    setSelectedDestination({
      id: feature.properties.Id,
      name: feature.properties.Name,
      location: {
        coordinates: feature.geometry.coordinates,
      },
    });

    setTimeout(() => {
      e.target.openPopup();
    }, 50);

    setTimeout(() => {
      e.target.closePopup();
    }, 1500);
  };

  return (
    <>
      {!isGeoJsonLoading &&
        !isGeoJsonError &&
        geoJsonData!.data.features.map((f: any, i: number) => {
          const icon = getIcon(f.properties.DestinationCategoryId);

          return (
            <Marker
              eventHandlers={{ click: (e) => selectDest(e, f) }}
              icon={icon}
              key={i}
              position={f.geometry.coordinates}
            >
              {selectedDestination.id === f.properties.Id && (
                <Popup>Bạn đang chọn địa điểm này!</Popup>
              )}
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
