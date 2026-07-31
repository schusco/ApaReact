import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import './components/site.css';
import { PlayerProvider } from './context/PlayerContext';
import 'bootstrap-icons/font/bootstrap-icons.css';

export function App() {
    return (
        <PlayerProvider>
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Layout>
        </PlayerProvider>
    );
}
