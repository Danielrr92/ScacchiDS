namespace ScacchiDS.Server.Models
{
    public class EsitoPartita
    {
        public int Id { get; set; }  // ID univoco per il risultato
        public string Descrizione { get; set; }  // Descrizione dell'esito (ad esempio "Vittoria", "Patta", ecc.)

        public const int ID_ESITO_VITTORIA_BIANCO = 1;
        public const int ID_ESITO_VITTORIA_NERO = 2;
        public const int ID_ESITO_PATTA = 3;
        public const int ID_ESITO_IN_CORSO = 4;
    }
}
