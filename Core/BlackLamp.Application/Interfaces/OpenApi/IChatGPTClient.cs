using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackLamp.Application.Interfaces.OpenApi
{
    public interface IChatGPTClient
    {
        Task<string> SummarizeText(string text, int maxLength = 120);
    }
}
