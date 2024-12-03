using ScacchiDS.Server.Services;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Text;

namespace ScacchiDS.Server.WebSockets
{
    public class WebSocketHandler
    {
        //giocatori connessi in attesa di match
        private static readonly List<(string SessionId, WebSocket Socket)> WaitingPlayers = new List<(string, WebSocket)>();

        //tutti i giocatori che hanno cliccato nuova partita
        private static readonly List<(string SessionId, WebSocket Socket)> ConnectedPlayers = new List<(string, WebSocket)>();

        //lista delle partite con gli id dei giocatori che vi hanno preso parte
        private static readonly List<(string GameId, (string SessionIdPlayer1, string SessionIdPlayer2))> Games = new List<(string, (string, string))>();

        private readonly GameService _gameService;

        public WebSocketHandler(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task HandleConnectionAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;
                return;
            }

            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await ProcessWebSocketAsync(webSocket);
        }

        private async Task ProcessWebSocketAsync(WebSocket webSocket)
        {
            lock (WaitingPlayers)
            {
                //id sessione proprietario per il giocatore che clicca nuova partita, sia esso loggato/registrato oppure no
                var sessionIdConnectedPlayer = Guid.NewGuid().ToString();

                

                if (WaitingPlayers.Count > 0)
                {
                    //inserisco il giocatore nella lista dei giocatori connessi
                    ConnectedPlayers.Add((sessionIdConnectedPlayer, webSocket));

                    //inserisco anche il waitingPlayer nella lista dei giocatori connessi
                    ConnectedPlayers.Add(WaitingPlayers[0]);

                    //aggiorno la lista dei giocatori in attesa di partita
                    var opponent = WaitingPlayers.First();
                    WaitingPlayers.Remove(opponent);

                    //creo il nuovo match
                    StartNewGame(sessionIdConnectedPlayer, opponent.SessionId);

                    //avviso i giocatori che il gioco è stato creato
                    NotifyGameCreated(webSocket, opponent.Socket);
                }
                else
                {
                    WaitingPlayers.Add((sessionIdConnectedPlayer, webSocket));
                }
            }

            // Keep the connection alive and handle incoming messages
            await ReceiveMessagesAsync(webSocket);
        }

        private async Task StartNewGame( string sessionIdPlayer1, string sessionIdPlayer2)
        {
            //genero l'id della partita e la inserisco tra le partite cominciate
            var gameSessionId = Guid.NewGuid().ToString();
            Games.Add((gameSessionId, (sessionIdPlayer1, sessionIdPlayer2)));
            await _gameService.CreateNewGameAsync(gameSessionId, sessionIdPlayer1, sessionIdPlayer2);
        }

        private async Task NotifyGameCreated(WebSocket player1, WebSocket player2)
        {
            var message = Encoding.UTF8.GetBytes("{\"action\":\"match_found\"}");
            await player1.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
            await player2.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
        }


        private async Task ReceiveMessagesAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Rimuovi il giocatore dalla lista giocatori totali oppure dalla lista giocatori in attesa nel caso in cui ci si trovasse
                    lock (ConnectedPlayers)
                    {
                        var player = ConnectedPlayers.FirstOrDefault(p => p.Socket == webSocket);
                        if (player != default)
                        {
                            ConnectedPlayers.Remove(player);
                        }
                        player = WaitingPlayers.FirstOrDefault(p => p.Socket == webSocket);
                        if (player != default)
                        {
                            ConnectedPlayers.Remove(player);
                        }
                    }
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }
    }
}
