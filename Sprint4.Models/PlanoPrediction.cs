using Microsoft.ML.Data;

namespace Sprint4.Models
{
    public class PlanoPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PlanoRecomendado { get; set; }
    }
}
