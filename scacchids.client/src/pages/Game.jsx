import React from "react";
import { useLocation } from "react-router-dom";
import ChessBoard from "../components/ChessBoard";
import ChessBoardSidebar from "../components/ChessBoardSidebar";
import "./Game.css";

const Game = () => {
    const location = useLocation();
    const boardState = location.state?.boardState || {}; // Stato della scacchiera passato

    return (
        <div className="game-container">
            <ChessBoard boardState={boardState} />
            <ChessBoardSidebar />
        </div>
    );
};

export default Game;
