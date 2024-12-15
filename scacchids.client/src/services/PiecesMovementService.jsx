class PiecesMovementService {

    constructor() {

    }

    startDrag(e, initialPosition, isDraggingRef) {
        e.preventDefault();

        const pezzoSelezionato = e.target;

        pezzoSelezionato.classList.add("dragging")

        if (pezzoSelezionato) {
            isDraggingRef.current = true;

            initialPosition.current = {
                x: e.clientX,
                y: e.clientY,
                offsetX: e.target.offsetLeft,
                offsetY: e.target.offsetTop,
            };
        }
    };

    dragPiece(e, initialPosition, isDraggingRef, pieceRef) {
        if (!isDraggingRef.current) return;

        const pezzoSelezionato = pieceRef.current;

        if (pezzoSelezionato) {

            // Calcola la nuova posizione basandosi sulla posizione iniziale
            const spostamentoX = e.clientX - initialPosition.current.x;
            const spostamentoY = e.clientY - initialPosition.current.y;

            const newX = initialPosition.current.offsetX + spostamentoX;
            const newY = initialPosition.current.offsetY + spostamentoY;

            pezzoSelezionato.style.left = `${newX}px`;
            pezzoSelezionato.style.top = `${newY}px`;

        }
    };

    endDrag(e, initialPosition, isDraggingRef, pieceRef) {

        if (!isDraggingRef.current) return;

        const pezzoSelezionato = pieceRef.current;
        
        if (pezzoSelezionato) {
            pezzoSelezionato.style.left = 0;
            pezzoSelezionato.style.top = 0;

            isDraggingRef.current = false;

            const divCasellaPartenza = pezzoSelezionato.offsetParent;
            const divCasellaDestinazione = ottieniCasellaDestinazione(e.clientX, e.clientY);

            divCasellaPartenza.removeChild(pezzoSelezionato);
            divCasellaDestinazione.appendChild(pezzoSelezionato);
        }
        pezzoSelezionato.classList.remove("dragging")
    }

}

function ottieniCasellaDestinazione(mouseX, mouseY) {
    // Effettua un'iterazione su tutte le caselle della scacchiera
    const caselle = document.querySelectorAll('.square');
    for (let casella of caselle) {
        // Controlla se il mouse si trova all'interno della casella
        const rect = casella.getBoundingClientRect();
        if (mouseX >= rect.left && mouseX <= rect.right && mouseY >= rect.top && mouseY <= rect.bottom) {
            // Restituisci la casella corrispondente
            return casella;
        }
    }
    // Se il mouse è fuori dalla scacchiera, restituisci null
    return null;
}

const piecesMovementService = new PiecesMovementService();

export default piecesMovementService;
