using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesApp.Models.Entities
{
    [Table("dbo.Messages")]
    public class Messages
    {
        [PrimaryKey, AutoIncrement]
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
