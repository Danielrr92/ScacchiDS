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

            // Inizializzazione dei pedoni
            // -bianchi
            matrice[1, 0] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "a2" };
            matrice[1, 1] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "b2" };
            matrice[1, 2] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "c2" };
            matrice[1, 3] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "d2" };
            matrice[1, 4] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "e2" };
            matrice[1, 5] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "f2" };
            matrice[1, 6] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "g2" };
            matrice[1, 7] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "h2" };

            // -neri
            matrice[6, 0] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "a7" };
            matrice[6, 1] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "b7" };
            matrice[6, 2] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "c7" };
            matrice[6, 3] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "d7" };
            matrice[6, 4] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "e7" };
            matrice[6, 5] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "f7" };
            matrice[6, 6] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "g7" };
            matrice[6, 7] = new Pezzo { TipoPezzoId = TipoPezzo.PEDONE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_PEDONE, posizione = "h7" };

            // Inizializzazione delle torri
            matrice[0, 0] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_TORRE, posizione = "a1" };
            matrice[0, 7] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_TORRE, posizione = "h1" };
            matrice[7, 0] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_TORRE, posizione = "a8" };
            matrice[7, 7] = new Pezzo { TipoPezzoId = TipoPezzo.TORRE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_TORRE, posizione = "h8" };

            // Inizializzazione dei cavalli
            matrice[0, 1] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_CAVALLO, posizione = "b1" };
            matrice[0, 6] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_CAVALLO, posizione = "g1" };
            matrice[7, 1] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_CAVALLO, posizione = "b8" };
            matrice[7, 6] = new Pezzo { TipoPezzoId = TipoPezzo.CAVALLO, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_CAVALLO, posizione = "g8" };

            // Inizializzazione degli alfieri
            matrice[0, 2] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_ALFIERE, posizione = "c1" };
            matrice[0, 5] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_ALFIERE, posizione = "f1" };
            matrice[7, 2] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_ALFIERE, posizione = "c8" };
            matrice[7, 5] = new Pezzo { TipoPezzoId = TipoPezzo.ALFIERE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_ALFIERE, posizione = "f8" };

            // Inizializzazione delle regine
            matrice[0, 3] = new Pezzo { TipoPezzoId = TipoPezzo.DONNA, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_DONNA, posizione = "d1" };
            matrice[7, 3] = new Pezzo { TipoPezzoId = TipoPezzo.DONNA, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_DONNA, posizione = "d8" };

            // Inizializzazione dei re
            matrice[0, 4] = new Pezzo { TipoPezzoId = TipoPezzo.RE, ColoreId = Colore.BIANCO, Simbolo = Pezzo.SIMBOLO_RE, posizione = "e1" };
            matrice[7, 4] = new Pezzo { TipoPezzoId = TipoPezzo.RE, ColoreId = Colore.NERO, Simbolo = Pezzo.SIMBOLO_RE, posizione = "e8" };
        }



        public List<List<string>> ToStringArray() // Metodo modificato per restituire una lista di liste
        {
            var result = new List<List<string>>();  // Crea una lista di liste

            for (int i = 0; i < 8; i++)
            {
                var row = new List<string>();  // Crea una lista per la riga i-esima
                for (int j = 0; j < 8; j++)
                {
                    row.Add(matrice[i, j]?.notazionePerDTO ?? string.Empty);  // Aggiungi l'elemento della matrice alla riga
                }
                result.Add(row);  // Aggiungi la riga alla lista principale
            }

            return result;  // Restituisci la lista di liste
        }

    }
}
