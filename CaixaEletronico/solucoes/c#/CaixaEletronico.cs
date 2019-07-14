using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_
{
    public class CaixaEletronico
    {
        private const int nota100 = 100;
        private const int nota50 = 50;
        private const int nota20 = 20;

        List<Nota> notasCaixa = new List<Nota>();


        public List<Nota> Sacar(int valorSaque)
        {
            List<Nota> notas = new List<Nota>();
            int saldoCaixa = CalcularTotalDinheiro(notasCaixa);
            if (saldoCaixa < valorSaque) throw new Exception("O caixa não possuí o valor solicitado");

            notas.AddRange(ObterNotas(valorSaque, nota100));

            int valor = valorSaque - CalcularTotalDinheiro(notas);

            notas.AddRange(ObterNotas(valor, nota50));

            valor = valorSaque - CalcularTotalDinheiro(notas);

            notas.AddRange(ObterNotas(valor, nota20));

            valor = CalcularTotalDinheiro(notas);


            if (valor < valorSaque)
                return new List<Nota>();

            return notas;


        }

        public ResultadoSaque SacarV2(int valorSaque)
        {
            ResultadoSaque resultado = new ResultadoSaque();
            int saldoCaixa = CalcularTotalDinheiro(notasCaixa);
            if (saldoCaixa < valorSaque)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = "O caixa não possuí o valor solicitado";
                return resultado;

            }
            if (!ValidarValorSolcitado(valorSaque))
            {
                resultado.Sucesso = false;
                resultado.Mensagem = "valor solicitado não é válido";
                return resultado;
            }

            resultado.Notas = ObterNotas(valorSaque);
            if (!resultado.Notas.Any())
            {
                resultado.Sucesso = false;
                resultado.Mensagem = "valor solicitado indisponível";
                return resultado;
            }
            else
            {
                resultado.Sucesso = true;
                var retorno = resultado.Notas.GroupBy(nota => nota.Valor).Select(group => new { Valor = group.Key, Quantidade = group.Count() }).OrderBy(x => x.Valor);
                StringBuilder sb = new StringBuilder();
                foreach (var nota in retorno)
                    sb.AppendLine($"Nota: {nota.Valor} Quantidade: {nota.Quantidade}");
                resultado.Mensagem = sb.ToString();
            }



            return resultado;
        }


        private int CalcularTotalDinheiro(List<Nota> notas)
        {
            return notas.Sum(nota => nota.Valor);
        }

        private List<Nota> ObterNotas(int valor, int nota)
        {
            List<Nota> notas = new List<Nota>();
            int valorCalculado = 0;
            int valorSaldo = valor - valorCalculado;
            int qtdnotas = valorSaldo / nota;

            while (notasCaixa.Where(n => n.Valor == nota).Any() && qtdnotas > 0)
            {
                var notaSlecionada = new Nota(nota);
                RemoverNota(notaSlecionada);
                notas.Add(notaSlecionada);
                qtdnotas--;
            }



            return notas;

        }

        //problema de sobrar 50 reais
        private List<Nota> ObterNotas(int valor)
        {
            List<Nota> notas = new List<Nota>();
            var grupoNotas = notasCaixa.GroupBy(n => n.Valor).Select(g => new Nota(g.Key)).OrderByDescending(o => o.Valor);
            int valorSaldo = valor;
            foreach (var gNota in grupoNotas)
            {
                valorSaldo = valor - CalcularTotalDinheiro(notas);
                int qtdNotasAsacar = valorSaldo / gNota.Valor;
                int qtdNotasCaixa = notasCaixa.Count(n => n.Valor == gNota.Valor);
                while (qtdNotasCaixa > 0 && qtdNotasAsacar > 0)
                {
                    var notaSlecionada = new Nota(gNota.Valor);
                    notas.Add(notaSlecionada);
                    qtdNotasCaixa--;
                    qtdNotasAsacar--;
                }
            }
            if (valor != CalcularTotalDinheiro(notas)) return new List<Nota>();
            RemoverNotas(notas);
            return notas;
        }

        

        private List<Nota> SacarNotas(int valor, int quantidade)
        {
            List<Nota> notas = new List<Nota>();
            var notaSlecionada = new Nota(valor);
            for (int i = 1; i <= quantidade; i++)
            {
                notas.Add(notaSlecionada);
            }
            return notas;

        }
        private bool ValidarValorSolcitado(int valor)
        {
            var grupoNotas = ObterNostasOrdemDescendenteAgrupadas();

            foreach (Nota n in grupoNotas)
                if (valor % n.Valor == 0) return true;

            return false;
        }

        private int ObterMultiplo(int valor)
        {
            var grupoNotas = ObterNostasOrdemDescendenteAgrupadas();

            foreach (Nota n in grupoNotas)
                if (valor % n.Valor == 0) return n.Valor;

            return 0;
        }

        private List<Nota> ObterNostasOrdemDescendenteAgrupadas()
        {
            return notasCaixa.GroupBy(g => g.Valor).Select(n => new Nota(n.Key)).OrderByDescending(o => o.Valor).ToList();
        }

        public void AdicionarNotas(int valor, int quantidade)
        {
            for (int i = 1; i <= quantidade; i++)
            {
                notasCaixa.Add(new Nota(valor));
            }
        }
        private void AdicionarNotas(IEnumerable<Nota> notas)
        {
            notasCaixa.AddRange(notas);
        }

        private void RemoverNotas(List<Nota> notas)
        {
            foreach (var nota in notas)
            {
                var n = notasCaixa.Where(n => n.Valor == nota.Valor).FirstOrDefault();
                notasCaixa.Remove(n);
            }
        }

        private void RemoverNota(Nota nota)
        {
            var n = notasCaixa.Where(n => n.Valor == nota.Valor).FirstOrDefault();
            notasCaixa.Remove(n);
        }

        public int SaldoDinheiro()
        {
            return CalcularTotalDinheiro(notasCaixa);
        }



    }
}