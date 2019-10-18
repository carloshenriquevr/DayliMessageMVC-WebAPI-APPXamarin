using DailyMessageMVC.Models.Entities;
using DailyMessageMVC.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class APIMessagesController : ApiController
    {
        private MessageService _messageService = new MessageService();
        //GET API/APIMessages
        public async Task<IEnumerable<Messages>> Get()
        {
            return await _messageService.FindAllAsync();
        }
    }
}
