using System;
using System.Collections.Generic;
using System.Text;

namespace TesteContaBancaria.Domain.ApiModel
{
    public class ContaBancariaApiModel
    {
        public int IdConta { get; set; }
        public string Saldo { get; set; }
        public string DataAbertura { get; set; }
        public string Nome { get; set; }
    }
}
