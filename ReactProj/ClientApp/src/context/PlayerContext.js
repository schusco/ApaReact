import React, { createContext, useState, useEffect, useContext } from 'react';

export const PlayerContext = createContext();

export function PlayerProvider({ children }) {
    const [user, setUser] = useState(() => {
        const storedUser = sessionStorage.getItem('user');
        return storedUser ? JSON.parse(storedUser) : null;
    });
    const [playerList, setPlayerList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    // Fetch once when the app boots up
    useEffect(() => {
        fetch('/api/players')
            .then(res => res.json())
            .then(data => {
                setPlayerList(data);
                setIsLoading(false);;
            })
            .catch(err => {
                console.error('Error fetching players:', err);
                setIsLoading(false);
            });
    }, []);

    const login = async (playerNumber, password, hasPassword) => {
        const loginResponse = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ playerNumber, password, hasPassword })
        });
        if (!loginResponse.ok) {
            throw new Error('Login failed');
        }
        const result = await loginResponse.json();
        sessionStorage.setItem('user', JSON.stringify(result));
        setUser(result);
    }

    const logout = () => {
        setUser(null);
        sessionStorage.removeItem('user');
    }

    return (
        <PlayerContext.Provider value={{ playerList, setPlayerList, isLoading, user, setUser, login, logout }}>
            {children}
        </PlayerContext.Provider>
    );
}

// Custom hook so components can easily grab the list or the updater
export function usePlayers() {
    return useContext(PlayerContext);
}