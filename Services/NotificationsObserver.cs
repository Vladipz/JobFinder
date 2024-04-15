using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net.Sockets;

namespace JobFinder.Services
{
    public class NotificationsObserver : IObserver<Vacancy>
    {
        private IDisposable _unsubscriber;
        private INotificationRepository _notificationRepository;
        public NotificationsObserver(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public NotificationsObserver()
        {
               
        }
        public void Subscribe(IObservable<Vacancy> provider) //TODO тут в документації по іншому
        {
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
        }

        public void Unsubscribe()
        {
            _unsubscriber?.Dispose();
            //_flights.Clear(); //з документації
        }
        public void OnCompleted()
        {
            //_flights.Clear(); //з документації
        }

        public void OnError(Exception error)
        {
            // No implementation.
        }

        public void OnNext(Vacancy value)
        {

            //var employer = _
            var subscribers =  value.Employer.Subscribers?.ToList();
            foreach (var item in subscribers)
            {
                Console.WriteLine(item.Email);
            }
            if (subscribers != null)
            {
                foreach (var subscriber in subscribers)
                {
                    Notification notification = new Notification()
                    {
                        Title = value.Title,
                        Description = "New vacancy",
                        OwnerId = subscriber.Id, 
                        Owner = subscriber, 
                        VacancyId = value.Id
                    };
                _notificationRepository.Add(notification);
                }
            }
            
            //TODO тут має бути реалізація додавання повідомлень в БД з повідомленнями
            
            
        }
        
    }
}
