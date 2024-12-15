import React, { useState } from "react";
import "./ChessBoard.css";
import ChessPiece from "./ChessPiece";



const ChessBoard = () => {
    const [boardState, setBoardState] = useState(initializeBoard());
    const [draggedPiece, setDraggedPiece] = useState(null);
    const [draggedFrom, setDraggedFrom] = useState(null);
    

    // Funzione per inizializzare la scacchiera in posizione di partenza
    function initializeBoard() {
        return {
            a8: "r", b8: "n", c8: "b", d8: "q", e8: "k", f8: "b", g8: "n", h8: "r",
            a7: "p", b7: "p", c7: "p", d7: "p", e7: "p", f7: "p", g7: "p", h7: "p",
            a2: "P", b2: "P", c2: "P", d2: "P", e2: "P", f2: "P", g2: "P", h2: "P",
            a1: "R", b1: "N", c1: "B", d1: "Q", e1: "K", f1: "B", g1: "N", h1: "R"
        };
    }

    return (
        <div
            className="wrapper-scacchiera"
        >
            <div id="scacchiera" className="board bordoScacchiera">
                {[...Array(8)].map((_, rowIndex) => (
                    <div key={rowIndex} className="row-Scacchiera">
                        {[...Array(8)].map((_, colIndex) => {
                            const isDark = (rowIndex + colIndex) % 2 === 1;
                            const squareId = String.fromCharCode(97 + colIndex) + (8 - rowIndex);
                            const piece = boardState[squareId];

                            //console.log(squareId);
                            //console.log(piece);
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
