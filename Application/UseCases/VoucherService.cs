using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Application.UseCases
{
    public class VoucherService
    {
        private IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = Injector.Injector.CreateInstance<IVoucherRepository>();
        }

        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public Voucher GetByName(string name)
        {
            return GetAll().FirstOrDefault(v => v.Name == name);
        }

        public Voucher Save(Voucher voucher)
        {
            return _voucherRepository.Save(voucher);
        }

        public Voucher Get(int id)
        {
            return _voucherRepository.Get(id);
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }

        public List<string> GetVoucherNamesForUser(int userId)
        {
            return GetAll()
                .Where(v => v.UserId == userId && v.ExpiryDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) >= 0)
                .Select(v => v.Name)
                .ToList();
        }
    }
}
