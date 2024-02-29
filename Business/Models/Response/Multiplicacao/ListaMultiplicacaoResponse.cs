using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Models.Response.Multiplicacao
{
    public class ListaMultiplicacaoResponse
    {
        [JsonPropertyName("lista")]
        public List<string>? Lista { get; set; }

        [JsonPropertyName("mensagem")]
        public string? Mensagem { get; set; }

        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; set; }
    }
}
