using Microsoft.ML;
using Sprint4.Models;

public class PredictionService
{
    private readonly MLContext _mlContext;
    private ITransformer _modelo;

    public PredictionService()
    {
        _mlContext = new MLContext();
        TreinarModelo();
    }

    private void TreinarModelo()
    {
        var dadosTreinamento = new List<ClientePlanoData>
        {
            new ClientePlanoData { Idade = 25, NumeroConsultas = 3, Internacoes = 0, EstadoCivil = "Solteiro", Cidade = "SP", Plano = "Bronze" },
            new ClientePlanoData { Idade = 50, NumeroConsultas = 10, Internacoes = 2, EstadoCivil = "Casado", Cidade = "RJ", Plano = "Ouro" },
            new ClientePlanoData { Idade = 35, NumeroConsultas = 5, Internacoes = 1, EstadoCivil = "Casado", Cidade = "SP", Plano = "Prata" }
        };

        var dataView = _mlContext.Data.LoadFromEnumerable(dadosTreinamento);

        var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ClientePlanoData.Plano))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("EstadoCivil"))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("Cidade"))
            .Append(_mlContext.Transforms.Concatenate("Features", "Idade", "NumeroConsultas", "Internacoes", "EstadoCivil", "Cidade"))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _modelo = pipeline.Fit(dataView);
        Console.WriteLine("[Info] Modelo treinado com sucesso.");
    }

    public PlanoSaudePrediction Predict(ClientePlanoData cliente)
    {
        var data = new[] { cliente };
        var inputData = _mlContext.Data.LoadFromEnumerable(data);

        var prediction = _modelo.Transform(inputData);

        var resultados = _mlContext.Data.CreateEnumerable<PlanoSaudePrediction>(prediction, reuseRowObject: false).FirstOrDefault();

        return resultados;
    }

}