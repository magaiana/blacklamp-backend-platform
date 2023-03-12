using BlackLamp.Application.Interfaces.OpenApi;
using BlackLamp.Domain.Models.OpenAi;
using System.Text.Json;

namespace BlackLamp.Application.Implementation
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly IChatGPTClient _chatGPTClient;

        public ChatGPTService(IChatGPTClient chatGPTClient)
        {
            _chatGPTClient = chatGPTClient ?? throw new ArgumentNullException(nameof(chatGPTClient));
        }

        public async Task<string> SummarizeText(string text, int maxLength = 120)
        { 
            var response = await _chatGPTClient.SummarizeText(text);
            return response;
        }
    }
}
