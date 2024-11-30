using System.Net.WebSockets;
using System.Text;

namespace ScacchiDS.Server.WebSockets
{
    public class WebSocketHandler
    {
        private static readonly List<WebSocket> WaitingPlayers = new List<WebSocket>();

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
                if (WaitingPlayers.Count > 0)
                {
                    //opponent è il giocatore che stava aspettando per primo
                    var opponent = WaitingPlayers.First();
                    WaitingPlayers.Remove(opponent);

                    NotifyMatchFound(webSocket, opponent);
                }
                else
                {
                    WaitingPlayers.Add(webSocket);
                }
            }

            // Keep the connection alive and handle incoming messages
            await ReceiveMessagesAsync(webSocket);
        }

        private async Task NotifyMatchFound(WebSocket player1, WebSocket player2)
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
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }
    }
}
