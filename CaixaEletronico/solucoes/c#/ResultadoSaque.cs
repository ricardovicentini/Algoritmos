using System;
using System.Collections.Generic;
using System.Text;

namespace c_
{
    public class ResultadoSaque
    {
        public ResultadoSaque()
        {
            Notas = new List<Nota>();
        }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<Nota> Notas { get; set; }
    }
}
