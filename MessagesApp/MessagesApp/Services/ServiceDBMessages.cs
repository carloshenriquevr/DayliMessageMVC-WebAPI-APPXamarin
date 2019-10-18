using MessagesApp.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.Services
{
    public class ServiceDBMessages
    {
        SQLiteConnection conn;

        public string StatusMessage { get; set; }

        public ServiceDBMessages(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Messages>();
        }
        public void Insert(Messages message)
        {
            if (message.MessageText != "" && message.MessageDate != null)
            {
                int result = conn.Insert(message);
                this.StatusMessage = string.Format("Nova mensagem");
            }
            else
            {
                this.StatusMessage = string.Format("Não há novas mensagens");
            }
        }
        public List<Messages> CarregarDados()
        {
            List<Messages> messages = new List<Messages>();
            try
            {
                messages = conn.Table<Messages>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return messages;
        }
    }
}
