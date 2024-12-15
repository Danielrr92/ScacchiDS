import React, { useRef } from "react";
import "./ChessPiece.css";
import piecesMovementService from '../services/PiecesMovementService';

const pieceImages = {
    k: "Black_King.png",
    q: "Black_Queen.png",
    r: "Black_Rook.png",
    b: "Black_Bishop.png",
    n: "Black_Knight.png",
    p: "Black_Pawn.png",
    K: "White_King.png",
    Q: "White_Queen.png",
    R: "White_Rook.png",
    B: "White_Bishop.png",
    N: "White_Knight.png",
    P: "White_Pawn.png",
};

const ChessPiece = ({ piece, squareId }) => {
    const pieceRef = useRef(null); // Riferimento al pezzo
    const initialPosition = useRef({ x: 0, y: 0, offsetLeft: 0, offsetTop: 0 }); // Posizione iniziale
    const isDraggingRef = useRef(false);

    const handleMouseDown = (e) => {
        piecesMovementService.startDrag(e, initialPosition, isDraggingRef);
        document.addEventListener("mousemove", handleMouseMove);
        document.addEventListener("mouseup", handleMouseUp);
    };

    const handleMouseMove = (e) => {
        piecesMovementService.dragPiece(e, initialPosition, isDraggingRef, pieceRef);
    };

    const handleMouseUp = (e) => {
        piecesMovementService.endDrag(e, initialPosition, isDraggingRef, pieceRef)
        document.removeEventListener("mousemove", handleMouseMove);
        document.removeEventListener("mouseup", handleMouseUp);
    };

    

    return (
        <img
            ref={pieceRef}
            src={`../assets/img/${pieceImages[piece]}`}
            alt={piece}
            className={`piece`}
            onMouseDown={handleMouseDown}

        />
    );
};

export default ChessPiece;
