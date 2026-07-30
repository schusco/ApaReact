import { Players } from "./components/Players";
import APA8 from "./components/Apa8";
import Stats from "./components/Stats";
import APA from "./components/Apa";
import Login from "./components/Login";
import Settings from "./components/Settings";

const AppRoutes = [
    {
        index: true,
        element: <APA />
    },
    {
        path: '/players',
        element: <Players />
    },   
    {
        path: '/stats',
        element: <Stats />
    }, {
        path: '/login',
        element: <Login />
    }, {
        path: '/settings',
        element: <Settings />
    }
];

export default AppRoutes;
