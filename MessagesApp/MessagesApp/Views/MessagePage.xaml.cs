using MessagesApp.Models.Entities;
using MessagesApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessagesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : MasterDetailPage
    {
        AccessAPIService accessAPIService = new AccessAPIService();
        ServiceDBMessages serviceDBMessages = new ServiceDBMessages(App.DbPath);
        public MessagePage()
        {
            InitializeComponent();
            UpdateDatas();
        }

        public Messages MessageListView { get; }

        public async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var returnAPI = await accessAPIService.GetMessages();
            var resultDB = serviceDBMessages.CarregarDados();

            if (resultDB.Any(item => item.MessageDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")))
            {
                await DisplayAlert("Alerta", "Messagem já atualizada", null, "ok");

            }
            else
            {
                var db = returnAPI.FirstOrDefault(item => item.MessageDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"));
                serviceDBMessages.Insert(db);
                UpdateDatas();
            }

        }
        public void UpdateDatas()
        {
            var messages = serviceDBMessages.CarregarDados();
            //MessageViewList.ItemsSource = messages.Where(item => item.MessageDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).Take(1).ToList() ;
            MessageViewList.ItemsSource = messages.OrderByDescending(item => item.MessageId).Take(1).ToList();
        }

    }
}