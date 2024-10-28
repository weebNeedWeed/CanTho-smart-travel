import { Carousel } from "@mantine/carousel";
import { Card, Text, Button, Drawer, Image, Badge } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import "@mantine/carousel/styles.css";

interface DestinationCardProps {
  destination: any;
  map: L.Map | null;
}

export default function DestinationCard(props: DestinationCardProps) {
  const [opened, { open, close }] = useDisclosure(false);
  const openDrawer = () => {
    open();
    if (props.map) {
      props.map.flyTo(props.destination.location.coordinates, 16);
    }
  };

  return (
    <>
      <Card shadow="sm" padding="md" radius="md" withBorder className="mb-4">
        <Text mb="xs" fw={500}>
          {props.destination.name}
        </Text>

        <Text size="sm" c="dimmed" mb="xs" className="h-20" lineClamp={4}>
          {props.destination.description}
        </Text>

        <div className="w-full flex justify-end gap-x-1">
          <Button variant="filled" onClick={openDrawer}>
            Chi tiết
          </Button>

          <Button variant="filled" color="pink">
            Thêm
          </Button>
        </div>
      </Card>

      <Drawer
        position="left"
        opened={opened}
        onClose={close}
        title={"Thông tin địa điểm"}
        overlayProps={{ backgroundOpacity: 0.1 }}
        classNames={{ content: "scrollbar" }}
      >
        <Card padding="xs" radius="xs" className="w-full">
          <Card.Section>
            <Carousel withIndicators height={200}>
              {props.destination.photos.map((p: any, i: number) => (
                <Carousel.Slide key={i}>
                  <Image src={import.meta.env.VITE_IMAGE_BASE_URL + p} />
                </Carousel.Slide>
              ))}
            </Carousel>
          </Card.Section>

          <Badge color="blue" mt="lg">
            {props.destination.destinationCategory.name}
          </Badge>

          <Text size="lg" fw={700} mb="xs">
            {props.destination.name}
          </Text>

          <div className="flex flex-col gap-1">
            <Text size="md" fw={500}>
              Giới thiệu
            </Text>
            <Text size="sm" c="dark" className="text-justify">
              {props.destination.description}
            </Text>
          </div>

          <div className="flex flex-col gap-1 mt-3">
            <Text size="md" fw={500}>
              Địa chỉ
            </Text>
            <Text size="sm" c="dark" className="text-justify">
              {props.destination.address}
              {" / "}
              {getTypeNameOfCommuneWard(props.destination.communeWard.type) +
                " " +
                props.destination.communeWard.name}
              {" / "}
              {getTypeNameOfDistrictCounty(
                props.destination.communeWard.districtCounty.type
              ) +
                " " +
                props.destination.communeWard.districtCounty.name}
            </Text>
          </div>

          <div className="flex flex-col gap-1 mt-3">
            <Text size="md" fw={500}>
              Giờ mở cửa
            </Text>
            <Text size="sm" c="dark" className="text-justify">
              {Object.keys(props.destination.openingHours).map((k, i) => (
                <div key={i}>
                  {k}: {props.destination.openingHours[k]}
                </div>
              ))}
            </Text>
          </div>

          <div className="flex flex-col gap-1 mt-3">
            <Text size="md" fw={500}>
              Tiện nghi
            </Text>
            <div className="flex flex-row flex-wrap gap-x-1.5 gap-y-1">
              {props.destination.amenities.map((a: any, i: number) => (
                <Badge color="gray" key={i}>
                  {a}
                </Badge>
              ))}
            </div>
          </div>

          <div className="flex flex-col gap-1 mt-3">
            <Text size="md" fw={500}>
              Liên hệ
            </Text>
            <Text size="sm" c="dark" className="text-justify">
              Email: {props.destination.email}
            </Text>
            <Text size="sm" c="dark" className="text-justify">
              Số điện thoại: {props.destination.phoneNumber}
            </Text>
          </div>

          <div className="flex flex-row flex-wrap font-thin gap-1 mt-3">
            {props.destination.tags.map((t: any, i: number) => (
              <span className="text-sm underline" key={i}>
                #{t}
              </span>
            ))}
          </div>
        </Card>
      </Drawer>
    </>
  );
}

function getTypeNameOfCommuneWard(typeNum: number): string {
  switch (typeNum) {
    case 0:
      return "xã";
    case 1:
      return "phường";
    case 3:
    default:
      return "thị trấn";
  }
}

function getTypeNameOfDistrictCounty(typeNum: number): string {
  switch (typeNum) {
    case 0:
      return "quận";
    case 1:
    default:
      return "huyện";
  }
}
