import {
  Center,
  Tooltip,
  UnstyledButton,
  Stack,
  rem,
  Drawer,
  NumberInput,
  TagsInput,
  Button,
} from "@mantine/core";

import {
  AiOutlineHome,
  AiOutlineLogout,
  AiOutlineSetting,
} from "react-icons/ai";
import { GiPositionMarker } from "react-icons/gi";
import classes from "./HomePage.module.css";
import { LatLng } from "leaflet";
import { FormEvent, useEffect, useState } from "react";
import { useDisclosure } from "@mantine/hooks";
import useProfileSettings from "../../hooks/useProfileSettings";
import { useNavigate } from "react-router-dom";
import { defaultProfileApiClient } from "../../helpers/ProfileApiClient";

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
        <span className="font-bold text-lg text-sky-700">CTST</span>
      </Center>

      <div className={classes.navbarMain}>
        <Stack justify="center" gap={0}>
          {props.map && (
            <NavbarLink
              icon={GiPositionMarker}
              label={"Vị trí hiện tại"}
              onClick={() => props.map!.flyTo(position!, 17)}
            />
          )}

          <SettingsButton />
        </Stack>
      </div>

      <Stack justify="center" gap={0}>
        <NavbarLink icon={AiOutlineLogout} label="Logout" />
      </Stack>
    </nav>
  );
}

function SettingsButton() {
  const navigate = useNavigate();
  const [opened, { open, close }] = useDisclosure(false);
  const { isError, isLoading, data } = useProfileSettings();

  const [budgetMin, setBudgetMin] = useState<string | number>(0);
  const [budgetMax, setBudgetMax] = useState<string | number>(0);
  const [tags, setTags] = useState<string[]>([]);

  useEffect(() => {
    if (isError) {
      navigate("/auth/login");
      return;
    }

    if (isLoading || !data) {
      return;
    }

    const pref = data.data;

    setBudgetMin(pref.budgetMin);
    setBudgetMax(pref.budgetMax);
    setTags(pref.preferenceTags);
  }, [isError, isLoading, data]);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      await defaultProfileApiClient.saveSettings({
        budgetMax,
        budgetMin,
        tags,
      });
      close();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <NavbarLink icon={AiOutlineSetting} label={"Cài đặt"} onClick={open} />

      <Drawer
        opened={opened}
        onClose={close}
        title={"Cài đặt"}
        overlayProps={{ backgroundOpacity: 0.1 }}
        classNames={{ content: "scrollbar" }}
      >
        <form onSubmit={handleSubmit}>
          <div className="flex justify-between gap-x-4">
            <NumberInput
              withAsterisk
              label="Quỹ tối thiểu"
              placeholder="0"
              className="w-full"
              thousandSeparator=","
              min={1_000}
              defaultValue={1_000}
              size="md"
              suffix=" đồng"
              value={budgetMin}
              onChange={setBudgetMin}
            />

            <NumberInput
              withAsterisk
              suffix=" đồng"
              thousandSeparator=","
              min={1_000}
              defaultValue={1_000}
              label="Quỹ tối đa"
              placeholder="0"
              className="w-full"
              size="md"
              value={budgetMax}
              onChange={setBudgetMax}
            />
          </div>

          <TagsInput
            label="Sở thích (ấn Enter để thêm)"
            placeholder="câu cá"
            size="md"
            mt="md"
            value={tags}
            onChange={setTags}
            data={[]}
          />

          <Button type="submit" mt="md" size="md" fullWidth>
            Lưu
          </Button>
        </form>
      </Drawer>
    </>
  );
}
