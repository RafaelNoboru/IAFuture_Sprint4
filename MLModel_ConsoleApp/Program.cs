﻿
// This file was auto-generated by ML.NET Model Builder. 

using MLModel_ConsoleApp;

// Create single instance of sample data from first line of dataset for model input
MLModel.ModelInput sampleData = new MLModel.ModelInput()
{
    Idade = 50F,
    NumeroConsultas = 10F,
    Internacoes = 2F,
    EstadoCivil = @"Casado",
    Cidade = @"RJ",
};



Console.WriteLine("Using model to make single prediction -- Comparing actual Plano with predicted Plano from sample data...\n\n");


Console.WriteLine($"Idade: {50F}");
Console.WriteLine($"NumeroConsultas: {10F}");
Console.WriteLine($"Internacoes: {2F}");
Console.WriteLine($"EstadoCivil: {@"Casado"}");
Console.WriteLine($"Cidade: {@"RJ"}");
Console.WriteLine($"Plano: {@"Ouro"}");


var sortedScoresWithLabel = MLModel.PredictAllLabels(sampleData);
Console.WriteLine($"{"Class",-40}{"Score",-20}");
Console.WriteLine($"{"-----",-40}{"-----",-20}");

foreach (var score in sortedScoresWithLabel)
{
    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
}

Console.WriteLine("=============== End of process, hit any key to finish ===============");
Console.ReadKey();
