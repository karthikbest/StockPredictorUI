namespace StockPredictor.Models
{
    public class PredictionResponse
    {
        public string CompanyName { get; set; } 
        public LastClosingPriceModel LastClosingPrice { get; set; } 
        public List<PredictedPriceModel> PredictedPrices { get; set; } 
        public string Recommendation { get; set; } 
    }

    public class LastClosingPriceModel
    {
        public string Date { get; set; } 
        public double Price { get; set; } 
    }

    public class PredictedPriceModel
    {
        public string Date { get; set; } 
        public double Price { get; set; } 
    }
}
