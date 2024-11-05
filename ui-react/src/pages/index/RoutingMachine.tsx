import { Polyline } from "react-leaflet";
import { defaultRoutingApiClient } from "../../helpers/RoutingApiClient";
import { useMapContext } from "../../contexts/MapContext";
import { useEffect, useState } from "react";
import { LatLng } from "leaflet";
import { getRandomColor, getRoute } from "../../helpers/commonFn";

export default function RoutingMachine() {
  const { itiDestinations, setVietnameseRoutes, currentPolyline } =
    useMapContext();
  const [myPosition, setMyPosition] = useState<LatLng | null>(null);

  useEffect(() => {
    navigator.geolocation.getCurrentPosition((pos) => {
      const { latitude, longitude } = pos.coords;
      setMyPosition(new LatLng(latitude, longitude));
    });
  }, []);

  useEffect(() => {
    if (myPosition === null) return;

    (async () => {
      const routes: any[] = [];
      const handleData = (i: number, dests: any, data: any) => {
        const ls: LatLng[] = data.data.routes[0].geometry.coordinates.map(
          (x) => {
            return new LatLng(x[1], x[0]);
          }
        );

        if (i === 0) {
          routes.push({
            title: "Từ vị trí hiện tại đến " + dests[i].name,
            ...getRoute(data.data),
            polyline: ls,
          });
        } else {
          routes.push({
            title: `Từ ${dests[i - 1].name} đến ${dests[i].name}`,
            ...getRoute(data.data),
            polyline: ls,
          });
        }
      };
      for (let i = 0; i < itiDestinations.length; i++) {
        if (!itiDestinations[i].location) {
          continue;
        }

        if (i === 0) {
          handleData(
            i,
            itiDestinations,
            await defaultRoutingApiClient.searchDestination(
              [myPosition.lat, myPosition.lng],
              itiDestinations[i].location.coordinates
            )
          );

          continue;
        }

        if (!itiDestinations[i - 1].location) {
          continue;
        }

        handleData(
          i,
          itiDestinations,
          await defaultRoutingApiClient.searchDestination(
            itiDestinations[i - 1].location.coordinates,
            itiDestinations[i].location.coordinates
          )
        );
      }

      setVietnameseRoutes(routes);
    })();
  }, [itiDestinations, myPosition]);
  return (
    <>
      {currentPolyline && (
        <Polyline
          positions={currentPolyline}
          pathOptions={{
            color: getRandomColor(),
            dashArray: "40 20 10 20",
            weight: 6,
          }}
        />
      )}
    </>
  );
}
