using Microsoft.ML;
using Sprint4.Models;

public static class ModelTraining
{
    public static void TreinarModelo(string caminhoModelo)
    {
        var mlContext = new MLContext();

        var dataPath = "D:\\Sprint4\\Sprint4\\Dados\\clientes_treino.csv";
        IDataView dataView = mlContext.Data.LoadFromTextFile<ClientePlanoData>(
            dataPath, hasHeader: true, separatorChar: ',');

        var pipeline = mlContext.Transforms.Conversion
            .MapValueToKey("PlanoEscolhido")
            .Append(mlContext.Transforms.Categorical.OneHotEncoding("EstadoCivil"))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding("Cidade"))
            .Append(mlContext.Transforms.Concatenate("Features",
                "Idade", "NumeroConsultas", "Internacoes", "EstadoCivil", "Cidade"))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("PlanoEscolhido", "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        var model = pipeline.Fit(dataView);

        mlContext.Model.Save(model, dataView.Schema, caminhoModelo);
        Console.WriteLine($"Modelo salvo em: {caminhoModelo}");
    }
}
