namespace StockPredictor.Models
{
    public class Company
    {
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public string DisplayName { get; set; } // Ticker - CompanyName for dropdown display
    }
}
