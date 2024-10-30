import { createContext, useContext, useState } from "react";

interface MapContextType {
  mapSelecting: string;
  selectedDestination: any;
  itiDestinations: any[];
  setMapSelecting: (string) => void;
  setSelectedDestination: (dest: any) => void;
  setItiDestinations: (a: any[]) => void;
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

  return (
    <MapContext.Provider
      value={{
        mapSelecting,
        setMapSelecting,
        selectedDestination,
        setSelectedDestination,
        itiDestinations,
        setItiDestinations,
      }}
    >
      {children}
    </MapContext.Provider>
  );
}
