using BookingApp.Domain.Models;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        public List<TourRequest> GetAll();
        public TourRequest Save(TourRequest tourRequest);       
        public int NextId();
        public void Delete(TourRequest tourRequest);
        public TourRequest Update(TourRequest tour);
        
    }
}
