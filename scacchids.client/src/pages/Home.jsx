import React, { useContext, useState, useEffect } from "react";
import './Home.css';
import { useNavigate } from 'react-router-dom';
import WebSocketContext from "../contexts/WebSocketContext";

const Home = () => {
    const navigate = useNavigate();
    const { webSocketService, status, gameId, connectWebSocket } = useContext(WebSocketContext);
    const [isConnecting, setIsConnecting] = useState(false);
    const [isWaitingForOpponent, setIsWaitingForOpponent] = useState(false);

    const startNewGame = async () => {
        const sleep = (ms) => new Promise(resolve => setTimeout(resolve, ms));
        setIsConnecting(true);
        try
        {
            if (await connectWebSocket()) {
                setIsConnecting(false);
                // Invia richiesta di trovare un match
                webSocketService.send({ action: "find_match" });
                setIsWaitingForOpponent(true)
                // Listener per ricevere il messaggio "match_found"
                const handleMessage = (message) => {
                    if (message.action === "match_found") {
                        console.log("Match found:", message);
                        const newGameId = message.data.GameSessionId;
                        localStorage.setItem("gameId", newGameId); // Memorizza l'ID partita
                        navigate("/game", { state: { boardState: message.boardState, gameId: newGameId } });
                    }
                };

                // Aggiungi il listener
                webSocketService.addListener(handleMessage);

                // Rimuovi il listener quando il componente si smonta
                return () => {
                    webSocketService.removeListener(handleMessage);
                    
                };
            } else {
                
                console.error("Impossible to connect. try again later.");
            }
            setIsConnecting(false);
        } catch (error) {
            setIsConnecting(false);
            console.error("Error connecting to WebSocket:", error);
        }
    };

    const handleConnect = async () => {
        if (status !== "connected") {
            await connectWebSocket(); // Connetti se non connesso
        }
    };


    return (
        <div style={{ padding: "20px" }}>
            <h1>Play a New Game</h1>
            <p>Status: {status}</p>
            <br />
            <button
                id="btnCreateGame"
                onClick={startNewGame}
                disabled={status == "connected" || isConnecting}
            >
                {isConnecting ? "Connecting..." : isWaitingForOpponent ? "Waiting for opponent..." : "NEW GAME"}
            </button>
        </div>
    );
};

export default Home;
