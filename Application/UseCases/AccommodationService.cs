using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository accommodationRepository;
        public AccommodationService()
        {
            accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
        }
        public List<Accommodation> GetAll()
        {
            return accommodationRepository.GetAll();
        }
        public Accommodation Get(int id)
        {
            return accommodationRepository.GetAccommodation(id);
        }
    }
}
