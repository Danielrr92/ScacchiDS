using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScacchiDS.Server.Models
{
    public class Scacchiera
    {
        private Pezzo[,] matrice = new Pezzo[8, 8];

        public Scacchiera()
        {
            InizializzaScacchiera();
        }

        public void InizializzaScacchiera()
        {
            // Creazione della scacchiera 8x8
            Pezzo[,] scacchiera = new Pezzo[8, 8];

            // Inizializzazione dei pedoni
            for (int i = 0; i < 8; i++)
            {
                scacchiera[1, i] = new Pezzo
                {
                    TipoPezzoId = TipoPezzo.PEDONE, 
                    ColoreId = Colore.BIANCO,
                    Simbolo = Pezzo.SIMBOLO_PEDONE
                };
                scacchiera[6, i] = new Pezzo
                {
                    TipoPezzoId = TipoPezzo.PEDONE, 
                    ColoreId = Colore.NERO,
                    Simbolo = Pezzo.SIMBOLO_PEDONE
                };
            }

            // Inizializzazione delle torri
            scacchiera[0, 0] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_TORRE };
            scacchiera[0, 7] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_TORRE };
            scacchiera[7, 0] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_TORRE };
            scacchiera[7, 7] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_TORRE };

            // Inizializzazione dei cavalli
            scacchiera[0, 1] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_CAVALLO };
            scacchiera[0, 6] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_CAVALLO };
            scacchiera[7, 1] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_CAVALLO };
            scacchiera[7, 6] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_CAVALLO };

            // Inizializzazione degli alfieri
            scacchiera[0, 2] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_ALFIERE };
            scacchiera[0, 5] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_ALFIERE };
            scacchiera[7, 2] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_ALFIERE };
            scacchiera[7, 5] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_ALFIERE };

            // Inizializzazione delle regine
            scacchiera[0, 3] = new Pezzo { TipoPezzoId = TipoPezzo.DONNA, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_DONNA };
            scacchiera[7, 3] = new Pezzo { TipoPezzoId = TipoPezzo.DONNA, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_DONNA };

            // Inizializzazione dei re
            scacchiera[0, 4] = new Pezzo { TipoPezzoId = TipoPezzo.RE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_RE };
            scacchiera[7, 4] = new Pezzo { TipoPezzoId = TipoPezzo.RE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_RE };
        }

    }
}
