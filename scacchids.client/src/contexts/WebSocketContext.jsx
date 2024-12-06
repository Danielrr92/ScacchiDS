import React, { createContext, useEffect, useState } from "react";
import webSocketService from "../services/WebSocketService";

const WebSocketContext = createContext();

export const WebSocketProvider = ({ children }) => {
    const [status, setStatus] = useState("disconnected");
    const [gameId, setGameId] = useState(null); // Memorizza l'ID della partita in corso

    // Funzione per connettersi al WebSocket
    const connectWebSocket = async () => {
        if (status === "disconnected") {
            try {
                const result = await webSocketService.connect("wss://localhost:7225/ws");
                console.log(result);

                // Verifica se la connessione è avvenuta con successo
                if (result && result.success) {
                    setStatus("connected");
                    console.log('setStatus("connected");');
                    return true; // Connessione riuscita
                } else {
                    console.error("WebSocket connection failed");
                    setStatus("disconnected");
                    return false; // Connessione fallita
                }
            } catch (error) {
                console.error("Error during WebSocket connection:", error);
                setStatus("disconnected");
                return false; // Connessione fallita
            }
        }

        return false; // Connessione non necessaria se già connessi
    };

    // Funzione per gestire gli eventi di WebSocket
    const handleEvent = (message) => {
        if (typeof message === "string") {
            setStatus(message); // Aggiorna lo stato (e.g., "connected", "disconnected")
        } else {
            console.log("Message in context:", message); // Gestisci messaggi specifici se necessario
        }
    };

    //useEffect(() => {
    //    // Verifica se è stato salvato uno stato precedente di connessione
    //    const savedStatus = localStorage.getItem("websocketStatus");
    //    const savedGameId = localStorage.getItem("gameId"); // Recupera l'ID partita

    //    if (savedStatus === "connected") {
    //        setStatus("connected");
    //        setGameId(savedGameId); // Riprendi l'ID partita salvato
    //        connectWebSocket(); // Riconnetti automaticamente se lo stato è "connected"
    //    }

    //    // Aggiungi l'event listener per il WebSocket
    //    webSocketService.addListener(handleEvent);

    //    // Salva lo stato della connessione e l'ID della partita
    //    const updateStatus = (newStatus) => {
    //        setStatus(newStatus);
    //        localStorage.setItem("websocketStatus", newStatus); // Memorizza lo stato in localStorage
    //        if (newStatus === "connected" && gameId) {
    //            localStorage.setItem("gameId", gameId); // Memorizza l'ID partita
    //        }
    //    };

    //    // Rimuovi l'event listener al termine
    //    return () => {
    //        webSocketService.removeListener(handleEvent);
    //    };
    //}, [gameId]); // Solo al montaggio del componente



    return (
        <WebSocketContext.Provider value={{ webSocketService, status, gameId, connectWebSocket }}>
            {children}
        </WebSocketContext.Provider>
    );
};

export default WebSocketContext;
