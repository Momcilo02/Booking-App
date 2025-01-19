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
    public class CheckPointService
    {
        private readonly ICheckPointRepository _checkPointRepository;
        public CheckPointService()
        {
            _checkPointRepository = Injector.Injector.CreateInstance<ICheckPointRepository>();
        }
        public List<CheckPoint> GetAll()
        {
            return _checkPointRepository.GetAll();
        }

        public CheckPoint Save(CheckPoint checkpoint)
        {
            return _checkPointRepository.Save(checkpoint);
        }
        public CheckPoint GetCheckPointByTourAndId(int tourId, int checkpointId)
        {
            List<CheckPoint> checkPoints = GetAll();
            return checkPoints.FirstOrDefault(cp => cp.TourId == tourId && cp.Id == checkpointId);
        }

        public CheckPoint Get(int id)
        {
            return _checkPointRepository.Get(id);
        }

        public void RemoveAllTourCheckpoints(int tourId)
        {
            var tourCheckPoints = _checkPointRepository.GetAll().Where(x => x.TourId == tourId).ToList();
            tourCheckPoints.ForEach(x => _checkPointRepository.DeleteById(x.Id));
        }
    }
}
