import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";
import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import HomePage from "./pages/index/HomePage";

const router = createBrowserRouter([
  {
    path: "/auth",
    children: [
      {
        index: true,
        element: <Navigate to="/auth/login" />,
      },
      {
        path: "login",
        element: <LoginPage />,
      },
      {
        path: "register",
        element: <RegisterPage />,
      },
    ],
  },
  {
    path: "/",
    element: <HomePage />,
  },
]);

export default function App() {
  return (
    <MantineProvider defaultColorScheme="light">
      <RouterProvider router={router} />
    </MantineProvider>
  );
}
