import { useDisclosure } from "@mantine/hooks";
import { NavbarLink } from "./NavBar";
import { FaPersonWalkingLuggage } from "react-icons/fa6";
import {
  Breadcrumbs,
  Button,
  Card,
  Drawer,
  Group,
  Text,
  ThemeIcon,
} from "@mantine/core";
import useProfileItineraries from "../../hooks/useProfileItineraries";
import { FaWalking } from "react-icons/fa";
import { IoMdTime } from "react-icons/io";
import { defaultProfileApiClient } from "../../helpers/ProfileApiClient";

export default function ItinerariesButton() {
  const [opened, { open, close }] = useDisclosure(false);
  const {
    data: itiData,
    isError: isItiError,
    isLoading: isItiLoading,
    refetch: itiRefetch,
  } = useProfileItineraries();
  return (
    <>
      <NavbarLink
        icon={FaPersonWalkingLuggage}
        label={"Lịch trình"}
        onClick={open}
      />

      <Drawer.Root
        opened={opened}
        onClose={close}
        classNames={{ content: "scrollbar" }}
      >
        <Drawer.Content>
          <Drawer.Header>
            <Drawer.Title>Lịch trình</Drawer.Title>
            <Drawer.CloseButton />
          </Drawer.Header>
          <Drawer.Body>
            <Card>
              <Button color="blue" radius="md">
                Thêm
              </Button>

              <div className="flex flex-col mt-4 gap-4">
                {!isItiError &&
                  !isItiLoading &&
                  itiData!.data.map((iti: any) => (
                    <ItineraryCard
                      itiRefetch={itiRefetch}
                      itinerary={iti}
                      key={iti.id}
                    />
                  ))}
              </div>
            </Card>
          </Drawer.Body>
        </Drawer.Content>
      </Drawer.Root>
    </>
  );
}

interface ItineraryCardProps {
  itinerary: any;
  itiRefetch: () => void;
}

function ItineraryCard({ itinerary, itiRefetch }: ItineraryCardProps) {
  const handleDelete = () => {
    defaultProfileApiClient
      .deleteItinerary(itinerary.id)
      .then(() => {
        itiRefetch();
      })
      .catch(console.log);
  };
  return (
    <Card shadow="sm" radius="md" withBorder>
      <Text fw={700} size="lg">
        #{itinerary.id}: {itinerary.name}
      </Text>

      <Group justify="flex-start" gap={10} mt="xs">
        <Text>
          <ThemeIcon size="sm" color="gray" variant="outline">
            <FaWalking style={{ width: "70%", height: "70%" }} />
          </ThemeIcon>
        </Text>
        <Text c="dimmed" size="sm">
          <Breadcrumbs separator="→" separatorMargin="xs">
            {itinerary.itineraryItems.map((item: any) => (
              <Text key={item.id}>{item.destination.name}</Text>
            ))}
          </Breadcrumbs>
        </Text>
      </Group>

      <Group justify="flex-start" gap={10} mt="xs">
        <Text>
          <ThemeIcon size="sm" color="gray" variant="outline">
            <IoMdTime style={{ width: "70%", height: "70%" }} />
          </ThemeIcon>
        </Text>
        <Text c="dimmed" size="sm">
          {itinerary.startDate} đến {itinerary.endDate}
        </Text>
      </Group>

      <div className="w-full flex justify-end gap-x-1 mt-3">
        <Button variant="filled">Chi tiết</Button>

        <Button variant="filled" color="pink" onClick={handleDelete}>
          Xóa
        </Button>
      </div>
    </Card>
  );
}
