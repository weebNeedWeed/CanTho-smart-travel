import { Center, Tooltip, UnstyledButton, Stack, rem } from "@mantine/core";

import { AiOutlineHome, AiOutlineLogout } from "react-icons/ai";
import classes from "./HomePage.module.css";

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

export default function NavBar() {
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
            active={true}
            onClick={() => console.log(1)}
          />
        </Stack>
      </div>

      <Stack justify="center" gap={0}>
        <NavbarLink icon={AiOutlineLogout} label="Logout" />
      </Stack>
    </nav>
  );
}
