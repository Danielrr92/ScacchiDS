import React, { createContext, useEffect, useState } from "react";
import webSocketService from "../services/WebSocketService";

const WebSocketContext = createContext();

export const WebSocketProvider = ({ children }) => {
    const [webSocketStatus, setWebSocketStatus] = useState("disconnected");
    const [gameId, setGameId] = useState(null); // Memorizza l'ID della partita in corso
    const [gameStarted, setGameStarted] = useState(false); // Memorizza l'ID della partita in corso
    const [isWaitingForOpponent, setIsWaitingForOpponent] = useState(false);
    const [boardState, setBoardState] = useState();
    const [isPlayingGame, setIsPlayingGame] = useState(false);
    const [doTheRedirectToGamePage, setDoTheRedirectToGamePage] = useState(false);
    const [colorPlayer, setColorPlayer] = useState();

    const INITIAL_BOARD_STATE = {
        a8: "r", b8: "n", c8: "b", d8: "q", e8: "k", f8: "b", g8: "n", h8: "r",
        a7: "p", b7: "p", c7: "p", d7: "p", e7: "p", f7: "p", g7: "p", h7: "p",
        a2: "P", b2: "P", c2: "P", d2: "P", e2: "P", f2: "P", g2: "P", h2: "P",
        a1: "R", b1: "N", c1: "B", d1: "Q", e1: "K", f1: "B", g1: "N", h1: "R"
    };

    const initializeBoard = () => {
        return INITIAL_BOARD_STATE;
    }

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

    const redirectToGamePageDone = () => {
        setDoTheRedirectToGamePage(false);
    }

    const connessione = async () => {
        if (webSocketStatus === "disconnected") {
            try {
                const result = await webSocketService.connect("wss://localhost:7225/ws");
                // Verifica se la connessione è avvenuta con successo
                if (result && result.success) {
                    setWebSocketStatus("connected");
                    return true; // Connessione riuscita
                } else {
                    setWebSocketStatus("disconnected");
                    return false; // Connessione fallita
                }
            } catch (error) {
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
                    setGameId(message.data.GameSessionId);
                    setColorPlayer(message.color);
                    setGameStarted(true);
                    setDoTheRedirectToGamePage(true)
                    setBoardState(initializeBoard())
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
        };

        webSocketService.addListener(handleMessage);
        return () => {
            webSocketService.removeListener(handleMessage);
        };

    }, []); // Dipendenze necessarie

    //dati accessibili nel contesto da altre parti del codice
    const webSocketState = {        
        webSocketStatus,        
    };

    const gameState = {
        isWaitingForOpponent,
        gameId,
        startNewGame,
        gameStarted,
        boardState,
        isPlayingGame,
        redirectToGamePageDone,
        doTheRedirectToGamePage,
        colorPlayer
    }

    return (
        <WebSocketContext.Provider value={{ webSocketState, gameState }}>
            {children}
        </WebSocketContext.Provider>
    );
};

export default WebSocketContext;
