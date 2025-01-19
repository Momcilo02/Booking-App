using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class VoucherDTO:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private DateOnly expiryDate;
        public DateOnly ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                if (value != expiryDate)
                {
                    expiryDate = value;
                    OnPropertyChanged(nameof(ExpiryDate));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                if (value != userId)
                {
                    userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private int guideId;
        public int GuideId
        {
            get { return guideId; }
            set
            {
                if (value != guideId)
                {
                    guideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }
            }
        }
        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                if (value != reason)
                {
                    reason = value;
                    OnPropertyChanged(nameof(Reason));
                }
            }
        }

        public VoucherDTO()
        {

        }
        public VoucherDTO(Voucher voucher)
        {
            Id = voucher.Id;
            ExpiryDate = voucher.ExpiryDate;
            Name = voucher.Name;
            UserId = voucher.UserId;
            GuideId = voucher.GuideId;
            Reason = voucher.Reason;
        }
        public Voucher ToVoucher()
        {
            return new Voucher(Id, expiryDate, name, userId, guideId,reason);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
