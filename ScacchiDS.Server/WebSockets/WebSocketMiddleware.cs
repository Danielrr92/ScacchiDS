namespace ScacchiDS.Server.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                await _webSocketHandler.HandleConnectionAsync(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
