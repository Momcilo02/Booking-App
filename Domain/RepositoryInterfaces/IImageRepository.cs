using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        List<Image> GetAll();
        int NextId();
        Image Save(Image image);
        void Delete(Image image);
        Image Update(Image image);
    }
}
