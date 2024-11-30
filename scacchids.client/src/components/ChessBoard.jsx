import './ChessBoard.css';

const ChessBoard = () => {
    return (
        <div className="wrapper-scacchiera">
            <div id="scacchiera" className="board bordoScacchiera">
                {/* Genera la scacchiera con i quadrati */}
                {[...Array(8)].map((_, rowIndex) => (
                    <div key={rowIndex} className="row-Scacchiera">
                        {[...Array(8)].map((_, colIndex) => {
                            const isDark = (rowIndex + colIndex) % 2 === 1;
                            const squareId = String.fromCharCode(97 + colIndex) + (8 - rowIndex);
                            return (
                                <div
                                    key={squareId}
                                    id={squareId}
                                    className={`square ${isDark ? 'dark' : 'light'}`}
                                ></div>
                            );
                        })}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ChessBoard;