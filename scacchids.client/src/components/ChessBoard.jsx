import React from "react";
import "./ChessBoard.css";

const pieceImages = {
    // Pezzi neri
    k: "Black_King.png",    // Re nero
    q: "Black_Queen.png",   // Regina nera
    r: "Black_Rook.png",    // Torre nera
    b: "Black_Bishop.png",  // Alfiere nero
    n: "Black_Knight.png",  // Cavallo nero
    p: "Black_Pawn.png",    // Pedone nero

    // Pezzi bianchi
    K: "White_King.png",    // Re bianco
    Q: "White_Queen.png",   // Regina bianca
    R: "White_Rook.png",    // Torre bianca
    B: "White_Bishop.png",  // Alfiere bianco
    N: "White_Knight.png",  // Cavallo bianco
    P: "White_Pawn.png"     // Pedone bianco
};

// Configurazione iniziale della scacchiera
const initialBoardState = {
    // Pezzi neri
    a8: "r", b8: "n", c8: "b", d8: "q", e8: "k", f8: "b", g8: "n", h8: "r",
    a7: "p", b7: "p", c7: "p", d7: "p", e7: "p", f7: "p", g7: "p", h7: "p",

    // Righe vuote
    a6: null, b6: null, c6: null, d6: null, e6: null, f6: null, g6: null, h6: null,
    a5: null, b5: null, c5: null, d5: null, e5: null, f5: null, g5: null, h5: null,
    a4: null, b4: null, c4: null, d4: null, e4: null, f4: null, g4: null, h4: null,
    a3: null, b3: null, c3: null, d3: null, e3: null, f3: null, g3: null, h3: null,

    // Pezzi bianchi
    a2: "P", b2: "P", c2: "P", d2: "P", e2: "P", f2: "P", g2: "P", h2: "P",
    a1: "R", b1: "N", c1: "B", d1: "Q", e1: "K", f1: "B", g1: "N", h1: "R"
};

const ChessBoard = () => {
    // Usa la configurazione iniziale della scacchiera
    const boardState = initialBoardState;

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
                            const pieceImage = piece ? `/assets/img/${pieceImages[piece]}` : null;

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
