import {
  Anchor,
  Button,
  Checkbox,
  Container,
  Group,
  Paper,
  PasswordInput,
  TextInput,
  Title,
  Text,
} from "@mantine/core";
import { FormEvent, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { defaultAuthApiClient } from "../../helpers/AuthApiClient";

export default function LoginPage() {
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!username || !password) {
      return;
    }

    try {
      const result = await defaultAuthApiClient.login(username, password);
      const token = result.data.token;
      localStorage.setItem("access_token", token);
      navigate("/");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Container size={420} my={40}>
      <Title ta="center">{"Chào bạn!"}</Title>
      <Text c="dimmed" size="sm" ta="center" mt={5}>
        {"Bạn chưa có tài khoản? "}
        <Anchor to="/auth/register" size="sm" component={Link}>
          Tạo tài khoản
        </Anchor>
      </Text>

      <Paper
        component="form"
        onSubmit={handleSubmit}
        withBorder
        shadow="md"
        p={30}
        mt={30}
        radius="md"
      >
        <TextInput
          label="Username"
          placeholder="myusername"
          required
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <PasswordInput
          label="Mật khẩu"
          placeholder="#Abc123"
          required
          mt="md"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Group justify="space-between" mt="lg">
          <Checkbox label="Nhớ mật khẩu" />
          <Anchor component="button" size="sm">
            {"Quên mật khẩu"}
          </Anchor>
        </Group>
        <Button type="submit" fullWidth mt="xl">
          Đăng nhập
        </Button>
        <Text c="dimmed" size="sm" ta="center" mt={5}>
          {"Hoặc "}
          <Anchor to="/" size="sm" component={Link}>
            về trang chủ
          </Anchor>
        </Text>
      </Paper>
    </Container>
  );
}
