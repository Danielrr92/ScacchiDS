import React, { useContext, useState, useEffect } from "react";
import './Home.css';
import { useNavigate } from 'react-router-dom';
import WebSocketContext from "../contexts/WebSocketContext";


const Home = () => {
    const navigate = useNavigate();

    const { webContextData } = useContext(WebSocketContext);

    const [isStartingNewGame, setIsStartingNewGame] = useState(false);

    const newGame = async () => {
        //setIsStartingNewGame(true);
        const gameStarted = await webContextData.startNewGame();
        //

    };

    useEffect(() => {
        //quando trovo un avversario gameStarted diventa true e faccio redirect alla pagina game
        if (webContextData.gameStarted) {
            navigate("/game", { state: { boardState: message.boardState, gameId: newGameId } });
        }
    }, [webContextData.gameStarted]); // Dipendenze necessarie

    return (
        <div style={{ padding: "20px" }}>
            <h1>Play a New Game</h1>
            <p>Status: {webContextData.webSocketStatus}</p>
            <br />
            <button
                id="btnCreateGame"
                onClick={newGame}
                disabled={webContextData.webSocketStatus == "connected" || isStartingNewGame}
            >
                {webContextData.isWaitingForOpponent ? "Waiting for opponent..." : "NEW GAME"}
            </button>
        </div>
    );
};

export default Home;
