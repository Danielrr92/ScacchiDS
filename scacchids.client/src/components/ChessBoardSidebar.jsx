import './ChessBoardSidebar.css';

const ChessBoardSidebar = () => {
    return (
        <div className="col-xl-3">
            <div className="messaggi">
                <label id="messaggi"></label>
            </div>
            <div className="btn-new-game" id="pannelloBtnInizioPartita">
                <div className="row">
                    <div className="col-xl-12">
                        <button id="btnCreateGame">NEW GAME</button>
                    </div>
                    <div id="divIdPartita" className="col-xl-12 hidden">
                        <p>ID: <label id="labelIdPartita"></label></p>
                    </div>
                    <div id="panelJoinGame">
                        <div className="col-xl-12">
                            <p>Join a game with an ID</p>
                            <input type="text" id="textBoxIdPartita" placeholder="Game ID" />
                        </div>
                        <div className="col-xl-12">
                            <button id="btnJoinGame">JOIN</button>
                        </div>
                    </div>
                </div>
            </div>
            <div className="moves-container">
                <div className="moves-header">
                    <span>Mosse</span>
                </div>
                <div id="moves" className="moves">
                    {/* Moves will be dynamically added here */}
                </div>
            </div>
            <div id="pannelloBtnRestart" className="hidden">
                <button id="btnRestart">RESTART</button>
            </div>
        </div>
    );
};

export default ChessBoardSidebar;