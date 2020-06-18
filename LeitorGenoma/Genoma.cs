using System;
using System.Collections.Generic;
using System.Text;

namespace LeitorGenoma
{
    public class Genoma
    {
        public string Organismo { get; set; }
        public string Gene { get; set; }
        public string TipoOrganismo { get; set; }
        public string FamiliaOrganismo { get; set; }
        public string PapelBiologico { get; set; }
        public long PosicaoInicialGenoma { get; set; }
        public long PosicaoFinalGenoma { get; set; }
        public string SequenciaDNA { get; set; }


        public static Genoma Criar(
            string organismo,
            string gene,
            string tipoOrganismo,
            string familiaOrganismo,
            string papelBiologico,
            long posicaoInicialGenoma,
            long posicaoFinalGenoma,
            string sequenciaDNA)
            => new Genoma()
            {
                Organismo = organismo,
                Gene = gene,
                TipoOrganismo = tipoOrganismo,
                FamiliaOrganismo = familiaOrganismo,
                PapelBiologico = papelBiologico,
                PosicaoInicialGenoma = posicaoInicialGenoma,
                PosicaoFinalGenoma = posicaoFinalGenoma,
                SequenciaDNA = sequenciaDNA
            };
    }
}
