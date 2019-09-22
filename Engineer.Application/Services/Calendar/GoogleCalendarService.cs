using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace Engineer.Application.Services.Calendar
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly string _googleCredentials = "api-credentials.json";
        private readonly string _calendarId = "fubestyrelse@gmail.com";
        private readonly CalendarService _google;


        public GoogleCalendarService()
        {
            string[] scopes = {CalendarService.Scope.Calendar};

            ServiceAccountCredential credential;

            using (var stream = new FileStream(_googleCredentials, FileMode.Open, FileAccess.Read))
            {
                var configuration = Google.Apis.Json.NewtonsoftJsonSerializer.Instance
                    .Deserialize<JsonCredentialParameters>(stream);

                credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(configuration.ClientEmail){ Scopes = scopes}
                    .FromPrivateKey(configuration.PrivateKey));
            }

            _google = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "RIA Aarhus Webshop"
            });
        }

        public void CreateLoanOrder(OrderDTO order)
        {

            var items = order.Products.Aggregate(string.Empty, (current, item) => current + $"({item.ProductName}: {item.Quantity}) ");

            var loanEvent = new Event
            {
                //Id = $"{order.OrderNumber.Replace('-', 'v')}",
                Summary = $"{order.Name} - {items}",
                Description = $"Navn: {order.Name}\nEmail: {order.Email}\nAU ID: {order.UniversityId}\nOrdre: {order.OrderNumber}",
                Start = new EventDateTime()
                {
                    DateTime = order.StartDate.DateTime.AddHours(8)
                },
                End = new EventDateTime()
                {
                    DateTime = order.EndDate.DateTime.AddHours(8)
                }
            };

            var request = _google.Events.Insert(loanEvent, _calendarId);

            try
            {
                request.Execute();
            }
            catch (Exception e)
            {
                HandleCalendarException(loanEvent);
            }
        }

        private void HandleCalendarException(Event loanEvent)
        {
            try
            {
                _google.Events.Update(loanEvent, _calendarId, loanEvent.Id).Execute();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}