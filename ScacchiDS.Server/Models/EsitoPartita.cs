namespace ScacchiDS.Server.Models
{
    public class EsitoPartita
    {
        public int Id { get; set; }  // ID univoco per il risultato
        public string Descrizione { get; set; }  // Descrizione dell'esito (ad esempio "Vittoria", "Patta", ecc.)
    }
}
