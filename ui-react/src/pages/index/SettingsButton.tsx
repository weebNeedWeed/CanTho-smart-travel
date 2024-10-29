import { Drawer, NumberInput, TagsInput, Button } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useState, useEffect, FormEvent } from "react";
import { AiOutlineSetting } from "react-icons/ai";
import { useNavigate } from "react-router-dom";
import { defaultProfileApiClient } from "../../helpers/ProfileApiClient";
import useProfileSettings from "../../hooks/useProfileSettings";
import { NavbarLink } from "./NavBar";

export default function SettingsButton() {
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
