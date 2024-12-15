import React, { createContext, useEffect, useState } from "react";
import webSocketService from "../services/WebSocketService";

const WebSocketContext = createContext();

export const WebSocketProvider = ({ children }) => {
    const [webSocketStatus, setWebSocketStatus] = useState("disconnected");
    const [gameId, setGameId] = useState(null); // Memorizza l'ID della partita in corso
    const [gameStarted, setGameStarted] = useState(false); // Memorizza l'ID della partita in corso
    const [isWaitingForOpponent, setIsWaitingForOpponent] = useState(false);



    // START NEW GAME - connessione + inizio nuova partita
    const startNewGame = async () => {
        const connessioneAperta = await connessione();
        if (!connessioneAperta)
            return false;
        sendMessage({ action: "find_match" });

    };

    // Funzione per inviare messaggi tramite WebSocket
    const sendMessage = (data) => {
        webSocketService.send(data);
    };

    const connessione = async () => {
        console.log("2) Start connessione web socket");
        if (webSocketStatus === "disconnected") {
            try {
                const result = await webSocketService.connect("wss://localhost:7225/ws");
                // Verifica se la connessione è avvenuta con successo
                if (result && result.success) {
                    setWebSocketStatus("connected");
                    //console.log('setStatus("connected");');
                    return true; // Connessione riuscita
                } else {
                    //console.error("WebSocket connection failed");
                    setWebSocketStatus("disconnected");
                    return false; // Connessione fallita
                }
            } catch (error) {
                console.error("Error during WebSocket connection:", error);
                setWebSocketStatus("disconnected");
                return false; // Connessione fallita
            }
        }

        return false; // Connessione non necessaria se già connessi
    }

    useEffect(() => {
        //ascolto dei messaggi dal server
        const handleMessage = (message) => {

            switch (message.action) {
                case "connected":

                    break;
                case "match_found":
                    setGameId(message.data.gameId);
                    setGameStarted(true);
                    break;
                case "waiting_opponent":
                    setIsWaitingForOpponent(true);
                    break;

                case "player_disconnected":
                    console.warn("Player disconnected:", message.data.playerId);
                    break;

                case "error":
                    console.error("Error from server:", message.data);
                    break;

                default:
                    console.log("Unknown action:", message.action);
                    break;
            }

            // Puoi gestire altre azioni qui
        };

        webSocketService.addListener(handleMessage);
        return () => {
            webSocketService.removeListener(handleMessage);
        };

    }, []); // Dipendenze necessarie

    //dati accessibili nel contesto da altre parti del codice
    const webContextData = {
        isWaitingForOpponent,
        webSocketStatus,
        startNewGame,
        gameId,
        gameStarted,
    };

    return (
        <WebSocketContext.Provider value={{ webContextData }}>
            {children}
        </WebSocketContext.Provider>
    );
};

export default WebSocketContext;
