using ScacchiDS.Server.Services;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using ScacchiDS.Server.DTOs;
using System.Text.Json;
using Azure;

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

            //connessione riuscita
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await ProcessWebSocketAsync(webSocket, context);
        }

        private async Task ProcessWebSocketAsync(WebSocket webSocket, HttpContext context)
        {
            //connessione riuscita - in attesa di messaggi            ( forse devo mandare un messaggio al client di connessione effettuata ma non so ancora se è corretto )
            await ReceiveMessagesAsync(webSocket, context);
        }





        private async Task NotifyGameCreated(WebSocket player1, WebSocket player2, GameDto gameDTO)
        {
            try
            {
                var responsePlayer1 = new
                {
                    action = "match_found",
                    data = gameDTO,
                    color = gameDTO.Player1Color,
                };

                var responsePlayer2 = new
                {
                    action = "match_found",
                    data = gameDTO,
                    color = gameDTO.Player2Color,
                };

                string message = JsonSerializer.Serialize(responsePlayer1);
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await player1.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                message = JsonSerializer.Serialize(responsePlayer2);
                messageBytes = Encoding.UTF8.GetBytes(message);
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

        private async Task ReceiveMessagesAsync(WebSocket webSocket, HttpContext context)
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                //gestione messaggi ricevuti dal client
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Deserializzazione
                    var webSocketMessage = WebSocketMessage.FromJson(receivedMessage);

                    switch (webSocketMessage.action)
                    {
                        case "find_match":

                            string SessionIdConnectedPlayer = context.User?.Identity?.Name ?? Guid.NewGuid().ToString();
                            if (!WaitingPlayers.IsEmpty)
                            {
                                //in questo caso sono il secondo giocatore ad aver cliccato new game
                                var (SessionIdPlayer1, WebSocketPlayer1) = WaitingPlayers.FirstOrDefault(); //opponent (player 1 in attesa)
                                WaitingPlayers.TryTake(out var _); // Rimuovi l'avversario dalla lista di attesa

                                // Aggiungi i giocatori nella lista dei connessi
                                ConnectedPlayers.Add((SessionIdConnectedPlayer, webSocket)); //second player
                                ConnectedPlayers.Add((SessionIdPlayer1, WebSocketPlayer1)); //first player


                                // Crea il nuovo match
                                var gameSessionId = Guid.NewGuid().ToString(); // id della partita
                                Games[gameSessionId] = (SessionIdConnectedPlayer, SessionIdPlayer1); //
                                try
                                {
                                    using var scope = _serviceProvider.CreateScope();

                                    GameService gameService = scope.ServiceProvider.GetRequiredService<GameService>();
                                    GameDto gameDTO = await gameService.CreateNewGameAsync(gameSessionId, SessionIdPlayer1, SessionIdConnectedPlayer);

                                    // Notifica ai giocatori che la partita è stata creata
                                    await NotifyGameCreated(webSocket, WebSocketPlayer1, gameDTO);

                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "ProcessWebSocketAsync-mio");
                                    throw;
                                }

                            }
                            else
                            {
                                WaitingPlayers.Add((SessionIdConnectedPlayer, webSocket));
                                await NotifyWaitingForOpponent(webSocket);
                            }
                            Console.WriteLine("Avvia ricerca partita...");
                            // Usa il Payload se necessario
                            break;

                        case "join_game":
                            Console.WriteLine("Unisciti alla partita...");
                            break;

                        default:
                            Console.WriteLine($"Azione non riconosciuta: {webSocketMessage.action}");
                            break;
                    }

                }
                else if (result.MessageType == WebSocketMessageType.Close)
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
