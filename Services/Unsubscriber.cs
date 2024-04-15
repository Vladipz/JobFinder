using JobFinder.Models;
using System;

namespace JobFinder.Services
{
    public class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<Vacancy>> _observers;
        private readonly IObserver<Vacancy> _observer;

        public Unsubscriber(List<IObserver<Vacancy>> observers, IObserver<Vacancy> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }
}
