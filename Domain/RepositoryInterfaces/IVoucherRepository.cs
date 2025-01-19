using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();
        Voucher Get(int id);
        Voucher Save(Voucher voucher);
        void Delete(Voucher voucher);
        Voucher Update(Voucher voucher);

    }
}
