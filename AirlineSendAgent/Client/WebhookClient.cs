using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Client
{
    public class WebhookClient : IWebhookClient
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public WebhookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }
        public async Task SendWebhookNotificationAsync(FlightDetailChangePayloadDto flightDetailChangePayloadDto)
        {
            var serializedPayload = JsonSerializer.Serialize(flightDetailChangePayloadDto);

            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailChangePayloadDto.WebhookURI);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedPayload);

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                using(var response = await httpClient.SendAsync(request))
                {
                    Console.WriteLine($" ----->  Successful  <-----------");
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unsuccessful {ex.Message}");
            }
        }
    }
}