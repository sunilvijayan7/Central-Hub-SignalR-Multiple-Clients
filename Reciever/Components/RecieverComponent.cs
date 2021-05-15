using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Reciever.Common;
using Reciever.Services.PushNotifications;

namespace Reciever.Components
{
    public class RecieverComponent : ComponentBase
    {
        public string url = "https://localhost:44339/notificationhub";
        public HubConnection _connection = null;
        public bool isConnected = false;
        public string connectionStatus = "Closed";
        public List<string> notifications = new List<string>();
        [Inject] public NavigationManager navigationManager { get; set; }
        public HttpClient _httpClient { get; set; }
        //[Parameter] public PushNotification NotificationModel { get; set; }
        //[Inject] public PushNotificationService notificationService { get; set; }
        public async Task NotifyMe()
        {
           // NotificationModel=new PushNotification();
           // NotificationModel.NotificationId = 1;
           // NotificationModel.NotificationObject = "From Admin Side";
           // var result = await notificationService.PostNotification(NotificationModel);
            //await _httpClient.PostAsJsonAsync(navigationManager.ToAbsoluteUri("https://localhost:44339/api/Notifications/"), "Hello");
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var builder = new HubConnectionBuilder();
            var httpConnectionOptions = HttpConnectionFactoryInternal.createHttpConnectionOptions(); // work around constructor call
            httpConnectionOptions.Url = navigationManager.ToAbsoluteUri(url);
            builder.Services.AddSingleton<EndPoint>(new UriEndPoint(httpConnectionOptions.Url));
            var opt = Microsoft.Extensions.Options.Options.Create<Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions>(httpConnectionOptions);
            builder.Services.AddSingleton(opt);
            builder.Services.AddSingleton<Microsoft.AspNetCore.Connections.IConnectionFactory, HttpConnectionFactoryInternal>();
            // normal stuff
            HubConnection hubConnection = builder.Build();
            await hubConnection.StartAsync();

            isConnected = true;
            connectionStatus = "Connected :-)";

            hubConnection.Closed += async (s) =>
            {
                isConnected = false;
                connectionStatus = "Disconnected";
                await _connection.StartAsync();
                isConnected = true;
            };

            hubConnection.On<string>("notification", m =>
            {
                notifications.Add(m);
                StateHasChanged();
            });
            
        }
    }
}
