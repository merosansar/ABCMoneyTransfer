using Newtonsoft.Json;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ABCMoneyTransfer.Services
{
    public class ExchangeRateService()
    {
        //private readonly HttpClient _httpClient = httpClient;

        public async Task<List<CurrencyRate>> GetExchangeRatesAsync(string from, string to)
        {
            var url = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={from}&to={to}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var exchangeRateResponse = JsonConvert.DeserializeObject<ExchangeRateResponse>(jsonResponse);

                    //// Return rates from the response
                    //return exchangeRateResponse?.Data?.Payload?.FirstOrDefault()?.Rates ?? new List<Rate>();

                    // Extract the required details
                    List<CurrencyRate> currencyRates = new List<CurrencyRate>();

                    foreach (var payload in exchangeRateResponse.Data.Payload)
                    {
                        foreach (var rate in payload.Rates)
                        {
                            currencyRates.Add(new CurrencyRate
                            {
                                Iso3 = rate.Currency.Iso3,
                                Name = rate.Currency.Name,
                                BuyRate = rate.Buy,
                                SellRate = rate.Sell
                            });
                        }
                    }
                    return currencyRates;
                }
                else
                {
                    throw new Exception($"API call failed with status code: {response.StatusCode}");
                }
            }
        }
    }

    public class ExchangeRateResponse
    {
        public Status Status { get; set; }
        public Errors Errors { get; set; }
        public Params Params { get; set; }
        public Data Data { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
    }

    public class Errors
    {
        public object Validation { get; set; }
    }

    public class Params
    {
        public string Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PerPage { get; set; }
        public string Page { get; set; }
    }

    public class Data
    {
        public List<Payload> Payload { get; set; }
    }

    public class Payload
    {
        public string Date { get; set; }
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        public Currency Currency { get; set; }
        public string Buy { get; set; }
        public string Sell { get; set; }
    }

    public class Currency
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
    }

    public class Pagination
    {
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public string PerPage { get; set; }
        public int? Total { get; set; }
    }

    public class CurrencyRate
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public string BuyRate { get; set; }
        public string SellRate { get; set; }
    }
}
