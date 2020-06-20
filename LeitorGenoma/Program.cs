using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeitorGenoma
{
    class Program
    {
        private const string caminhoLeitura = "C:/Users/PICHAU/Documents/UCS/FundamentosSO/intergenidb.csv";
        private const string caminhoEscrita = "C:/Users/PICHAU/Documents/UCS/FundamentosSO/intergenidb-ordenado.csv";

        static void Main(string[] args)
        {

            Task<ICollection<Genoma>> task0 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(0));
            Task<ICollection<Genoma>> task1 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(1));
            Task<ICollection<Genoma>> task2 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(2));
            Task<ICollection<Genoma>> task3 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(3));
            Task<ICollection<Genoma>> task4 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(4));
            Task<ICollection<Genoma>> task5 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(5));
            Task<ICollection<Genoma>> task6 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(6));
            Task<ICollection<Genoma>> task7 = Task<ICollection<Genoma>>.Factory.StartNew(() => ExecutarAcaoThread(7));

            var mainTask = Task.WhenAll(task0, task1, task2, task3, task4, task5, task6, task7);

            ExecutarAcaoMainThread(mainTask);
        }

        static ICollection<Genoma> ExecutarAcaoThread(int taskNumber)
        {
            //Leitura dados
            var lineLimit = 10000;
            var lines = File.ReadLines(caminhoLeitura).Skip(taskNumber * lineLimit).Take(lineLimit);
            
            //Criação e Ordenação dos dados
            var genomas = CriarGenomas(lines);
            return genomas.OrderBy(x => x.Gene).ThenBy(y => y.Organismo).ToList();
        }

        private static ICollection<Genoma> CriarGenomas(IEnumerable<string> lines)
        {
            var retorno = new List<Genoma>();
            foreach(var line in lines)
            {
                var dados = line.Split(';');
                retorno.Add(Genoma.Criar(dados[0], dados[1], dados[2], dados[3], dados[4], Convert.ToInt64(dados[5]), Convert.ToInt64(dados[6]), dados[7]));
            }
            return retorno;
        }

        private static void ExecutarAcaoMainThread(Task<ICollection<Genoma>[]> mainTask)
        {
            while (!mainTask.IsCompleted) { }

            var GenomasEscrita = mainTask.Result.SelectMany(result => result).OrderBy(x => x.Gene).ThenBy(y => y.Organismo).ToList();
            var escrita = new StringBuilder();
            foreach (var Genoma in GenomasEscrita)
                escrita.AppendLine(ObterFormatoLinha(Genoma));

            File.WriteAllText(caminhoEscrita, escrita.ToString());
        }

        private static string ObterFormatoLinha(Genoma genoma)
        {
            return $@"{genoma.Gene};{genoma.Organismo};{genoma.TipoOrganismo};{genoma.FamiliaOrganismo};{genoma.PapelBiologico};{genoma.PosicaoInicialGenoma};{genoma.PosicaoFinalGenoma};{genoma.SequenciaDNA}";
        }
    }
}
