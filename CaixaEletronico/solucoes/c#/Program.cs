using System;
using System.Linq;

namespace c_
{
    class Program
    {
        static void Main(string[] args)
        {
            CaixaEletronico cx = new CaixaEletronico();
            cx.AdicionarNotas(100, 5);
            cx.AdicionarNotas(50, 5);
            cx.AdicionarNotas(20, 5);

            do
            {
                Console.WriteLine($"O saldo atual do caixa é {cx.SaldoDinheiro()}");
                Console.WriteLine("Quanto quer sacar?");

                int valorSolicitado = 0;
                int.TryParse(Console.ReadLine(), out valorSolicitado);


                var resultado = cx.SacarV2(valorSolicitado);
                Console.WriteLine("Sucesso: " + resultado.Sucesso);
                Console.WriteLine("Mensagem:");
                Console.WriteLine(resultado.Mensagem);

                Console.WriteLine($"O saldo atual do caixa é {cx.SaldoDinheiro()}");

                Console.WriteLine("Continuar sacando?(s/n)");


            } while (Console.ReadKey().Key == ConsoleKey.S);



        }
    }
}
