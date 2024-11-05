import { LatLng } from "leaflet";
import { useEffect, useState } from "react";
import { Marker, Popup, useMap } from "react-leaflet";
import L from "leaflet";
import { useMapContext } from "../../contexts/MapContext";

interface CurrentLocationProps {
  setMap: (map: L.Map) => void;
}
export default function CurrentLocation(props: CurrentLocationProps) {
  const [position, setPosition] = useState<LatLng | null>(null);
  const { setMap, setCurrentLocation } = useMapContext();
  const map = useMap();
  useEffect(() => {
    navigator.geolocation.getCurrentPosition((pos) => {
      const { latitude, longitude } = pos.coords;
      setPosition(new LatLng(latitude, longitude));
      setCurrentLocation(new LatLng(latitude, longitude));
      map.flyTo([latitude, longitude], 17);
    });
  }, []);

  useEffect(() => {
    if (!map) return;
    props.setMap(map);
    setMap(map);
  }, [map]);

  const icon = L.AwesomeMarkers.icon({
    icon: "user",
    prefix: "fa",
    markerColor: "red",
  });

  return position === null ? null : (
    <Marker
      icon={icon}
      position={position}
      eventHandlers={{
        mouseover: (e) => e.target.openPopup(),
        mouseout: (e) => e.target.closePopup(),
      }}
    >
      <Popup>Bạn đang ở đây!</Popup>
    </Marker>
  );
}
