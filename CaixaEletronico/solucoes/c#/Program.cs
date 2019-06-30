using System;
using System.Linq;

namespace c_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Quanto quer sacar?");
            int valorSolicitado =  0;
            int.TryParse(Console.ReadLine(), out valorSolicitado);

            CaixaEletronico cx = new CaixaEletronico();
            var notas = cx.CalcularNotas(valorSolicitado);

            var retorno = notas.GroupBy(nota=>nota.Valor).Select(group => new {Valor = group.Key, Quantidade = group.Count()  } ).OrderBy(x=>x.Valor);

            if(retorno.Any())
            {
                foreach(var nota in retorno)
                    Console.WriteLine($"Nota: {nota.Valor} Quantidade: {nota.Quantidade}");

            }
            else
            {
                Console.WriteLine("Valor inválido");
            }
            

        }
    }
}
