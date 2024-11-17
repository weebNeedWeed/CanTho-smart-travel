import { MapContainer, TileLayer, ZoomControl, GeoJSON } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet.awesome-markers/dist/leaflet.awesome-markers.css";
import "leaflet.awesome-markers";
import NavBar from "./NavBar";
import CurrentLocation from "./CurrentLocation";
import { useState } from "react";
import DestinationMarkers from "./DestinationMarkers";
import DestinationList from "./DestinationList";
import MapContextProvider from "../../contexts/MapContext";
import RoutingMachine from "./RoutingMachine";
import canThoGeoJson from "./../../assets/cantho.json";

export default function HomePage() {
  const [map, setMap] = useState<L.Map | null>(null);
  return (
    <MapContextProvider>
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
              url="https://tile.openstreetmap.org/{z}/{x}/{y}.png"
            />

            <DestinationMarkers />

            <RoutingMachine />

            <ZoomControl position="bottomright" />

            <GeoJSON data={canThoGeoJson as any} />
          </MapContainer>
        </div>

        <DestinationList map={map} />
      </div>
    </MapContextProvider>
  );
}
