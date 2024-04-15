using JobFinder.Models;
using System.Net.Sockets;

namespace JobFinder.Interfaces
{
    public interface IVacancyService
    {
        Task<bool> Update(Vacancy vacancy);
        IDisposable Subscribe(IObserver<Vacancy> observer);
        void Unsubscribe(IObserver<Vacancy> observer);


    }
}
