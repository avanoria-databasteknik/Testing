import { createBrowserRouter } from "react-router-dom";
import AppLayout from "./components/layouts/AppLayout";
import AdminLayout from "./components/layouts/AdminLayout";
import AdminDashboard from "./components/pages/admin/AdminDashboard";
import BlankLayout from "./components/layouts/BlankLayout";
import NotFoundPage from "./components/pages/NotFoundPage";
import HomePage from "./components/pages/HomePage";

export const router = createBrowserRouter([
    {
        element: <AppLayout />,
        children: [
            {
                path: "/",
                handle: { title: "Course Online" },
                element: <HomePage />
            }
        ]
    },
    {
        element: <AdminLayout />,
        path: "/admin",
        children: [
            {
                index: true,
                handle: { title: "Admin Center" },
                element: <AdminDashboard />
            }
        ]
    },
    {
        element: <BlankLayout />,
        children: [
            {
                path: "*",
                handle: { title: "Page Not Found" },
                element: <NotFoundPage />
            },
        ]
    },
]);