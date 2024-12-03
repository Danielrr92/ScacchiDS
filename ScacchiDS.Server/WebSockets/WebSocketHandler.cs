using ScacchiDS.Server.Services;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

namespace ScacchiDS.Server.WebSockets
{
    public class WebSocketHandler
    {
        // Giocatori connessi in attesa di match
        private static readonly ConcurrentBag<(string SessionId, WebSocket Socket)> WaitingPlayers = new ConcurrentBag<(string, WebSocket)>();

        // Tutti i giocatori che hanno cliccato nuova partita
        private static readonly ConcurrentBag<(string SessionId, WebSocket Socket)> ConnectedPlayers = new ConcurrentBag<(string, WebSocket)>();

        // Lista delle partite con gli ID dei giocatori che vi hanno preso parte
        private static readonly ConcurrentDictionary<string, (string Player1, string Player2)> Games = new ConcurrentDictionary<string, (string, string)>();

        private readonly IServiceProvider _serviceProvider;

        public WebSocketHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task HandleConnectionAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;
                return;
            }

            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await ProcessWebSocketAsync(webSocket, context);
        }

        private async Task ProcessWebSocketAsync(WebSocket webSocket, HttpContext context)
        {

            string sessionIdConnectedPlayer = context.User?.Identity?.Name ?? Guid.NewGuid().ToString();
            if (WaitingPlayers.Count > 0)
            {
                var opponent = WaitingPlayers.FirstOrDefault();
                WaitingPlayers.TryTake(out var _); // Rimuovi l'avversario dalla lista di attesa

                // Aggiungi i giocatori nella lista dei connessi
                ConnectedPlayers.Add((sessionIdConnectedPlayer, webSocket));
                ConnectedPlayers.Add((opponent.SessionId, opponent.Socket));

                // Crea il nuovo match
                var gameSessionId = Guid.NewGuid().ToString();
                Games[gameSessionId] = (sessionIdConnectedPlayer, opponent.SessionId);

                var gameService = _serviceProvider.GetRequiredService<GameService>();
                await gameService.CreateNewGameAsync(gameSessionId, sessionIdConnectedPlayer, opponent.SessionId);

                // Notifica ai giocatori che la partita è stata creata
                await NotifyGameCreated(webSocket, opponent.Socket);
            }
            else
            {
                WaitingPlayers.Add((sessionIdConnectedPlayer, webSocket));
            }

            await ReceiveMessagesAsync(webSocket);
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
                    // Rimuovi il giocatore dalle liste
                    RemovePlayerFromLists(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }

        private void RemovePlayerFromLists(WebSocket webSocket)
        {
            // Rimuovi il giocatore da entrambe le liste (connessi e in attesa)
            var player = ConnectedPlayers.FirstOrDefault(p => p.Socket == webSocket);
            if (player != default) ConnectedPlayers.TryTake(out _);

            var waitingPlayer = WaitingPlayers.FirstOrDefault(p => p.Socket == webSocket);
            if (waitingPlayer != default) WaitingPlayers.TryTake(out _);
        }
    }
}
