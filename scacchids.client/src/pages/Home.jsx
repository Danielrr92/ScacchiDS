import './Home.css';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

const Home = () => {
    const navigate = useNavigate();
    let ws;

    const startNewGame = () => {
        
        ws = new WebSocket('ws://localhost:5000/ws');
        ws.onopen = () => {
            console.log('WebSocket connected');
            ws.send(JSON.stringify({ action: 'find_match' }));
        };

        ws.onmessage = (event) => {
            const message = JSON.parse(event.data);
            if (message.action === 'match_found') {
                console.log('Match found!', message);
                // Naviga alla scacchiera con i dettagli del match
                console.log('Navigating to the game page...');
                navigate('/game'); // Cambia l'URL e naviga alla pagina del gioco
            }
        };

        ws.onclose = () => {
            console.log('WebSocket disconnected');
        };
    };

    useEffect(() => {
        return () => {
            if (ws) ws.close();
        };
    }, []);


    return (
        <div style={{ padding: '20px' }}>
            <h1>Play a New Game</h1>
            <button id="btnCreateGame" onClick={startNewGame}>
                NEW GAME
            </button>
        </div>
    );
};

export default Home;