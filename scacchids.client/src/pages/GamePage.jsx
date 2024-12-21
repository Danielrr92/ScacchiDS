import React, { useContext, useState } from "react";
import WebSocketContext from "../contexts/WebSocketContext";
//import { useLocation } from "react-router-dom";
import ChessBoard from "../components/ChessBoard";
import ChessBoardSidebar from "../components/ChessBoardSidebar";
import "./GamePage.css";

const GamePage = () => {
    //const location = useLocation();
    //const state = location.state || {}; // Stato della scacchiera passato

    const { gameState } = useContext(WebSocketContext);


    return (
        <div>
            <ChessBoard />
            <ChessBoardSidebar />
        </div>
    );
};

export default GamePage;
