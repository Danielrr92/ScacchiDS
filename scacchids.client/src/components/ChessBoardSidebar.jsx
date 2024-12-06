import './ChessBoardSidebar.css';

const ChessBoardSidebar = () => {
    return (
        <div className="col-xl-3">
            <div className="messaggi">
                <label id="messaggi"></label>
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