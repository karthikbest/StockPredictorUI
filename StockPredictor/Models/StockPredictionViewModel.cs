using System.Collections.Generic;

namespace StockPredictor.Models
{
    public class StockPredictionViewModel
    {
        // User-entered company name (from autocomplete input)
        public string CompanyName { get; set; }

        // Predicted stock prices for the next 5 days
        public List<double> PredictedPrices { get; set; }

        // Buy/Sell/Hold recommendation based on predictions
        public string Recommendation { get; set; }
    }
}
