import {
  Anchor,
  Button,
  Container,
  Paper,
  PasswordInput,
  TextInput,
  Title,
  Text,
} from "@mantine/core";
import { FormEvent, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { defaultAuthApiClient } from "../../helpers/AuthApiClient";

export default function RegisterPage() {
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!username || !password || !confirmPassword) {
      return;
    }

    if (password !== confirmPassword) {
      return;
    }

    try {
      const result = await defaultAuthApiClient.register(username, password);
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
        {"Bạn đã có tài khoản? "}
        <Anchor to="/auth/login" size="sm" component={Link}>
          Đăng nhập
        </Anchor>
      </Text>

      <Paper
        onSubmit={handleSubmit}
        component="form"
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
        <PasswordInput
          label="Nhập lại mật khẩu"
          placeholder="#Abc123"
          required
          mt="md"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
        <Button type="submit" fullWidth mt="xl">
          Đăng ký
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
