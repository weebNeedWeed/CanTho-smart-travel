import { randomId, useDisclosure } from "@mantine/hooks";
import { NavbarLink } from "./NavBar";
import { FaPersonWalkingLuggage } from "react-icons/fa6";
import {
  ActionIcon,
  Breadcrumbs,
  Button,
  Card,
  Drawer,
  Group,
  NumberInput,
  rem,
  Text,
  TextInput,
  ThemeIcon,
} from "@mantine/core";
import useProfileItineraries from "../../hooks/useProfileItineraries";
import { FaWalking } from "react-icons/fa";
import { IoMdAdd, IoMdClose, IoMdTime } from "react-icons/io";
import { defaultProfileApiClient } from "../../helpers/ProfileApiClient";
import { DateInput, DateTimePicker } from "@mantine/dates";
import "@mantine/dates/styles.css";
import { DragDropContext, Draggable, Droppable } from "@hello-pangea/dnd";
import classes from "./DndListHandle.module.css";
import cx from "clsx";
import { LuGripVertical } from "react-icons/lu";
import { MdOutlineDeleteForever } from "react-icons/md";
import { useForm } from "@mantine/form";
import { useMapContext } from "../../contexts/MapContext";
import { useEffect } from "react";
import moment from "moment";

export default function ItinerariesButton() {
  const [opened, { open, close }] = useDisclosure(false);
  const {
    data: itiData,
    isError: isItiError,
    isLoading: isItiLoading,
    refetch: itiRefetch,
  } = useProfileItineraries();

  const handleCreate = () => {
    defaultProfileApiClient
      .createItinerary()
      .then(() => {
        itiRefetch();
      })
      .catch(console.log);
  };

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
              <Button color="blue" radius="md" onClick={handleCreate}>
                Thêm
              </Button>

              <div className="flex flex-col mt-4 gap-4">
                {!isItiError &&
                  !isItiLoading &&
                  itiData!.data.map((iti: any, i: number) => (
                    <ItineraryCard
                      itiRefetch={itiRefetch}
                      itinerary={iti}
                      key={iti.id}
                      index={i + 1}
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
  index: number;
}

function ItineraryCard({ itinerary, itiRefetch, index }: ItineraryCardProps) {
  const [opened, { open, close }] = useDisclosure(false);
  const handleDelete = () => {
    defaultProfileApiClient
      .deleteItinerary(itinerary.id)
      .then(() => {
        itiRefetch();
      })
      .catch(console.log);
  };
  return (
    <>
      <Card shadow="sm" radius="md" withBorder>
        <Text fw={700} size="lg" component={"span"}>
          #{index}: {itinerary.name}
        </Text>

        <Group justify="flex-start" gap={10} mt="xs">
          <Text component={"span"}>
            <ThemeIcon size="sm" color="gray" variant="outline">
              <FaWalking style={{ width: "70%", height: "70%" }} />
            </ThemeIcon>
          </Text>
          <Text component={"span"} c="dimmed" size="sm">
            <Breadcrumbs separator="→" separatorMargin="xs">
              {itinerary.itineraryItems.length === 0 && (
                <Text>{"Chưa có lộ trình"}</Text>
              )}
              {itinerary.itineraryItems.map((item: any) => (
                <Text key={item.id}>{item.destination.name}</Text>
              ))}
            </Breadcrumbs>
          </Text>
        </Group>

        <Group justify="flex-start" gap={10} mt="xs">
          <Text component={"span"}>
            <ThemeIcon size="sm" color="gray" variant="outline">
              <IoMdTime style={{ width: "70%", height: "70%" }} />
            </ThemeIcon>
          </Text>
          <Text component={"span"} c="dimmed" size="sm">
            {itinerary.startDate} đến {itinerary.endDate}
          </Text>
        </Group>

        <div className="w-full flex justify-end gap-x-1 mt-3">
          <Button variant="filled" onClick={open}>
            Chi tiết
          </Button>

          <Button variant="filled" color="pink" onClick={handleDelete}>
            Xóa
          </Button>
        </div>
      </Card>
      <Drawer.Root
        opened={opened}
        onClose={close}
        classNames={{ content: "scrollbar" }}
      >
        <Drawer.Content>
          <Drawer.Header>
            <Drawer.Title>Chi tiết lịch trình</Drawer.Title>
            <Drawer.CloseButton />
          </Drawer.Header>
          <Drawer.Body>
            <ItineraryDetails itiRefetch={itiRefetch} itinerary={itinerary} />
          </Drawer.Body>
        </Drawer.Content>
      </Drawer.Root>
    </>
  );
}

interface ItineraryDetailsProps {
  itinerary: any;
  itiRefetch: () => void;
}

function ItineraryDetails({ itinerary, itiRefetch }: ItineraryDetailsProps) {
  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      name: itinerary.name,
      totalCost: itinerary.totalCost,
      startDate: new Date(itinerary.startDate),
      endDate: new Date(itinerary.endDate),
      itineraryItems: itinerary.itineraryItems.map((v) => ({
        ...v,
        startTime: new Date(v.startTime),
        endTime: new Date(v.endTime),
      })),
    },
  });
  const {
    setMapSelecting,
    mapSelecting,
    selectedDestination,
    setItiDestinations,
  } = useMapContext();

  useEffect(() => {
    const index = form
      .getValues()
      .itineraryItems.findIndex((x) => x.id === mapSelecting);
    if (index === -1) return;

    const cloned = [...form.getValues().itineraryItems];
    cloned[index].destination.id = selectedDestination.id;
    cloned[index].destination.name = selectedDestination.name;
    cloned[index].destination.location = {
      coordinates: selectedDestination.location.coordinates,
    };

    form.setValues({
      itineraryItems: cloned,
    });

    setTimeout(() => {
      setItiDestinations(form.values.itineraryItems.map((x) => x.destination));
    }, 100);
  }, [selectedDestination]);

  useEffect(() => {
    setItiDestinations(form.values.itineraryItems.map((x) => x.destination));
  }, [form.values.itineraryItems]);

  const handleSubmit = (values: any) => {
    defaultProfileApiClient
      .updateItinerary(itinerary.id, {
        ...values,
        startDate: moment(values.startDate).format("YYYY-MM-DD"),
        endDate: moment(values.endDate).format("YYYY-MM-DD"),
        itineraryItems: values.itineraryItems.map((v, i: number) => ({
          ...v,
          destinationId: v.destination.id,
          priority: i + 1,
        })),
      })
      .then(() => {
        itiRefetch();
      })
      .catch(console.log);
  };

  return (
    <form onSubmit={form.onSubmit(handleSubmit)}>
      <TextInput
        withAsterisk
        label="Tên lịch trình"
        placeholder="Lịch trình 1"
        className="w-full"
        size="md"
        key={form.key("name")}
        {...form.getInputProps("name")}
      />

      <NumberInput
        suffix=" đồng"
        thousandSeparator=","
        min={0}
        label="Tổng chi phí dự kiến"
        placeholder="1,000"
        className="w-full"
        size="md"
        mt="md"
        key={form.key("totalCost")}
        {...form.getInputProps("totalCost")}
      />

      <div className="flex justify-start items-center gap-x-2">
        <DateInput
          withAsterisk
          valueFormat="DD/MM/YYYY"
          label="Ngày bắt đầu dự kiến"
          placeholder="1/11/2001"
          className="w-full"
          size="md"
          mt="md"
          key={form.key("startDate")}
          {...form.getInputProps("startDate")}
        />

        <DateInput
          valueFormat="DD/MM/YYYY"
          label="Ngày kết thúc dự kiến"
          placeholder="1/11/2001"
          className="w-full"
          size="md"
          mt="md"
          key={form.key("endDate")}
          {...form.getInputProps("endDate")}
        />
      </div>

      <div className="mt-4">
        <div className="flex mb-2 items-center justify-start gap-2">
          <Text fw={600} size="md">
            Lộ trình
          </Text>
          <ActionIcon
            variant="filled"
            onClick={() =>
              form.insertListItem("itineraryItems", {
                id: randomId(),
                destination: { id: -1, name: "" },
                startTime: new Date(),
                endTime: new Date(),
                notes: "",
              })
            }
          >
            <IoMdAdd style={{ width: "70%", height: "70%" }} />
          </ActionIcon>
        </div>

        <DragDropContext
          onDragEnd={({ destination, source }) =>
            form.reorderListItem("itineraryItems", {
              from: source.index,
              to: destination?.index || 0,
            })
          }
        >
          <Droppable droppableId="dnd-list" direction="vertical">
            {(provided) => (
              <div {...provided.droppableProps} ref={provided.innerRef}>
                {form.getValues().itineraryItems.map((item, index) => (
                  <Draggable
                    key={item.id}
                    index={index}
                    draggableId={item.id.toString()}
                  >
                    {(provided, snapshot) => {
                      if (snapshot.isDragging) {
                        provided.draggableProps.style!["left"] =
                          provided.draggableProps.style!["offsetLeft"];
                        provided.draggableProps.style!["top"] =
                          provided.draggableProps.style!["offsetTop"];
                      }
                      return (
                        <div
                          className={cx(classes.item, {
                            [classes.itemDragging]: snapshot.isDragging,
                          })}
                          ref={provided.innerRef}
                          {...provided.draggableProps}
                        >
                          <div
                            {...provided.dragHandleProps}
                            className={classes.dragHandle}
                          >
                            <LuGripVertical
                              style={{ width: rem(18), height: rem(36) }}
                            />
                          </div>
                          <div className="w-full">
                            <div className="flex gap-x-2 justify-start w-full">
                              <DateTimePicker
                                className="w-1/2"
                                label="Giờ bắt đầu"
                                placeholder="Chọn ngày giờ"
                                key={form.key(
                                  `itineraryItems.${index}.startTime`
                                )}
                                {...form.getInputProps(
                                  `itineraryItems.${index}.startTime`
                                )}
                              />

                              <DateTimePicker
                                className="w-1/2"
                                label="Giờ kết thúc"
                                placeholder="Chọn ngày giờ"
                                key={form.key(
                                  `itineraryItems.${index}.endTime`
                                )}
                                {...form.getInputProps(
                                  `itineraryItems.${index}.endTime`
                                )}
                              />
                            </div>
                            <input
                              type="hidden"
                              key={form.key(
                                `itineraryItems.${index}.destination.id`
                              )}
                              {...form.getInputProps(
                                `itineraryItems.${index}.destination.id`
                              )}
                            />
                            <TextInput
                              label="Địa điểm"
                              key={form.key(
                                `itineraryItems.${index}.destination.name`
                              )}
                              {...form.getInputProps(
                                `itineraryItems.${index}.destination.name`
                              )}
                              placeholder={
                                mapSelecting === item.id
                                  ? "Đang chọn địa điểm trên bản đồ"
                                  : "Click để chọn địa điểm trên bản đồ"
                              }
                              readOnly
                              onClick={() => setMapSelecting(item.id)}
                              rightSection={
                                mapSelecting === item.id && (
                                  <ActionIcon
                                    variant="subtle"
                                    aria-label="Settings"
                                    onClick={() => setMapSelecting("")}
                                  >
                                    <IoMdClose width="70%" height="70%" />
                                  </ActionIcon>
                                )
                              }
                            />

                            <TextInput
                              label="Ghi chú (nếu có)"
                              key={form.key(`itineraryItems.${index}.notes`)}
                              {...form.getInputProps(
                                `itineraryItems.${index}.notes`
                              )}
                            />
                          </div>
                          <ActionIcon
                            variant="filled"
                            aria-label="Settings"
                            color="pink"
                            ml="lg"
                            onClick={() =>
                              form.removeListItem("itineraryItems", index)
                            }
                          >
                            <MdOutlineDeleteForever
                              style={{ width: "70%", height: "70%" }}
                            />
                          </ActionIcon>
                        </div>
                      );
                    }}
                  </Draggable>
                ))}
                {provided.placeholder}
              </div>
            )}
          </Droppable>
        </DragDropContext>
      </div>

      <Button type="submit" mt="md" size="md" fullWidth>
        Lưu
      </Button>
    </form>
  );
}