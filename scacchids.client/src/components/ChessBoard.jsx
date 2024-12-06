import React from "react";
import "./ChessBoard.css";

const pieceImages = {
    // Pezzi neri
    k: "/assets/img/Black_King.png",   // Re nero
    q: "/assets/img/Black_Queen.png", // Regina nera
    r: "/assets/img/Black_Rook.png",  // Torre nera
    b: "/assets/img/Black_Bishop.png",// Alfiere nero
    n: "/assets/img/Black_Knight.png",// Cavallo nero
    p: "/assets/img/Black_Pawn.png",  // Pedone nero

    // Pezzi bianchi
    K: "/assets/img/White_King.png",   // Re bianco
    Q: "/assets/img/White_Queen.png",  // Regina bianca
    R: "/assets/img/White_Rook.png",   // Torre bianca
    B: "/assets/img/White_Bishop.png", // Alfiere bianco
    N: "/assets/img/White_Knight.png", // Cavallo bianco
    P: "/assets/img/White_Pawn.png"    // Pedone bianco
};

const ChessBoard = ({ boardState }) => {
    return (
        <div className="wrapper-scacchiera">
            <div id="scacchiera" className="board bordoScacchiera">
                {/* Genera la scacchiera con i quadrati */}
                {[...Array(8)].map((_, rowIndex) => (
                    <div key={rowIndex} className="row-Scacchiera">
                        {[...Array(8)].map((_, colIndex) => {
                            const isDark = (rowIndex + colIndex) % 2 === 1;
                            const squareId = String.fromCharCode(97 + colIndex) + (8 - rowIndex);

                            // Ottieni il pezzo dalla posizione corrente
                            const piece = boardState[squareId];
                            const pieceImage = piece ? `/images/${pieceImages[piece]}` : null;

                            return (
                                <div
                                    key={squareId}
                                    id={squareId}
                                    className={`square ${isDark ? "dark" : "light"}`}
                                >
                                    {pieceImage && (
                                        <img src={pieceImage} alt={piece} className="chess-piece" />
                                    )}
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
