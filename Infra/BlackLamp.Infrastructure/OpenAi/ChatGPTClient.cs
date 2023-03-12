using BlackLamp.Application.Interfaces.OpenApi;
using BlackLamp.Domain.Models.OpenAi;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlackLamp.Infrastructure.OpenAi
{
    public class ChatGPTClient : IChatGPTClient
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "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";
        private const string API_BASE_URL = "https://api.openai.com/v1/completions";

        public ChatGPTClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
        }

        public async Task<string> SummarizeText(string text, int maxLength = 120)
        {
            var prompt = $"Please summarize the following text in {maxLength} characters:\n{text}";

            var data = new
            {
                prompt,
                max_tokens = maxLength,
                n = 1,
                stop = "\n",
                model = "text-davinci-003"
            };

            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(API_BASE_URL, content);
            if (response.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<ChatGPTResponse>(responseString);

                return responseJson?.Choices[0]?.Text?.Trim() ?? string.Empty;
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
    }
}
