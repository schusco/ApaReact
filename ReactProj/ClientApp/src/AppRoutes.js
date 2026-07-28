import { Players } from "./components/Players";
import APA8 from "./components/Apa8";
import Stats from "./components/Stats";
import APA from "./components/Apa";
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
        path: '/apa8',
        element: <APA8 />
    },
    {
        path: '/stats',
        element: <Stats />
    }    
];

export default AppRoutes;
