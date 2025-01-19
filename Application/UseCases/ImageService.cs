using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
        }
        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public Image Save(Image tour)
        {
            return _imageRepository.Save(tour);
        }

        public Image Update(Image tour)
        {
            return _imageRepository.Update(tour);
        }
        public void Delete(Image tour)
        {
            _imageRepository.Delete(tour);
        }
        public List<Image> GetByAccommodationId(int id)
        {
            List<Image> images = new List<Image>();
            foreach (Image image in _imageRepository.GetAll()) { 
                if(image.Type == Enumeration.EntityType.Accommodation && image.EntityId == id)
                {
                    images.Add(image);
                }
            }
            return images;
        }
    }
}
