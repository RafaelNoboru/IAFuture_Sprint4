using Microsoft.ML.Data;

namespace Sprint4.Models
{
    public class PlanoSaudePrediction
    {
        [ColumnName("PredictedLabel")]
        public string PlanoEscolhido { get; set; }
        public float[] Score { get; set; }
    }
}
