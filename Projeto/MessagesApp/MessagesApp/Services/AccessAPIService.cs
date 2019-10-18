using MessagesApp.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.Services
{
    public class AccessAPIService
    {
        public async Task<List<Messages>> GetMessages()
        {   // porta antiga da API http://localhost:56799/api/apimessages
            // Abra o prompt do NodeJs e insira o comando
            // iisexpress-proxy 56799 to 3000
            // 56799 representa a porta que o ISSExpress aplicou na WebAPI
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://172.16.2.31:3000/api/apimessages");
            var messages = JsonConvert.DeserializeObject<List<Messages>>(response);
            return messages;        
        }
    }
}
