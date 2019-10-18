using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyMessageMVC.Models.Entities
{
    public class Messages
    {
        #region Entities
        [Key]
        public int MessageId { get; set; }

        [Display(Name = "Messagem:")]
        [Required(ErrorMessage = "{0} Obrigatório")]
        public string MessageText { get; set; }

        [Display(Name = "Data:")]
        [Required(ErrorMessage = "{0} Obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MessageDate { get; set; }
        #endregion

        public Messages()
        {
        }
        public Messages(int messageId, string messageText, DateTime messageDate)
        {
            MessageId = messageId;
            MessageText = messageText;
            MessageDate = messageDate;
        }
    }
}