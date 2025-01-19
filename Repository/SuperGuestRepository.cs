using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class SuperGuestRepository : ISuperGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/super-guests.csv";
        private readonly Serializer<SuperGuest> _serializer;
        private List<SuperGuest> superGuests;
        public SuperGuestRepository()
        {
            _serializer = new Serializer<SuperGuest>();
            superGuests = _serializer.FromCSV(FilePath);
        }

        public void Delete(SuperGuest newSuperGuest)
        {
            superGuests = _serializer.FromCSV(FilePath);
            SuperGuest  founded = superGuests.Find(ar => ar.Id == newSuperGuest.Id);
            if(founded != null)
            {
                superGuests.Remove(founded);
            }
            _serializer.ToCSV(FilePath, superGuests);
        }

        public List<SuperGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);

        }

        public SuperGuest GetByGuestId(int id)
        {
            superGuests = _serializer.FromCSV(FilePath);
            return superGuests.Find(x => x.GuestId == id);
        }

        public SuperGuest GetbyId(int id)
        {
            superGuests = _serializer.FromCSV(FilePath);
            return superGuests.Find(x => x.Id == id);
        }

        public SuperGuest Save(SuperGuest newSuperGuest)
        {
            newSuperGuest.Id = NextId();
            superGuests = _serializer.FromCSV(FilePath);
            superGuests.Add(newSuperGuest);
            _serializer.ToCSV(FilePath, superGuests);
            return newSuperGuest;
        }
        private int NextId()
        {
            superGuests = _serializer.FromCSV(FilePath);
            if(superGuests.Count < 1)
            {
                return 1;
            }
            return superGuests.Max(x => x.Id) + 1;
        }

    }
}
