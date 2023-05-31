using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CountriesProxy.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CountriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(string countryName = null, int? countryPopulation = null, string sortingDirection = null)
        {
            try
            {
                var apiUrl = "https://restcountries.com/v3.1/all";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var countries = await JsonSerializer.DeserializeAsync<List<Country>>(contentStream);

                    var jsonCountries = JsonSerializer.Serialize(countries);

                    return Ok(jsonCountries);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching countries data: {ex.Message}");
            }
        }
    }
}
