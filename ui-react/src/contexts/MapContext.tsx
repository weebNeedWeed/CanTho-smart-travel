import { LatLng } from "leaflet";
import { createContext, useContext, useState } from "react";

interface MapContextType {
  mapSelecting: string;
  selectedDestination: any;
  itiDestinations: any[];
  vietnameseRoutes: any;
  map: any;
  currentLocation: LatLng | null;
  currentPolyline: LatLng[] | null;
  setCurrentPolyline: (v: LatLng[] | null) => void;
  setCurrentLocation: (v: LatLng) => void;
  setMap: (m: any) => void;
  setMapSelecting: (string) => void;
  setSelectedDestination: (dest: any) => void;
  setItiDestinations: (a: any[]) => void;
  setVietnameseRoutes: (route: any) => void;
}

const MapContext = createContext<MapContextType | null>(null);

export const useMapContext = () => {
  const mapContext = useContext(MapContext);

  if (!mapContext) {
    throw new Error(
      "useMapContext has to be used within <MapContext.Provider>"
    );
  }

  return mapContext;
};

export default function MapContextProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [mapSelecting, setMapSelecting] = useState<string>("");
  const [selectedDestination, setSelectedDestination] = useState<any>({
    id: "",
    name: "",
  });
  const [itiDestinations, setItiDestinations] = useState<any[]>([]);
  const [vietnameseRoutes, setVietnameseRoutes] = useState<any[]>([]);
  const [map, setMap] = useState<any>(null);
  const [currentLocation, setCurrentLocation] = useState<LatLng | null>(null);
  const [currentPolyline, setCurrentPolyline] = useState<LatLng[] | null>(null);

  return (
    <MapContext.Provider
      value={{
        mapSelecting,
        setMapSelecting,
        selectedDestination,
        setSelectedDestination,
        itiDestinations,
        setItiDestinations,
        vietnameseRoutes,
        setVietnameseRoutes,
        map,
        setMap,
        currentLocation,
        setCurrentLocation,
        currentPolyline,
        setCurrentPolyline,
      }}
    >
      {children}
    </MapContext.Provider>
  );
}
