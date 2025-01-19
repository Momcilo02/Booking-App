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
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/voucher.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }
        public Voucher Update(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher current = _vouchers.Find(ar => ar.Id == voucher.Id);
            int index = _vouchers.IndexOf(current);
            _vouchers[index] = voucher;
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }
        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Voucher Get(int id)
        {
            List<Voucher> vouchers = GetAll();
            return vouchers.Find(l => l.Id == id);
        }
        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(c => c.Id) + 1;
        }

        public void Delete(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher founded = _vouchers.Find(c => c.Id == voucher.Id);
            if (founded != null)
            {
                _vouchers.Remove(founded);
                _serializer.ToCSV(FilePath, _vouchers);
            }
        }

    }
}
