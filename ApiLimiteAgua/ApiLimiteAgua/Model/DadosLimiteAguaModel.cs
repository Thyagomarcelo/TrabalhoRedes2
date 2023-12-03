using Microsoft.AspNetCore.Routing.Constraints;
using System;

namespace ApiLimiteAgua.Model
{
    public class DadosLimiteAguaModel
    {
        public DateTime dataColeta {get;set;}
        public string local { get; set;}
        public double altura { get; set;}
    }
}
