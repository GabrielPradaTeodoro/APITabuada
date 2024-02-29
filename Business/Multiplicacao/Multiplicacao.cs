using Business.Models.Response.Multiplicacao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Multiplicacao
{
    public class Multiplicacao
    {
        public Multiplicacao() { }

        public async Task<ListaMultiplicacaoResponse> MultiplicarLista(List<int> lista)
        {
            var response = new ListaMultiplicacaoResponse();
            response.Lista = new List<string>();
            
            foreach (var item in lista)
            {
                var tabuada = MultiplicarItem(item);
                
                var arquivo = WriteFileAsync(item.ToString(), tabuada);
                
                var tabuadaLeitura = ReadTextAsync(await arquivo);
                response.Lista.AddRange((await tabuadaLeitura).ToString().Split("\r\n") );
            }

            return response;
        }

        private async Task<List<string>> MultiplicarItem(int item)
        {
            var retorno = new List<string>();
            const int min = 1;
            const int max = 10;

            for (int i = min; i <= max; i++)
            {
                var valor = item * i;
                string texto = $"{item} x {i} = {valor}";
                retorno.Add(texto);
            }

            return retorno;
        }

        private static async Task<string> WriteFileAsync(string item, Task<List<string>> tabuada)
        {
            var caminho = Directory.GetCurrentDirectory();
            var file = $"{caminho}\\arquivos\\tabuada_de_{item}.txt";

            if (File.Exists(file)) File.Delete(file);

            var lista = await tabuada;

            using (FileStream writer = new FileStream(
                file, 
                FileMode.Append, 
                FileAccess.Write, 
                FileShare.None, 
                bufferSize: 4096, 
                useAsync: true))
            {
                StringBuilder builder = new StringBuilder();
                foreach (string value in lista)
                {
                    builder.AppendLine(value);
                }
                Byte[] info = new UTF8Encoding(true).GetBytes(builder.ToString());
                await writer.WriteAsync(info, 0, info.Length);
            }

            return file;
        }

        public static async Task<string> ReadTextAsync(string arquivo)
        {
            using (FileStream sourceStream = new FileStream(arquivo,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                string retorno = string.Empty;
                var lista = new List<string>();
                byte[] buffer = new byte[0x1000];
                int numRead;

                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    retorno = Encoding.UTF8.GetString(buffer, 0, numRead);
                }

                return retorno;
            }
        }
    }
}
