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
    public class CheckPointRepository : ICheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkPoints.csv";

        private readonly Serializer<CheckPoint> _serializer;
        private List<CheckPoint> _checkPoints;

        public CheckPointRepository()
        {
            _serializer = new Serializer<CheckPoint>();
            _checkPoints = _serializer.FromCSV(FilePath);
        }
        public List<CheckPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public CheckPoint Get(int id)
        {
            List<CheckPoint> checkPoints = GetAll();
            return checkPoints.Find(l => l.Id == id);
        }

        public int NextId()
        {
            _checkPoints = GetAll();
            if (_checkPoints.Count < 1)
            {
                return 1;
            }
            return _checkPoints.Max(c => c.Id) + 1;
        }
        public CheckPoint Save(CheckPoint checkPoint)
        {
            checkPoint.Id = NextId();
            _checkPoints = _serializer.FromCSV(FilePath);
            _checkPoints.Add(checkPoint);
            _serializer.ToCSV(FilePath, _checkPoints);
            return checkPoint;
        }

        public void DeleteById(int id)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckPoint founded = _checkPoints.Find(ar => ar.Id == id);
            if (founded != null)
            {
                _checkPoints.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _checkPoints);
        }


    }
}
