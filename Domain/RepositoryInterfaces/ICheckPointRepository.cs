using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICheckPointRepository
    {
        List<CheckPoint> GetAll();
        CheckPoint Get(int id);
        int NextId();
        CheckPoint Save(CheckPoint checkPoint);
        void DeleteById(int id);
    }
}
