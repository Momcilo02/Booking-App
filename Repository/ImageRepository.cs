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
    public class ImageRepository : IImageRepository
    {
        private const string FilePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> _images;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
        }
        public List<Image> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _images = _serializer.FromCSV(FilePath);
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(x => x.Id) + 1;
        }
        public Image Save(Image image)
        {
            image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
            return image;
        }
        public void Delete(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image founded = _images.Find(img => img.Id == image.Id);
            if (founded != null)
            {
                _images.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _images);
        }
        public Image Update(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image current = _images.Find(img => img.Id == image.Id);
            int index = _images.IndexOf(current);
            _images[index] = image;
            _serializer.ToCSV(FilePath, _images);
            return image;
        }

    }
}
