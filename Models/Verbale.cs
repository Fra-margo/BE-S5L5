﻿namespace Esercizio.Models
{
    public class Verbale
    {
        public int IDverbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public int IDanagrafica { get; set; }
        public int IDviolazione { get; set; }

        public List<Anagrafica> Anagrafiche { get; set; }
        public List<TipoViolazione> Violazioni { get; set; }
    }
}
