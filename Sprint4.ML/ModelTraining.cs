using Microsoft.Extensions.Options;
using Microsoft.ML;
using Sprint4.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sprint4.ML
{
    public class ModelTraining
    {
        private readonly MLContext _mlContext;
        private readonly string _caminhoModelo;

        public ModelTraining(IOptions<MLConfig> config)
        {
            _mlContext = new MLContext();   
            _caminhoModelo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modelos", "modelo.zip");
        }
        public static void TreinarModelo(string caminhoModelo)
        {
            try
            {
                var mlContext = new MLContext();
                var dados   = new List<ClientePlanoData>
                {
                    new ClientePlanoData { Idade = 30, Sexo = Models.Enum.Sexo.Masculino, Profissao = "Engenheiro", EstadoSaude = Models.Enum.EstadoSaude.Saudavel, RendaMensal = 5000, PlanoRecomendado = "Plano A" },
            
                };

                var dataView = mlContext.Data.LoadFromEnumerable(dados);

                var pipeline = mlContext.Transforms.Conversion.MapValueToKey("PlanoRecomendado")
                    .Append(mlContext.Transforms.Concatenate("Features", "Idade", "RendaMensal"))
                    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("PlanoRecomendado", "Features"));

                var modelo = pipeline.Fit(dataView);

                mlContext.Model.Save(modelo, dataView.Schema, caminhoModelo);
                Console.WriteLine("Modelo treinado e salvo em: " + caminhoModelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao treinar o modelo: " + ex.Message);
            }
        }
    }
}
