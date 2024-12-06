using ScacchiDS.Server.Services;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using ScacchiDS.Server.DTOs;
using System.Text.Json;

namespace ScacchiDS.Server.WebSockets
{
    public class WebSocketHandler(IServiceProvider serviceProvider, ILogger<GameService> logger)
    {
        // Giocatori connessi in attesa di match
        private static readonly ConcurrentBag<(string SessionId, WebSocket Socket)> WaitingPlayers = [];

        // Tutti i giocatori che hanno cliccato nuova partita
        private static readonly ConcurrentBag<(string SessionId, WebSocket Socket)> ConnectedPlayers = [];

        // Lista delle partite con gli ID dei giocatori che vi hanno preso parte
        private static readonly ConcurrentDictionary<string, (string Player1, string Player2)> Games = new();

        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<GameService> _logger = logger;

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
            if (!WaitingPlayers.IsEmpty)
            {
                var (SessionId, Socket) = WaitingPlayers.FirstOrDefault(); //opponent
                WaitingPlayers.TryTake(out var _); // Rimuovi l'avversario dalla lista di attesa

                // Aggiungi i giocatori nella lista dei connessi
                ConnectedPlayers.Add((sessionIdConnectedPlayer, webSocket));
                ConnectedPlayers.Add((SessionId, Socket));

                // Crea il nuovo match
                var gameSessionId = Guid.NewGuid().ToString();
                Games[gameSessionId] = (sessionIdConnectedPlayer, SessionId);
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    GameService gameService = scope.ServiceProvider.GetRequiredService<GameService>();
                    GameDto gameDTO = await gameService.CreateNewGameAsync(gameSessionId, sessionIdConnectedPlayer, SessionId);

                    // Notifica ai giocatori che la partita è stata creata
                    await NotifyGameCreated(webSocket, Socket, gameDTO);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ProcessWebSocketAsync-mio");
                    throw;
                }

            }
            else
            {
                WaitingPlayers.Add((sessionIdConnectedPlayer, webSocket));
                await NotifyWaitingForOpponent(webSocket);
            }

            await ReceiveMessagesAsync(webSocket);
        }

        private async Task NotifyGameCreated(WebSocket player1, WebSocket player2, GameDto gameDTO)
        {
            try
            {
                var response = new
                {
                    action = "match_found",
                    data = gameDTO
                };

                string message = JsonSerializer.Serialize(response);
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await player1.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                await player2.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ErroreNotifyGameCreated-mio");
                throw;
            }
        }

        private async Task NotifyWaitingForOpponent(WebSocket player1)
        {
            try
            {
                var message = Encoding.UTF8.GetBytes("{\"action\":\"waiting_opponent\"}");
                await player1.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ErroreNotifyGameCreated-mio");
                throw;
            }
        }

        private static async Task ReceiveMessagesAsync(WebSocket webSocket)
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

        private static void RemovePlayerFromLists(WebSocket webSocket)
        {
            // Rimuovi il giocatore da entrambe le liste (connessi e in attesa)
            var player = ConnectedPlayers.FirstOrDefault(p => p.Socket == webSocket);
            if (player != default) ConnectedPlayers.TryTake(out _);

            var waitingPlayer = WaitingPlayers.FirstOrDefault(p => p.Socket == webSocket);
            if (waitingPlayer != default) WaitingPlayers.TryTake(out _);
        }
    }
}
