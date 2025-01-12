using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockPredictor.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockPredictionApp.Controllers
{
    public class StockController : Controller
    {
        private readonly string apiBaseUrl = "https://stockml-api.onrender.com/"; 

        private readonly string sp500FilePath = "App_Data/sp500.json"; // Path to the S&P 500 tickers JSON file

        // Load the index page with the S&P 500 dropdown
        public IActionResult Index()
        {
            try
            {
                var companies = LoadSp500Companies();
                ViewBag.Companies = companies.Select(c => new { c.Ticker, c.DisplayName }).ToList();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Failed to load tickers: {ex.Message}";
                return View();
            }
        }

        // Fetch predictions from the Flask API
        [HttpPost]
        public async Task<IActionResult> Predict(string selectedTicker)
        {
            if (string.IsNullOrEmpty(selectedTicker))
            {
                ViewBag.Error = "Please select a valid company.";
                var companies = LoadSp500Companies();
                ViewBag.Companies = companies.Select(c => new { c.Ticker, c.DisplayName }).ToList();
                return View("Index");
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestUrl = $"{apiBaseUrl}/predict?company={selectedTicker}";
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize JSON response into PredictionResponse model
                        var prediction = JsonConvert.DeserializeObject<PredictionResponse>(jsonResponse);

                        return View("PredictionResult", prediction);
                    }
                    else
                    {
                        ViewBag.Error = $"API Error: {response.ReasonPhrase}";
                        var companies = LoadSp500Companies();
                        ViewBag.Companies = companies.Select(c => new { c.Ticker, c.DisplayName }).ToList();
                        return View("Index");
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                ViewBag.Error = $"JSON Parsing Error: {ex.Message}";
                var companies = LoadSp500Companies();
                ViewBag.Companies = companies.Select(c => new { c.Ticker, c.DisplayName }).ToList();
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                var companies = LoadSp500Companies();
                ViewBag.Companies = companies.Select(c => new { c.Ticker, c.DisplayName }).ToList();
                return View("Index");
            }
        }

        // Load S&P 500 Companies from JSON file
        private List<Company> LoadSp500Companies()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), sp500FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception("S&P 500 configuration file not found.");
            }

            var json = System.IO.File.ReadAllText(filePath);
            var companies = JsonConvert.DeserializeObject<List<Company>>(json);

            // Add a "DisplayName" field for dropdown display
            companies.ForEach(c => c.DisplayName = $"{c.Ticker} - {c.CompanyName}");
            return companies;
        }
    }

 
}
