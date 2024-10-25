import { MapContainer, TileLayer, ZoomControl } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet.awesome-markers/dist/leaflet.awesome-markers.css";
import "leaflet.awesome-markers";
import NavBar from "./NavBar";
import CurrentLocation from "./CurrentLocation";
import { useState } from "react";
import DestinationMarkers from "./DestinationMarkers";
import DestinationList from "./DestinationList";

export default function HomePage() {
  const [map, setMap] = useState<L.Map | null>(null);
  return (
    <div className="h-screen w-screen fixed">
      <NavBar map={map || undefined} />

      <div className="absolute w-full h-full top-0 left-0 z-10">
        <MapContainer
          center={[10.029965384684171, 105.77084060032108]}
          zoom={12}
          scrollWheelZoom={true}
          style={{ height: "100vh" }}
        >
          <CurrentLocation setMap={setMap} />

          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />

          <DestinationMarkers />

          <ZoomControl position="bottomright" />
        </MapContainer>
      </div>

      <DestinationList map={map} />
    </div>
  );
}
