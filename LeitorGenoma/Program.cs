using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeitorGenoma
{
    class Program
    {
        private const string caminho = "C:/Users/PICHAU/Documents/UCS/FundamentosSO/LeitorGenoma/LeitorGenoma/intergenidb.csv";
        static void Main(string[] args)
        {

            Task task0 = Task.Factory.StartNew(() => doStuff(0, 10000));
            Task task1 = Task.Factory.StartNew(() => doStuff(1, 10000));
            Task task2 = Task.Factory.StartNew(() => doStuff(2, 10000));
            Task task3 = Task.Factory.StartNew(() => doStuff(3, 10000));
            Task task4 = Task.Factory.StartNew(() => doStuff(4, 10000));
            Task task5 = Task.Factory.StartNew(() => doStuff(5, 10000));
            Task task6 = Task.Factory.StartNew(() => doStuff(6, 10000));

            Task.WaitAll(task0, task1, task2, task3, task4, task5, task6);
            Console.WriteLine("All threads complete");

            //Parallel.ForEach(File.ReadLines(caminho).Take(10), (line, _, lineNumber) =>
            //{
            //    Console.WriteLine(_);
            //    Console.WriteLine(line);
            //});
        }

        static void doStuff(int taskNumber, int lineLimit)
        {
            var lines = File.ReadLines(caminho).Skip(taskNumber * lineLimit).Take(lineLimit);
            var genomas = CriarGenomas(lines);
            Console.WriteLine(taskNumber);
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
    }
}
