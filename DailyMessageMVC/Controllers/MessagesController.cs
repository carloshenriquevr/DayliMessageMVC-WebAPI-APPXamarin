using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DailyMessageMVC.Models.Entities;
using DailyMessageMVC.Models.Services;

namespace DailyMessageMVC.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MessageService _messageService = new MessageService();
        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var resultMessage = _messageService.FindAllAsync();

            return View(await resultMessage);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Messages message)
        {
            var messages = _messageService.FindAllAsync();

            if (!ModelState.IsValid)
            {
                var resultMessage = new Messages { MessageId = message.MessageId, MessageText = message.MessageText, MessageDate = message.MessageDate };

                return View(resultMessage);
            }
            await _messageService.Add(message);
            return RedirectToAction("Index");
        }

        //public ActionResult Edit(int? calendarId)
        //{
        //    if (calendarId == null)
        //    {
        //        return View();
        //    }
        //    var calendar = _calendarService.GetById(calendarId.Value);
        //    var events = _eventService.FindAll();
        //    var viewModel = new CalendarFormViewModel { Calendar = calendar, Events = events };

        //    ViewBag.Events = GetAllEvents(events);
        //    ViewBag.Status = GetAllStatus();
        //    return View(viewModel);
    }


}
