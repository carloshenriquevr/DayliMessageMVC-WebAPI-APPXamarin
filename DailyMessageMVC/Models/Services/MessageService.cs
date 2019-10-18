using DailyMessageMVC.Data;
using DailyMessageMVC.Models.Entities;
using DailyMessageMVC.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DailyMessageMVC.Models.Services
{
    public class MessageService
    {
        public IUnitOfWork<Messages> IUnitMessage { get; set; }

        public MessageService()
        {
            IUnitMessage = new GenericRepositoryMessage<Messages>();
        }
        public  async  Task<IEnumerable<Messages>> FindAllAsync()
        {
            return  await IUnitMessage.GetAllAsync();
        }
        public async Task<int> Add(Messages Messages)
        {
            return  await IUnitMessage.SaveAsync(Messages);
        }
        public async  Task<Messages> GetById(int id)
        {
            return  await IUnitMessage.GetByIdAsync(id);
        }
        public async Task<int> UpdateAsync(Messages Messages)
        {
            return  await IUnitMessage.UpadateAsync(Messages);
        }
        public void Remove(Messages Messages)
        {
            IUnitMessage.Delete(Messages);
        }
        public IEnumerable<Messages> SearhMessages(string query)
        {
            int id;

            if (int.TryParse(query, out id))
            {
                return IUnitMessage.Where(x => x.MessageId == id);
            }
            return IUnitMessage.Where(x => x.MessageText.Contains(query));
        }
    }
}
