import ChessBoard from '../components/ChessBoard';
import ChessBoardSidebar from '../components/ChessBoardSidebar';
import './Game.css';

const Game = () => {
    return (
        <div className="game-container">
            <ChessBoard />
            <ChessBoardSidebar />
        </div>
    );
};

export default Game;