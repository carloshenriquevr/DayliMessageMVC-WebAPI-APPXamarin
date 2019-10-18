using MessagesApp.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MessagesApp.Services;

namespace MessagesApp
{
    public partial class MainPage : ContentPage
    {
        AccessAPIService _accessAPIService = new AccessAPIService();
        public MainPage()
        {
            InitializeComponent();
            UpdateDatas();
        }
        async void UpdateDatas()
        {
           var messages = await _accessAPIService.GetMessages();
            MessageListView.ItemsSource = messages.OrderBy(item => item.MessageDate).ToList();
        }
    }



}
