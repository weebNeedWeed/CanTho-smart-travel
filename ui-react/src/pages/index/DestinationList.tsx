import {
  Accordion,
  ActionIcon,
  rem,
  TextInput,
  TextInputProps,
} from "@mantine/core";
import { FaArrowRight, FaSearch } from "react-icons/fa";
import useDestCategories from "../../hooks/useDestCategories";

import { useEffect, useState } from "react";
import { defaultDestinationApiClient } from "../../helpers/DestinationApiClient";

export default function DestinationList() {
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
    <div className="z-20 absolute h-screen max-h-screen left-20 top-0 p-4 bg-white shadow-lg shadow-gray-400 flex flex-col">
      <SearchInput className="w-80 mb-4" />

      <div className="w-full h-full overflow-auto scrollbar">
        <Accordion defaultValue="1">
          {!isDestCatsError &&
            !isDestCatsLoading &&
            destCatsData!.data.map((cat: any) => (
              <Accordion.Item key={cat.id} value={cat.id.toString()}>
                <Accordion.Control>{cat.name}</Accordion.Control>
                <Accordion.Panel>
                  {destsMap.get(cat.id)?.map((dest: any) => (
                    <div key={dest.id}>{dest.name}</div>
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
}

function SearchInput(props: SearchInputProps) {
  return (
    <TextInput
      radius="xl"
      size="md"
      placeholder="Tìm địa điểm"
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
