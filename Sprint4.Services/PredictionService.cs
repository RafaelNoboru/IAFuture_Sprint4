using Microsoft.ML;
using Microsoft.Extensions.Options;
using Sprint4.Models;
using System.IO;
using Sprint4.ML;

namespace Sprint4.Services
{
    public class PredictionService
    {
        private readonly MLContext _mlContext;
        private readonly string _caminhoModelo;
        private ITransformer _modelo;

        public string CaminhoModelo => _caminhoModelo;

        public PredictionService(IOptions<MLConfig> config)
        {
            _mlContext = new MLContext();
            _caminhoModelo = config.Value.CaminhoModelo;
            CarregarModelo();
        }

        private void CarregarModelo()
        {
            if (!File.Exists(_caminhoModelo))
            {
                throw new FileNotFoundException($"Modelo não encontrado em: {_caminhoModelo}");
            }

            using (var stream = new FileStream(_caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _modelo = _mlContext.Model.Load(stream, out var schema);
            }

            Console.WriteLine("Modelo carregado com sucesso!");
        }

        // Método para prever o plano de saúde com base nos dados do cliente
        public string Predict(ClientePlanoData cliente)
        {
            // Criação de um novo DataView a partir dos dados do cliente
            var dadosCliente = new[] { cliente };
            var dataViewCliente = _mlContext.Data.LoadFromEnumerable(dadosCliente);

            // Fazer previsões usando o modelo carregado
            var previsao = _modelo.Transform(dataViewCliente);

            // Converter a previsão em um resultado útil
            var resultado = _mlContext.Data.CreateEnumerable<PredictedPlano>(previsao, reuseRowObject: false).FirstOrDefault();
            return resultado?.PlanoRecomendado ?? "Plano não encontrado";
        }
    }

    // Classe para os dados que representam a previsão
    public class PredictedPlano
    {
        public string PlanoRecomendado { get; set; }
    }
}
