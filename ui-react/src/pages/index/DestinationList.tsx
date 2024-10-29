import {
  Accordion,
  ActionIcon,
  Drawer,
  Loader,
  rem,
  TextInput,
  TextInputProps,
} from "@mantine/core";
import { FaArrowRight, FaSearch } from "react-icons/fa";
import useDestCategories from "../../hooks/useDestCategories";

import { useEffect, useState } from "react";
import { defaultDestinationApiClient } from "../../helpers/DestinationApiClient";
import DestinationCard from "./DestinationCard";
import { defaultSearchApiClient } from "../../helpers/SearchApiClient";
import { useDisclosure } from "@mantine/hooks";

interface DestinationListProps {
  map: L.Map | null;
}

export default function DestinationList({ map }: DestinationListProps) {
  const {
    data: destCatsData,
    isError: isDestCatsError,
    isLoading: isDestCatsLoading,
  } = useDestCategories();

  const [destsMap, setDestsMap] = useState(new Map<any, any>());

  useEffect(() => {
    if (!isDestCatsError && !isDestCatsLoading) return;
    defaultDestinationApiClient
      .getAllDests()
      .then((res) => {
        const data = res.data;
        const newMap = new Map<any, any>();

        data.forEach((dest: any) => {
          if (!newMap.get(dest.destinationCategoryId)) {
            newMap.set(dest.destinationCategoryId, []);
          }
          newMap.get(dest.destinationCategoryId).push(dest);
        });

        setDestsMap(newMap);
      })
      .catch(console.log);
  }, [destCatsData, isDestCatsError, isDestCatsLoading]);

  return (
    <div className="z-20 absolute w-96 max-w-96 h-screen max-h-screen left-20 top-0 p-4 bg-white shadow-lg shadow-gray-400 flex flex-col">
      <SearchInput map={map} className="w-full mb-4" />

      <div className="w-full h-full overflow-auto scrollbar">
        <Accordion defaultValue="1">
          {!isDestCatsError &&
            !isDestCatsLoading &&
            destCatsData!.data.map((cat: any) => (
              <Accordion.Item key={cat.id} value={cat.id.toString()}>
                <Accordion.Control>{cat.name}</Accordion.Control>
                <Accordion.Panel>
                  {destsMap.get(cat.id)?.map((dest: any) => (
                    <DestinationCard
                      key={dest.id}
                      destination={dest}
                      map={map}
                    />
                  ))}
                </Accordion.Panel>
              </Accordion.Item>
            ))}
        </Accordion>
      </div>
    </div>
  );
}

interface SearchInputProps extends TextInputProps {
  onSearch?: (value: string) => void;
  map: L.Map | null;
}

function SearchInput(props: SearchInputProps) {
  const [opened, { open, close }] = useDisclosure(false);
  const [keyword, setKeyword] = useState("");
  const [destinations, setDestinations] = useState<any | null>(null);
  const [searching, setSearching] = useState(false);

  const handleSearch = async () => {
    if (keyword === "") {
      return;
    }

    setSearching(true);

    try {
      const res = await defaultSearchApiClient.searchDestination(keyword);
      const ids = res.data;

      defaultDestinationApiClient
        .getAllDests(ids)
        .then(({ data }) => {
          setDestinations(data);
          open();
        })
        .catch(console.log);
    } catch (err) {
      console.log(err);
    }

    setSearching(false);
  };

  return (
    <>
      <TextInput
        radius="xl"
        size="md"
        placeholder="Tìm địa điểm"
        value={keyword}
        onChange={(e) => setKeyword(e.target.value)}
        rightSectionWidth={42}
        leftSection={<FaSearch style={{ width: rem(18), height: rem(18) }} />}
        rightSection={
          <ActionIcon
            size={32}
            radius="xl"
            variant="filled"
            onClick={handleSearch}
            disabled={searching}
          >
            {searching ? (
              <Loader color="blue" size="sm" />
            ) : (
              <FaArrowRight style={{ width: rem(18), height: rem(18) }} />
            )}
          </ActionIcon>
        }
        {...props}
      />

      <Drawer
        opened={opened}
        onClose={close}
        title={"Từ khóa: " + keyword}
        overlayProps={{ backgroundOpacity: 0.1 }}
        classNames={{ content: "scrollbar" }}
      >
        {destinations?.map((dest: any) => (
          <DestinationCard key={dest.id} destination={dest} map={props.map} />
        ))}
      </Drawer>
    </>
  );
}
