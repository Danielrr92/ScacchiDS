import React, { useContext, useState } from "react";
import "./ChessBoard.css";
import ChessPiece from "./ChessPiece";
import WebSocketContext from "../contexts/WebSocketContext";


const ChessBoard = () => {   
    const { gameState } = useContext(WebSocketContext);

    return (
        <div
            className="wrapper-scacchiera"
        >
            <div id="scacchiera" className={`board bordoScacchiera ${gameState.colorPlayer == 2 ? "black" : ""}`}>
                {[...Array(8)].map((_, rowIndex) => (
                    <div key={rowIndex} className={`row-Scacchiera ${gameState.colorPlayer == 2 ? "black" : ""}`}>
                        {[...Array(8)].map((_, colIndex) => {
                            const isDark = (rowIndex + colIndex) % 2 === 1;
                            const squareId = String.fromCharCode(97 + colIndex) + (8 - rowIndex);
                            let piece;
                            if (gameState.boardState != null)
                                piece= gameState.boardState[squareId];

                            return (
                                <div
                                    key={squareId}
                                    id={squareId}
                                    className={`square ${isDark ? "dark" : "light"}`}
                                >     
                                    {piece && ChessPiece({ piece, squareId })}
                                    
                                </div>
                            );
                        })}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ChessBoard;
