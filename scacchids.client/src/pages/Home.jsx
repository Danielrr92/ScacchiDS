import React, { useContext, useState, useEffect } from "react";
import './Home.css';
import { useNavigate } from 'react-router-dom';
import WebSocketContext from "../contexts/WebSocketContext";


const Home = () => {
    const navigate = useNavigate();
    const { webSocketState, gameState } = useContext(WebSocketContext);

    const newGame = async () => {
        await gameState.startNewGame();
    };


    useEffect(() => {
        // Esegui il redirect solo la prima volta
        if (gameState.doTheRedirectToGamePage) {
            navigate("/game"); // Redirect alla pagina della partita
            gameState.redirectToGamePageDone(); // Resetta lo stato del redirect
        }
    }, [gameState.doTheRedirectToGamePage]);


    return (
        <div style={{ padding: "20px" }}>
            <h1>Play a New Game</h1>
            <p>Status: {webSocketState.webSocketStatus}</p>
            <br />
            <button
                id="btnCreateGame"
                onClick={newGame}
                disabled={webSocketState.webSocketStatus == "connected"}
            >
                {gameState.isWaitingForOpponent ? "Waiting for opponent..." : "NEW GAME"}
            </button>
        </div>
    );
};

export default Home;
