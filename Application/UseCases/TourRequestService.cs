using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        private ILanguageRepository _languageRepository;
        private ILocationRepository _locationRepository;
        private IVoucherRepository _voucherRepository;
        public TourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
            _languageRepository = Injector.Injector.CreateInstance<ILanguageRepository>();
            _locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            _voucherRepository = Injector.Injector.CreateInstance<IVoucherRepository>();
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }



        public List<TourRequest> GetComplexTourParts(int complexTourId)
        {
            return _tourRequestRepository.GetAll().Where(tr => tr.ComplexTourId == complexTourId).ToList();
        }

        public List<DateOnly> GetAvailableDatesForPart(int guideId, int complexTourId, DateOnly startDate, DateOnly endDate)
        {
            var tourRequests = _tourRequestRepository.GetAll();
            var guideTours = tourRequests.Where(tr => tr.GuideId == guideId && tr.Status == 1).ToList();
            var complexTourParts = tourRequests.Where(tr => tr.ComplexTourId == complexTourId && tr.GuideId != guideId && tr.Status == 1).ToList();

            List<DateOnly> availableDates = new List<DateOnly>();

            for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
            {
                bool isAvailable = true;

                if (guideTours.Any(tr => tr.FinalDate == date))
                {
                    isAvailable = false;
                }

                if (complexTourParts.Any(tr => tr.FinalDate == date))
                {
                    isAvailable = false;
                }

                if (isAvailable)
                {
                    availableDates.Add(date);
                }
            }

            return availableDates;
        }

        public void AcceptPartOfComplexTour(int tourRequestId, int guideId, DateOnly finalDate)
        {
            var tourRequest = _tourRequestRepository.GetAll().FirstOrDefault(tr => tr.Id == tourRequestId);

            if (tourRequest != null)
            {
                tourRequest.FinalDate = finalDate;
                tourRequest.Status = 1;
                tourRequest.GuideId = guideId;
                _tourRequestRepository.Update(tourRequest);
            }
        }

        public List<TourRequest> GetComplexTours()
        {
            List<TourRequest> tourRequests = new List<TourRequest>();
            foreach (var tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.ComplexTourId != 0)
                {
                    tourRequests.Add(tourRequest);
                }
            }
            return tourRequests;

        }

        public void CancelToursAndIssueVouchers(int guideId)
        {
            var tourRequests = _tourRequestRepository.GetAll().Where(tr => tr.GuideId == guideId && tr.Status == 0).ToList();

            foreach (var tourRequest in tourRequests)
            {
                tourRequest.Status = -1; 
                _tourRequestRepository.Update(tourRequest);

                var existingVouchers = _voucherRepository.GetAll().Where(v => v.UserId == tourRequest.TouristId && v.GuideId == guideId).ToList();

                if (existingVouchers.Count > 0)
                {
                    foreach (var voucher in existingVouchers)
                    {
                        voucher.GuideId = -1; 
                        _voucherRepository.Update(voucher);
                    }
                }
                else
                {
                    var newVoucher = new Voucher
                    {
                        ExpiryDate = DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
                        Name = "Tour Voucher",
                        UserId = tourRequest.TouristId,
                        GuideId = -1,
                        Reason = $"Tour canceled due to guide resignation"
                    };
                    _voucherRepository.Save(newVoucher);
                }
            }
        }
        public TourRequest GetLast()
        {
            return _tourRequestRepository.GetAll().LastOrDefault();
        }
        public List<TourRequest> GetComplexTourPart(int complexTourId)
        {
            return _tourRequestRepository.GetAll().Where(x => x.ComplexTourId == complexTourId).ToList();
        }
        public bool IsGuideAvailable(int guideId, DateOnly date) 
        {
            var tourRequests = _tourRequestRepository.GetAll();

            var guideAcceptedTourRequests = tourRequests.Where(x=>x.GuideId == guideId).ToList();

            if(guideAcceptedTourRequests.Where(x=>x.FinalDate == date).Any()) 
            {
                return false;
            }

            return true;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }
        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }

        public Language? GetMostRequestedLanguage() 
        {
            var tourRequests = _tourRequestRepository.GetAll();

            var oneYearAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));


            var languageCounts = new Dictionary<int, int>();

            foreach (var tourRequest in tourRequests.Where(t => t.StartDate >= oneYearAgo))
            {
                if (languageCounts.ContainsKey(tourRequest.Language.Id))
                {
                    languageCounts[tourRequest.Language.Id]++;
                }
                else
                {
                    languageCounts[tourRequest.Language.Id] = 1;
                }
            }

            if (languageCounts.Count == 0) return null;

            var mostRequestedLanguageId = languageCounts
               .OrderByDescending(kv => kv.Value)
               .Select(kv => kv.Key)
               .FirstOrDefault();

            return _languageRepository.Get(mostRequestedLanguageId);
            
        }

        public Location? GetMostRequestedLocation()
        {
            var tourRequests = _tourRequestRepository.GetAll();

            var oneYearAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var locationCounts = new Dictionary<int, int>();

            foreach (var tourRequest in tourRequests.Where(t => t.StartDate >= oneYearAgo))
            {
                if (locationCounts.ContainsKey(tourRequest.Location.Id))
                {
                    locationCounts[tourRequest.Location.Id]++;
                }
                else
                {
                    locationCounts[tourRequest.Location.Id] = 1;
                }
            }

            if (locationCounts.Count == 0) return null;

            var mostRequestedLocationId = locationCounts
               .OrderByDescending(kv => kv.Value)
               .Select(kv => kv.Key)
               .FirstOrDefault();

            return _locationRepository.Get(mostRequestedLocationId);
        }

        public void ChangeStatus()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly twoDaysAgo = today.AddDays(2);
            List<TourRequest> allTourRequests = GetAll();
            foreach (TourRequest tourRequest in allTourRequests)
            {
                if (tourRequest.StartDate <= twoDaysAgo && tourRequest.Status != 1)
                {

                    tourRequest.Status = -1;
                    _tourRequestRepository.Update(tourRequest);
                }
            }
        }
        public List<TourRequest> GetByDescription(string description)
        {
            return _tourRequestRepository.GetAll().Where(x => x.Description == description).ToList();
        }
        public List<TourRequest> GetAcceptedTours()
        {
            var acceptedTours = new List<TourRequest>();
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.Status == 1 && tourRequest.FinalDate.Year == 2024)
                {
                    acceptedTours.Add(tourRequest);
                }
            }
            return acceptedTours;
        }
        public List<TourRequest> GetRejectedTours()
        {
            var acceptedTours = new List<TourRequest>();
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.Status != 1)
                {
                    acceptedTours.Add(tourRequest);
                }
            }
            return acceptedTours;
        }
        public double GetAceptedTourProcentageByYear(int year)
        {
            double procentage;
            double counterRejected = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                counterAll++;
                if (tourRequest.Status == 1 && tourRequest.StartDate.Year == year)
                {
                    counterRejected++;
                }
            }
            procentage = (counterRejected / counterAll) * 100;
            return procentage;
        }

        public double GetRejectedTourProcentageByYear(int year)
        {
            double procentage;
            double counterRejected = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                counterAll++;
                if (tourRequest.Status != 1 && tourRequest.StartDate.Year == year)
                {
                    counterRejected++;
                }
            }
            procentage = (counterRejected / counterAll) * 100;
            return procentage;
        }
        public double GetAceptedTourProcentage()
        {
            double procentage;
            double counterRejected = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                counterAll++;
                if (tourRequest.Status == 1)
                {
                    counterRejected++;
                }
            }
            procentage = (counterRejected / counterAll) * 100;
            return procentage;
        }
        public double GetRejectedTourProcentage()
        {
            double procentage;
            double counterRejected = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                counterAll++;
                if (tourRequest.Status != 1)
                {
                    counterRejected++;
                }
            }
            procentage = (counterRejected / counterAll) * 100;
            return procentage;
        }
        public double GetPeopleGuestProcentage()
        {
            double procentage;
            double sumGuestNum = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                counterAll++;
                sumGuestNum += tourRequest.GuestNumber;
            }
            procentage = sumGuestNum / counterAll;
            return procentage;
        }
        public double GetPeopleGuestProcentageByYear(int year)
        {
            double procentage;
            double sumGuestNum = 0;
            double counterAll = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.StartDate.Year == year)
                {
                    counterAll++;
                    sumGuestNum += tourRequest.GuestNumber;
                }
            }
            procentage = sumGuestNum / counterAll;
            return procentage;
        }

        public Dictionary<string, int> GetTourRequestStatistics(int? locationId, int? languageId, int? year)
        {
            var tourRequests = _tourRequestRepository.GetAll();
            var query = tourRequests.AsQueryable();

            if (locationId.HasValue)
            {
                query = query.Where(tr => tr.Location.Id == locationId.Value);
            }

            if (languageId.HasValue)
            {
                query = query.Where(tr => tr.Language.Id == languageId.Value);
            }

            Dictionary<string, int> statistics = new Dictionary<string, int>();

            if (year.HasValue)
            {
                statistics = query.Where(tr => tr.StartDate.Year == year.Value || tr.EndDate.Year == year.Value)
                                   .GroupBy(tr => tr.StartDate.Month)
                                   .OrderBy(g => g.Key)
                                   .ToDictionary(g => g.Key.ToString("00"), g => g.Count());
            }
            else
            {
                statistics = query.GroupBy(tr => tr.StartDate.Year)
                                   .OrderBy(g => g.Key)
                                   .ToDictionary(g => g.Key.ToString(), g => g.Count());
            }

            return statistics;
        }
    }
}
