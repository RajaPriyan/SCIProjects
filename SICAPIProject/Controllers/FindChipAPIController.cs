using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SICAPIProject.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SICAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindChipAPIController : ControllerBase
    {
        // GET: api/<FindChipAPIController>
        private readonly HttpClient _httpClient;

        // Constructor to inject HttpClient
        public FindChipAPIController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET api/<FindChipAPIController>/2n222
        [HttpGet("{partNumber}")]
        public async Task<IActionResult> GetDistributorOffers(string partNumber)
        {
            try
            {
                // Construct URL for FindChips API
                var url = $"https://api.findchips.com/v1/parts/{partNumber}/distributors";

                // Make the HTTP request asynchronously
                var response = await _httpClient.GetAsync(url);

                // Check if the response status code is successful (2xx range)
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    // Deserialize the JSON response
                    dynamic result = JsonConvert.DeserializeObject(responseData);
                    var distributorOffers = new List<DistributorOffer>();

                    foreach (var distributor in result.distributors)
                    {
                        var offers = distributor.offers;
                        foreach (var offer in offers)
                        {
                            var distributorOffer = new DistributorOffer
                            {
                                DistributorName = distributor.name,
                                SellerName = offer.sellerName ?? "N/A",
                                MOQ = offer.moq ?? "N/A",
                                SPQ = offer.spq ?? "N/A",
                                UnitPrice = offer.unitPrice ?? "N/A",
                                Currency = offer.currency ?? "N/A",
                                OfferUrl = offer.offerUrl ?? "N/A"
                            };
                            distributorOffers.Add(distributorOffer);
                        }
                    }

                    return Ok(distributorOffers);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Return a 404 error if the part number is not found
                    return NotFound(new { message = "Part number not found." });
                }
                else
                {
                    // Handle other unexpected status codes (e.g., 500 internal server error)
                    return StatusCode((int)response.StatusCode, new { message = "An error occurred while fetching data." });
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
    }
}
