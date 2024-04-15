using JobFinder.Interfaces;
using JobFinder.Models;
using System.Linq;
using System.Net.Sockets;

namespace JobFinder.Services
{
    public class VacancyService : IVacancyService, IObservable<Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private List<IObserver<Vacancy>> _observers = new List<IObserver<Vacancy>>();// not right

        public VacancyService(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }
        //public void Subscribe(IObserver<Vacancy> observer)
        //{
        //    if (!observers.Contains(observer))
        //    {
        //        observers.Add(observer);
        //    }
        //}

        public void Unsubscribe(IObserver<Vacancy> observer)
        {
            _observers.Remove(observer);
        }

        public IDisposable Subscribe(IObserver<Vacancy> observer)
        {
            // Check whether observer is already registered. If not, add it.
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }


        public async Task<bool> Update(Vacancy vacancy)
        {
            bool result = _vacancyRepository.Update(vacancy);
            if (vacancy.VacancyStatus == Data.Enum.Status.Approved)
            {

                vacancy = await _vacancyRepository.GetByIdAsync(vacancy.Id);
                foreach (var observer in _observers)
                {

                    observer.OnNext(vacancy);
                }
            }
            
            return result; 
        }

        IDisposable IObservable<Vacancy>.Subscribe(IObserver<Vacancy> observer)
        {
            throw new NotImplementedException();
        }

        
    }
}
