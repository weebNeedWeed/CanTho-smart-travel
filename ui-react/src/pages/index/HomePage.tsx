import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";

import "leaflet.awesome-markers";
import "leaflet.awesome-markers/dist/leaflet.awesome-markers.css";
import NavBar from "./NavBar";
import { ActionIcon, rem, TextInput, TextInputProps } from "@mantine/core";
import { FaArrowRight, FaSearch } from "react-icons/fa";

export default function HomePage() {
  return (
    <div className="h-screen w-screen fixed">
      <NavBar />

      <div className="absolute w-full h-full top-0 left-0 z-10">
        <MapContainer
          center={[10.029965384684171, 105.77084060032108]}
          zoom={16}
          scrollWheelZoom={true}
          style={{ height: "100vh" }}
        >
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />
          <Marker position={[10.029965384684171, 105.77084060032108]}>
            <Popup>
              A pretty CSS3 popup. <br /> Easily customizable.
            </Popup>
          </Marker>
        </MapContainer>
      </div>

      <div className="z-20 absolute left-20 top-0 p-4">
        <SearchInput className="relative z-10" />

        <div className="z-0 absolute h-screen w-full bg-white top-0 left-0  shadow-lg shadow-gray-400"></div>
      </div>
    </div>
  );
}

interface SearchInputProps extends TextInputProps {
  onSearch?: (value: string) => void;
}

function SearchInput(props: SearchInputProps) {
  return (
    <TextInput
      radius="xl"
      size="md"
      placeholder="Search questions"
      rightSectionWidth={42}
      leftSection={<FaSearch style={{ width: rem(18), height: rem(18) }} />}
      rightSection={
        <ActionIcon size={32} radius="xl" variant="filled">
          <FaArrowRight style={{ width: rem(18), height: rem(18) }} />
        </ActionIcon>
      }
      {...props}
    />
  );
}
