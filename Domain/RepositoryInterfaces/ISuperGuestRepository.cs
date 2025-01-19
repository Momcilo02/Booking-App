using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    interface ISuperGuestRepository
    {
        List<SuperGuest> GetAll();
        SuperGuest GetbyId(int id);
        SuperGuest Save(SuperGuest newSuperGuest);
        void Delete(SuperGuest newSuperGuest);
        SuperGuest GetByGuestId(int id);
    }
}
