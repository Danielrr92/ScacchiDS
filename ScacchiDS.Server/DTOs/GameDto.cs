namespace ScacchiDS.Server.DTOs
{
    public class GameDto
    {
        public string? GameSessionId { get; set; }
        public string? Player1SessionId { get; set; }
        public string? Player2SessionId { get; set; }
        public int Player1Color { get; set; }
        public int Player2Color { get; set; }

        //public string[,]? ChessBoard { get; set; }

        // Cambia ChessBoard in una lista di liste
        public List<List<string>>? ChessBoard { get; set; }
    }
}
