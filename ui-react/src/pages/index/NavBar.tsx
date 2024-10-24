import { Center, Tooltip, UnstyledButton, Stack, rem } from "@mantine/core";

import { AiOutlineHome, AiOutlineLogout } from "react-icons/ai";
import { GiPositionMarker } from "react-icons/gi";
import classes from "./HomePage.module.css";
import { LatLng } from "leaflet";
import { useEffect, useState } from "react";

interface NavbarLinkProps {
  icon: typeof AiOutlineHome;
  label: string;
  active?: boolean;
  onClick?(): void;
}

function NavbarLink({ icon: Icon, label, active, onClick }: NavbarLinkProps) {
  return (
    <Tooltip label={label} position="right" transitionProps={{ duration: 0 }}>
      <UnstyledButton
        onClick={onClick}
        className={classes.link}
        data-active={active || undefined}
      >
        <Icon style={{ width: rem(20), height: rem(20) }} stroke={"1.5"} />
      </UnstyledButton>
    </Tooltip>
  );
}

interface NavbarProps {
  map?: L.Map;
}

export default function NavBar(props: NavbarProps) {
  const [position, setPosition] = useState<LatLng | null>(null);
  useEffect(() => {
    navigator.geolocation.getCurrentPosition((pos) => {
      const { latitude, longitude } = pos.coords;
      setPosition(new LatLng(latitude, longitude));
    });
  }, []);
  return (
    <nav className={classes.navbar}>
      <Center>
        <span className="font-bold text-lg text-sky-700">CTSM</span>
      </Center>

      <div className={classes.navbarMain}>
        <Stack justify="center" gap={0}>
          <NavbarLink
            icon={AiOutlineHome}
            label={"Home"}
            // active={true}
            // onClick={() => console.log(1)}
          />
          {props.map && (
            <NavbarLink
              icon={GiPositionMarker}
              label={"Vị trí hiện tại"}
              onClick={() => props.map!.flyTo(position!, 17)}
            />
          )}
        </Stack>
      </div>

      <Stack justify="center" gap={0}>
        <NavbarLink icon={AiOutlineLogout} label="Logout" />
      </Stack>
    </nav>
  );
}
