using System;
using System.Collections.Generic;
using System.Linq;

namespace c_
{
    public class CaixaEletronico
    {
        private const int nota100 = 100;
        private const int nota50 = 50;
        private const int nota20 = 20;
        public CaixaEletronico()
        {
          
        }

        public List<Nota> CalcularNotas(int valorSaque)
        {
            List<Nota> notas = new List<Nota>();
            
            notas.AddRange(ObterNotas(valorSaque,nota100));

            int valor = valorSaque - calcularTotal(notas);

            notas.AddRange(ObterNotas(valor,nota50));

            valor = valorSaque - calcularTotal(notas);

            notas.AddRange(ObterNotas(valor,nota20));

            valor = calcularTotal(notas);

            if(valor < valorSaque)
                return new List<Nota>();

            return notas;

            
        }

        private int calcularTotal(List<Nota> notas){
            return notas.Sum(nota100=> nota100.Valor);
        }

        private List<Nota> ObterNotas(int valor, int nota)
        {
            List<Nota> notas = new List<Nota>();
            int valorCalculado = 0;
            int valorSaldo = valor - valorCalculado;
            int qtdnotas =  valorSaldo / nota;
            
            for(int i=0;i<qtdnotas;i++)
                notas.Add(new Nota(nota));
                
            return notas;

        }

    

    }
}