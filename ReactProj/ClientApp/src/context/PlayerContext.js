import React, { createContext, useState, useEffect, useContext } from 'react';

const PlayerContext = createContext();

export function PlayerProvider({ children }) {
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

    return (
        <PlayerContext.Provider value={{ playerList, setPlayerList, isLoading }}>
            {children}
        </PlayerContext.Provider>
    );
}

// Custom hook so components can easily grab the list or the updater
export function usePlayers() {
    return useContext(PlayerContext);
}