using System.Text.Json;

namespace ScacchiDS.Server.DTOs
{
    public class WebSocketMessage
    {
        public string? action { get; set; } // Identifica il tipo di messaggio, ad esempio "find_match"
        public object? Payload { get; set; } // Contiene i dati specifici del messaggio (può essere un oggetto più complesso)

        // Facoltativo: utilità per serializzare/deserializzare
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static WebSocketMessage FromJson(string json)
        {
            var deserializedObject = JsonSerializer.Deserialize<WebSocketMessage>(json);
            if (deserializedObject != null)
            {
                return deserializedObject;
            }
            return new WebSocketMessage();
        }
    }
}
