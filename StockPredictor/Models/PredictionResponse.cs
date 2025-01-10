namespace StockPredictor.Models
{
    public class PredictionResponse
    {
        public string CompanyName { get; set; } // Matches "CompanyName" in the JSON
        public LastClosingPriceModel LastClosingPrice { get; set; } // Matches "LastClosingPrice" in the JSON
        public List<PredictedPriceModel> PredictedPrices { get; set; } // Matches "PredictedPrices" in the JSON
        public string Recommendation { get; set; } // Matches "Recommendation" in the JSON
    }

    public class LastClosingPriceModel
    {
        public string Date { get; set; } // Matches "Date" in "LastClosingPrice"
        public double Price { get; set; } // Matches "Price" in "LastClosingPrice"
    }

    public class PredictedPriceModel
    {
        public string Date { get; set; } // Matches "Date" in "PredictedPrices"
        public double Price { get; set; } // Matches "Price" in "PredictedPrices"
    }
}
